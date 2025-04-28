/*
    Copyright (c) 2009, FindWhere

    All rights reserved.

    This composite work is copyrighted in its entirety. Permission to use this material may be granted only after we receive a written request from you, and you receive written permission from us.
*/
using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using OpenNETCF.Net;

namespace MobileShared
{
    /// <summary>
    /// Shared functionality concering UDP.
    /// </summary>
    public class UDP
    {
        ConnectionManager ConnMan;

        public UDP()
        {
            ConnMan = new ConnectionManager();
        }

        /// <summary>
        /// Send text message to server.
        /// </summary>
        /// <returns>
        /// -1 There's no data to be sent
        /// -2 Socket exception
        /// -3 Other exception
        /// </returns>
        public string SendMessage(string DataToSend, string Server, int Port, int Timeout)
        {
            string returnData = "";

            RunUDPTest(DataToSend);

            // Cancel if there's no data to be sent
            if (DataToSend.Length == 0)
                return "-1";

            try
            {
                // Blocks till connected
                DoConnect(20000);
                
                IPEndPoint EP = new IPEndPoint(Dns.GetHostEntry(Server).AddressList[0], Port);
                 
                // This constructor arbitrarily assigns the local port number.
                UDPClientEx udpClient = new UDPClientEx();
                udpClient.Timeout = Timeout;
                udpClient.Connect(Server, Port);

                // Sends a message to the host to which you have connected.
                Byte[] sendBytes = Encoding.ASCII.GetBytes(DataToSend);
                udpClient.Send(sendBytes, sendBytes.Length);

                IPEndPoint E = new IPEndPoint(Dns.GetHostEntry(Server).AddressList[0], 0);

                // Waits for X timeout seconds until a message returns on this socket from a remote host.
                Byte[] receiveBytes = udpClient.DoRecieve(ref E);
                returnData = Encoding.ASCII.GetString(receiveBytes, 0, receiveBytes.Length);
                //if (returnData.Length > 0)
                //    System.Windows.Forms.MessageBox.Show("Responce =" + returnData);
                udpClient.Close();
                udpClient = null;
            }
            catch(SocketException ex)
            {
                returnData = "-2";
            }
            catch (Exception ex)
            {
                returnData = "-3";
            }

            return returnData;
        }

        public void RunUDPTest(string data)
        {
            UDPClientEx udpClient = new UDPClientEx();
            string returnData = "";
            string Server = "92.65.62.41";
            Byte[] inputToBeSent = new Byte[256];

            inputToBeSent = Encoding.ASCII.GetBytes(data);
            udpClient.Connect(IPAddress.Parse("92.65.62.41"), 48889);
            IPEndPoint E = new IPEndPoint(Dns.GetHostEntry(Server).AddressList[0], 0);

            // Waits for X timeout seconds until a message returns on this socket from a remote host.
            Byte[] receiveBytes = udpClient.DoRecieve(ref E);
            returnData = Encoding.ASCII.GetString(receiveBytes, 0, receiveBytes.Length);
            if (returnData.Length > 0)
                System.Windows.Forms.MessageBox.Show("Response =" + returnData);

            //int nBytesSent = udpClient.Send(inputToBeSent, inputToBeSent.Length);

            udpClient.Close();
            udpClient = null;

            DoTcpConnection();
        }

        public void DoTcpConnection()
        {
            string url = "92.65.62.41:48889";
            bool res = GPRSConnection.Setup(url);
            if (res)
            {
                TcpClient tc = new TcpClient(url, 80);
                NetworkStream ns = tc.GetStream();
                byte[] buf = new byte[100];
                ns.Write(buf, 0, 100);
                tc.Client.Shutdown(SocketShutdown.Both);
                ns.Close();
                tc.Close();
                System.Windows.Forms.MessageBox.Show("Wrote 100 bytes");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Connection establishment failed");
            }
        }

        public void Disconnect()
        {
            if (Configuration.Instance().DisconnectAfterPlot)
                if (ConnMan.Status == ConnectionStatus.Connected)
                    ConnMan.RequestDisconnect();
        }

        public void DoConnect(int Timeout)
        {
            bool blnIsSucess = false;
            int connAttemptsCount = 0;
            //for (connAttemptsCount = 0; connAttemptsCount < 3; connAttemptsCount++)
            {
                //try
                {
                    ConnMan.Timeout = (uint)Timeout;
                    if (ConnMan.Status == ConnectionStatus.Disconnected)
                        ConnMan.Connect(true, ConnectionMode.Synchronous);
                    //blnIsSucess = true;
                    //break;
                }
                //catch (Exception ex)
                //{
                //    throw ex;
                //} 
            }
            //if (blnIsSucess == false && connAttemptsCount > 2)
            //{
            //    throw new Exception("Unable to connect");
            //}
        }
    }
}
