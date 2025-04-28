//-----------------------------------------------------------------------
// <copyright file="TcpServer.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Matjazev.Tcp.Plugin;
using Matjazev.Tcp.Plugin.Interfaces;

namespace Matjazev.Tcp
{
  internal class TcpServer
  {
    private TcpListener tcpListener = null;
    private Thread listenThread = null;
    private bool stopWorking = true;
    private string programPath;

    public delegate void StatusChangedEvent(object sender, EventArgs e);

    public event StatusChangedEvent StatusChanged;

    private bool doEndConnection(object sender, EventArgs e)
    {
      return this.stopWorking;
    }

    private void ListenForClients()
    {
      this.tcpListener.Start();

      try
      {
        while (true && !this.stopWorking)
        {
          TcpClient client = this.tcpListener.AcceptTcpClient();

          JobExecuter executer = new JobExecuter(this, client, this.doEndConnection);
          Thread clientThread = new Thread(new ThreadStart(executer.HandleClient));
          clientThread.Start();
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
      }
    }

    public bool IsWorking
    {
      get { return !this.stopWorking; }
    }

    public TcpServer(string programPath)
    {
      this.programPath = programPath;
    }

    public string Start(int port)
    {
      if (this.stopWorking == false) return string.Empty;

      PluginsManager.Inst.LoadPlugins(this.programPath);

      IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
      string ipAddress = host.AddressList[0].ToString();

      lock (this) 
      { 
        this.stopWorking = false; 
      }

      this.tcpListener = new TcpListener(IPAddress.Any, port);
      this.listenThread = new Thread(new ThreadStart(this.ListenForClients));
      this.listenThread.Start();

      if (this.StatusChanged != null) this.StatusChanged(this, null);

      return ipAddress;
    }

    public void Stop()
    {
      if (this.stopWorking == true) return;

      lock (this) 
      { 
        this.stopWorking = true; 
      }

      try
      {
        this.tcpListener.Stop();
        this.tcpListener = null;
      }
      catch (Exception)
      {
      }

      if (this.StatusChanged != null) this.StatusChanged(this, null);
    }
  }
}
