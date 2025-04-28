using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;

namespace TestSocketCliet
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cmdConnect;
		private System.Windows.Forms.TextBox tbHostAddress;
		private System.Windows.Forms.TextBox tbHostPort;
		private System.Windows.Forms.TextBox tbOutput;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private AsynchSocketLib.AsynchSocketManager socketMgr;
		private bool IsConnected = false;

		private static int _mainThreadId;
		private System.Windows.Forms.Button cmdDisconnect;
		private System.Windows.Forms.TextBox tbSend;
		private System.Windows.Forms.Button cmdSend;

		// delegates for the socket manager
		protected delegate void HandleInitiateConnectionDelegate(int id, bool bSuccess);
		protected delegate void HandleDisconnectDelegate(int id);
		protected delegate void HandleInputDelegate(int id, string msg);

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
	
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			_mainThreadId = Thread.CurrentThread.GetHashCode();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cmdConnect = new System.Windows.Forms.Button();
			this.tbHostAddress = new System.Windows.Forms.TextBox();
			this.tbHostPort = new System.Windows.Forms.TextBox();
			this.tbOutput = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.cmdDisconnect = new System.Windows.Forms.Button();
			this.tbSend = new System.Windows.Forms.TextBox();
			this.cmdSend = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cmdConnect
			// 
			this.cmdConnect.Location = new System.Drawing.Point(208, 62);
			this.cmdConnect.Name = "cmdConnect";
			this.cmdConnect.TabIndex = 0;
			this.cmdConnect.Text = "Connect";
			this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
			// 
			// tbHostAddress
			// 
			this.tbHostAddress.Location = new System.Drawing.Point(96, 24);
			this.tbHostAddress.Name = "tbHostAddress";
			this.tbHostAddress.Size = new System.Drawing.Size(400, 20);
			this.tbHostAddress.TabIndex = 1;
			this.tbHostAddress.Text = "";
			// 
			// tbHostPort
			// 
			this.tbHostPort.Location = new System.Drawing.Point(96, 65);
			this.tbHostPort.Name = "tbHostPort";
			this.tbHostPort.TabIndex = 2;
			this.tbHostPort.Text = "";
			this.tbHostPort.Validating += new System.ComponentModel.CancelEventHandler(this.tbHostPort_Validating);
			this.tbHostPort.Validated += new System.EventHandler(this.tbHostPort_Validated);
			// 
			// tbOutput
			// 
			this.tbOutput.Location = new System.Drawing.Point(8, 104);
			this.tbOutput.Multiline = true;
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.ReadOnly = true;
			this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbOutput.Size = new System.Drawing.Size(504, 360);
			this.tbOutput.TabIndex = 3;
			this.tbOutput.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Host Address:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Host Port:";
			// 
			// cmdDisconnect
			// 
			this.cmdDisconnect.Enabled = false;
			this.cmdDisconnect.Location = new System.Drawing.Point(296, 62);
			this.cmdDisconnect.Name = "cmdDisconnect";
			this.cmdDisconnect.TabIndex = 6;
			this.cmdDisconnect.Text = "Disconnect";
			this.cmdDisconnect.Click += new System.EventHandler(this.cmdDisconnect_Click);
			// 
			// tbSend
			// 
			this.tbSend.Enabled = false;
			this.tbSend.Location = new System.Drawing.Point(8, 472);
			this.tbSend.Name = "tbSend";
			this.tbSend.Size = new System.Drawing.Size(424, 20);
			this.tbSend.TabIndex = 7;
			this.tbSend.Text = "";
			// 
			// cmdSend
			// 
			this.cmdSend.Enabled = false;
			this.cmdSend.Location = new System.Drawing.Point(437, 471);
			this.cmdSend.Name = "cmdSend";
			this.cmdSend.TabIndex = 8;
			this.cmdSend.Text = "Send";
			this.cmdSend.Click += new System.EventHandler(this.cmdSend_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(520, 501);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cmdSend,
																		  this.tbSend,
																		  this.cmdDisconnect,
																		  this.label2,
																		  this.label1,
																		  this.tbOutput,
																		  this.tbHostPort,
																		  this.tbHostAddress,
																		  this.cmdConnect});
			this.Name = "Form1";
			this.Text = "Socket Test";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		public static bool IsMainThread()
		{
			return ( Thread.CurrentThread.GetHashCode() == _mainThreadId ? true : false );
		}

		private void MyHostAddressValidate()
		{
		}

		private void MyHostPortValidate()
		{
			// Confirm there is text in the control.
			if (tbHostPort.Text.Length == 0)
			{
				throw new Exception("Host Port is a required field");
			}
				// Confirm that it is numeric
			else
			{
				try
				{
					System.Convert.ToInt32( tbHostPort.Text );
				}
				catch( Exception ex )
				{
					throw new Exception("Host Port must be numeric");
				}
			}
		}

		private void tbHostPort_Validating(object sender, 
			System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				MyHostPortValidate();
			}
			catch( Exception ex )
			{
				// Cancel the event and select the text to be corrected by the user.
				e.Cancel = true;
				tbHostPort.Select(0, tbHostPort.Text.Length);

				// Set the ErrorProvider error with the text to display. 
				this.errorProvider1.SetError(tbHostPort, ex.Message);
			}

		}

		private void tbHostPort_Validated(object sender, System.EventArgs e)
		{
			// If all conditions have been met, clear the ErrorProvider of errors.
			errorProvider1.SetError(tbHostPort, "");
		}

		private void cmdConnect_Click(object sender, System.EventArgs e)
		{

			if( errorProvider1.GetError(tbHostPort) != string.Empty )
			{
				MessageBox.Show( "Please correct errors before continuing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
				tbHostPort.Focus();
			}

			System.Threading.Thread thread = new System.Threading.Thread( new ThreadStart(ThreadFunction) );
			thread.Start();
		}

		protected void HandleInitiateConnection(int id, bool bSuccess)
		{
			if( !IsMainThread() )
			{
				object [] args = new object[2];

				args[0] = id;
				args[1] = bSuccess;
				try
				{
					Invoke( new HandleInitiateConnectionDelegate(HandleInitiateConnection), args );
				}
				catch( Exception ex )
				{}
			}
			else
			{
				if( !bSuccess )
				{
					//					MessageBox.Show( "Connected!" );
					//				}
					//				else
					//				{
					cmdConnect.Enabled = true;
					MessageBox.Show( "Failed to connect!" );
				}
				else
				{
					cmdDisconnect.Enabled = true;
					cmdSend.Enabled = true;
					tbSend.Enabled = true;
				}
			}
		}

		protected void HandleDisconnect(int id)
		{
			if( !IsMainThread() )
			{
				object [] args = new object[1];

				args[0] = id;
				try
				{
					Invoke( new HandleDisconnectDelegate(HandleDisconnect), args );
				}
				catch( Exception ex )
				{}
			}
			else
			{
//				MessageBox.Show( "Disconnected!" );
				cmdConnect.Enabled = true;
				cmdDisconnect.Enabled = false;

				cmdSend.Enabled = false;
				tbSend.Enabled = false;
			}
		}

		protected void HandleInput(int id, string msg)
		{
			if( !IsMainThread() )
			{
				object [] args = new object[2];

				args[0] = id;
				args[1] = msg;
				Invoke( new HandleInputDelegate(HandleInput), args );
			}
			else
			{
				tbOutput.Text += msg;
				tbOutput.SelectionStart = tbOutput.TextLength;
			}
		}

		private void ThreadFunction()
		{
			socketMgr = new AsynchSocketLib.AsynchSocketManager( new AsynchSocketLib.AsynchSocketManager.OnConnectDelegate(HandleInitiateConnection), new AsynchSocketLib.AsynchSocketManager.OnDisconnectDelegate(HandleDisconnect), new AsynchSocketLib.AsynchSocketManager.OnReceiveDelegate(HandleInput) );
            //create thread locking in the spirit of the plotting calculator when working on forex
			// and start it up
			cmdConnect.Enabled = false;
			socketMgr.Connect( tbHostAddress.Text, Convert.ToInt32(tbHostPort.Text) );
		}

		private void richTextBox1_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void cmdDisconnect_Click(object sender, System.EventArgs e)
		{
			socketMgr.Disconnect();
		}

		private void cmdSend_Click(object sender, System.EventArgs e)
		{
			socketMgr.SendMessage( tbSend.Text + "\n" );
			tbOutput.Text += "-->" + tbSend.Text;
			tbOutput.Text += "\r\n";
		}
	}
}
