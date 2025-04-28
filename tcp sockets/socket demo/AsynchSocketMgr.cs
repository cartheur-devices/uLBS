using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;
using System.Reflection;

namespace AsynchSocketLib
{
	class SocketStateObject
	{
		private Socket _workSocket;
		public byte[] buffer;
		public StringBuilder sb = new StringBuilder();
		public bool complete;
		public const int BUFFER_SIZE = 1024;

		public SocketStateObject(Socket _socket)
		{
			buffer = new byte[BUFFER_SIZE];
			complete = false;
			_workSocket = _socket;
		}

		public Socket WorkSocket
		{
			get { return _workSocket; }
		}
	}

	class AsynchSocketManager
	{

		private const int SleepPeriod = 10; // sleep for 10 miliseconds

		private Queue TransmitQueue = new Queue();
		private Queue ReceiveQueue = new Queue();
		private ReaderWriterLock TransmitLock = new ReaderWriterLock();
		private ReaderWriterLock ReceiveLock = new ReaderWriterLock();
		private bool _IsConnected = false;
		private Socket _Socket;
		//private TcpClient client;
		
		private ManualResetEvent ReaderDone = new ManualResetEvent(false);
		private ManualResetEvent WriterDone = new ManualResetEvent(false);
		private ManualResetEvent StopEvent = new ManualResetEvent(false);
		private AutoResetEvent DataReady = new AutoResetEvent(false);

		private int MyId;
		static private int Id;
		private long StartTime;

		public delegate void OnReceiveDelegate(int id, string msg);
		public delegate void OnConnectDelegate(int id, bool bSuccess);
		public delegate void OnDisconnectDelegate(int id);

		public OnReceiveDelegate OnReceive;
		public OnConnectDelegate OnConnect;
		public OnDisconnectDelegate OnDisconnect;

		public AsynchSocketManager(	OnConnectDelegate OnConnect, 
			OnDisconnectDelegate OnDisconnect,
			OnReceiveDelegate OnReceive)
		{
			// copy the delegates
			this.OnReceive = OnReceive;
			this.OnConnect = OnConnect;
			this.OnDisconnect = OnDisconnect;

			Id++; // increment our "global" id - could use 'real' quid's here...
			MyId = Id; // save the value locally
			StartTime = DateTime.Now.Ticks;
		}


		~ AsynchSocketManager()
		{
			Disconnect();
		}

		public int SessionID
		{
			get { return MyId; }
		}

		public int Connect(string hostName, int serviceport)
		{
			if (_IsConnected)
				return -1; //'no need to do anything once connected

			// resolve...
			IPHostEntry hostEntry = Dns.Resolve(hostName);
			if ( hostEntry != null )
			{
				// create an end-point for the first address...
				IPEndPoint endPoint = new IPEndPoint(hostEntry.AddressList[0], serviceport);

				// create a tcp client...
				TcpClient client = new TcpClient();

				try
				{
					client.Connect(endPoint);
				}
				catch (Exception ex)
				{
					ex = ex;
					OnConnect(MyId, false);
					return -1;
				}

				_Socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
				try
				{
					_Socket.Connect( endPoint );
				}
				catch (Exception ex)
				{
					ex = ex;
					OnConnect(MyId, false);
					return -1;
				}

				OnConnect(MyId, true);

				ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveThreadEntryPoint));
				ThreadPool.QueueUserWorkItem(new WaitCallback(SendThreadEntryPoint));
				_IsConnected = true;

				// return this unique id
				return MyId;
			}
			else
			{
				return -1;
			}
		}

//		private void ProcessConnection(TcpClient client)
//		{
//			// set it up...
//			_client = client;
//
//			// get the socket through reflection...
//			PropertyInfo propertyInfo = _client.GetType().GetProperty("Client", BindingFlags.Instance | BindingFlags.NonPublic);
//			if (propertyInfo == null)
//				return;
//
//			Socket = (Socket)propertyInfo.GetValue(_client, null);
//
//			// spin up the threads...
//			//add the two threads for Receive and transmit
//			ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveThreadEntryPoint));
//			ThreadPool.QueueUserWorkItem(new WaitCallback(SendThreadEntryPoint));
//			_isConnected = true;
//		}


		public bool IsConnected
		{
			get { return (_Socket == null ? false : _Socket.Connected); }
		}

		public void Disconnect()
		{
			if (IsConnected)
			{
				_IsConnected = false;

				// signal the threads to end
				StopEvent.Set();

				// wait for workers to spin down
				//WaitHandle[] waithandles;

				//waithandles = new WaitHandle[2];
				//waithandles[0] = _readerDone;
				//waithandles[1] = _writerDone;

				// wait for those guys to stop
				//WaitHandle.WaitAll(waithandles, -1, false);

				// now kill the socket
				if (_Socket != null)
				{
					_Socket.Close();
				}
				OnDisconnect(MyId);
			}
		}

		public void SendMessage(string message)
		{
			// queue it...
			TransmitLock.AcquireWriterLock(-1);
			try
			{
				TransmitQueue.Enqueue(message);
			}
			catch {}
			finally { TransmitLock.ReleaseWriterLock(); }

			// signal that data was sent
			DataReady.Set();
		}

		public void ReceiveThreadEntryPoint(object state)
		{
			try
			{
				// loop...
				while( true )
				{
					WaitHandle[] handles = new WaitHandle[1];
					handles[0] = StopEvent;

					if ( _Socket != null && _Socket.Connected )
					{
						// not disconnected
						try
						{
							// start the recieve operation
							System.IAsyncResult iar;
							SocketStateObject so2 = new SocketStateObject(_Socket);
							iar = _Socket.BeginReceive(so2.buffer, 0, SocketStateObject.BUFFER_SIZE, 0, new AsyncCallback(AsynchReadCallback), so2);

							if( WaitHandle.WaitAny(handles) == 0 )
							{
								break;
							}
						}
						catch {}
					}
				}
			}
			catch {}
		}

		public void SendThreadEntryPoint(object state)
		{
			try
			{
				Queue workQueue = new Queue();

				// loop...
				while( true )
				{
					WaitHandle[] handles = new WaitHandle[2];
					handles[0] = StopEvent;
					handles[1] = DataReady;

					if( WaitHandle.WaitAny(handles) == 0 )
					{
						break;
					}
					else if (_Socket != null && _Socket.Connected)
					{
						// not disconnected
						// go through the queue...
						TransmitLock.AcquireWriterLock(-1);
						try
						{
							workQueue.Clear();
							foreach( string message in TransmitQueue)
							{
								workQueue.Enqueue(message);
							}
							TransmitQueue.Clear();
						}
						catch {}
						finally 
						{
							TransmitLock.ReleaseWriterLock();
						}

						// loop the outbound messages...
						foreach( string message in workQueue )
						{
							SocketStateObject so2 = new SocketStateObject(_Socket);
							byte[] buff = Encoding.ASCII.GetBytes(message);

							// send it...
							System.IAsyncResult iar;
							iar = _Socket.BeginSend(buff, 0, buff.Length, 0, new AsyncCallback(AsynchSendCallback), so2);
						}
					}
				}
			}
			catch {}
		}

		public void AsynchSendCallback(System.IAsyncResult ar)
		{
			SocketStateObject so = (SocketStateObject)ar.AsyncState;
			Socket s = so.WorkSocket;

			try
			{
				// sanity check
				if (s == null || !s.Connected) return;
				int send = s.EndSend(ar);
			}
			catch {}
		}

		private void AsynchReadCallback(System.IAsyncResult ar)
		{
			SocketStateObject so = (SocketStateObject)ar.AsyncState;
			Socket s = so.WorkSocket;

			try
			{
				// sanity check
				if (s == null || !s.Connected) return;
				int read = s.EndReceive(ar);
				if (read > 0)
				{
					string msg = Encoding.ASCII.GetString(so.buffer, 0, read);
					if (OnReceive != null)
					{
						OnReceive(MyId, msg);
					}

					// and start recieving more
					s.BeginReceive(so.buffer, 0, SocketStateObject.BUFFER_SIZE, 0, new AsyncCallback(AsynchReadCallback), so);
				}
			}
			catch {}
		}
	}
}
