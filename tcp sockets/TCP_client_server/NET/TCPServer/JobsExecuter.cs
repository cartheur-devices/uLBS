//-----------------------------------------------------------------------
// <copyright file="JobsExecuter.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Net.Sockets;
using System.Xml;
using Matjazev.Tcp.Plugin;
using Matjazev.Tcp.Plugin.Interfaces;

namespace Matjazev.Tcp
{
  public delegate bool EndConnectionEvent(object sender, EventArgs e);

  internal class JobExecuter
  {
    private TcpServer server = null;
    private TcpClient client = null;
    private EndConnectionEvent endConnectionEvent = null;

    public JobExecuter(TcpServer server, TcpClient client, EndConnectionEvent endConnectionEvent)
    {
      this.server = server;
      this.client = client;
      this.endConnectionEvent = endConnectionEvent;
    }

    public void HandleClient()
    {
      System.Diagnostics.Debug.WriteLine("client start");
      NetworkStream clientStream = this.client.GetStream();

      try
      {
        try
        {
          EventArgs e = new EventArgs();
          while (!this.endConnectionEvent(this, e))
          {
            try
            {
              this.client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 5 * 1000);
              this.client.Client.Blocking = true;

              IMessage inMessage = new Message(clientStream);
              IMessage outMessage = inMessage.Execute(ExecuteTime.OnServer);
              outMessage.SendToStream(clientStream);
            }
            catch (Exception ex)
            {
              if (ex.InnerException is SocketException)
              {
                SocketException se = ex.InnerException as SocketException;
                if (se.ErrorCode != 10060)
                  throw;
              }
              else
                throw;
            }
          }
        }
        catch (Exception)
        {
        }
      }
      finally
      {
        this.client.Close();
        System.Diagnostics.Debug.WriteLine("client close");
      }
    }
  }
}
