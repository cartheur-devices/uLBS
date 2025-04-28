//-----------------------------------------------------------------------
// <copyright file="JobExecuter.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using Matjazev.Tcp.Plugin;
using Matjazev.Tcp.Plugin.Interfaces;

namespace Matjazev.Tcp
{
  public class JobExecuter
  {
    internal class Connection : IDisposable
    {
      private static Connection inst = null;

      private TcpClient client = null;
      private NetworkStream stream = null;
      private string ip = null;
      private int port = 0;

      protected Connection(string ip, int port)
      {
        this.ip = ip;
        this.port = port;

        this.client = new TcpClient(ip, port);
        this.stream = this.client.GetStream();
      }

      public static Connection Inst(string ip, int port)
      {
        if (inst == null) 
          inst = new Connection(ip, port);

        if ((inst.ip != ip) || (inst.port != port)) 
          inst = new Connection(ip, port);

        return inst;
      }

      internal bool TryReconect()
      {
        try
        {
          inst = new Connection(this.ip, this.port);
          return true;
        }
        catch (Exception)
        {
        }

        return false;
      }

      public NetworkStream Stream
      { 
        get 
        { 
          return this.stream; 
        } 
      }

      #region IDisposable Members
      void IDisposable.Dispose()
      {
        if (this.stream != null) this.stream.Close();
        if (this.client != null) this.client.Close();
        System.Diagnostics.Debug.WriteLine("Client has gone.");
      }
      #endregion
    }

    private static IMessage ExecuteTCP(string ip, int port, IMessage message)
    {
      try
      {
        NetworkStream stream = Connection.Inst(ip, port).Stream;

        message.SendToStream(stream);
        return new Message(stream);
      }
      catch (Exception ex)
      {
        if (ex.InnerException is SocketException)
        {
          SocketException socEx = ex.InnerException as SocketException;
          if (socEx.ErrorCode == 10053)
          {
            if (Connection.Inst(ip, port).TryReconect())
              return ExecuteTCP(ip, port, message);
          }
        }

        return Message.ErrorMessage(ex.Message);
      }
    }

    private static void doLog(string logDir, XmlDocument xmlDoc, string fname)
    {
      string name = string.Format(@"{0}\{1}_{2}", logDir, DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss"), fname);
      xmlDoc.Save(string.Format(name));
    }

    public static IMessage Execute(string ip, int port, Message jobs)
    {
      return Execute(ip, port, jobs, string.Empty);
    }

    public static IMessage Execute(string ip, int port, IMessage jobs, string logDir)
    {
      if (logDir != string.Empty) doLog(logDir, jobs.XmlDocument, "A1_Command");
      IMessage first = jobs.Execute(ExecuteTime.BeforeServer);
      if (logDir != string.Empty) doLog(logDir, first.XmlDocument, "A2_BeforeExecute");
      IMessage second = ExecuteTCP(ip, port, first);
      if (logDir != string.Empty) doLog(logDir, second.XmlDocument, "A3_Execute");
      IMessage third = second.Execute(ExecuteTime.AfterServer);
      if (logDir != string.Empty) doLog(logDir, third.XmlDocument, "A4_AfterExecute");

      return third;
    }
  }
}
