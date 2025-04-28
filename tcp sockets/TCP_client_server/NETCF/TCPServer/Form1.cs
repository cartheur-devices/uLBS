//-----------------------------------------------------------------------
// <copyright file="Form1.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace TCPServer
{
  public partial class Form1 : Form
  {
    private Matjazev.Tcp.TcpServer tcpServer = null;
    private List<string> ipDescriptions = new List<string>();
    private int ipDescriptionPos = 0;

    private void showLabel(object sender, EventArgs e)
    {
      bool working = (this.tcpServer != null) && (this.tcpServer.IsWorking);
      lblInfo.Text = (working) ? "Server running" : "Server stopped";
      lblIP.Visible = working;
    }

    public Form1()
    {
      InitializeComponent();
      string programPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
      this.tcpServer = new Matjazev.Tcp.TcpServer(programPath);

      this.tcpServer.StatusChanged += this.showLabel;
      this.showLabel(this.tcpServer, null);
    }

    private void lblInfo_Click(object sender, EventArgs e)
    {
      this.ipDescriptionPos++;
      if (this.ipDescriptionPos >= this.ipDescriptions.Count) this.ipDescriptionPos = 0;
      lblIP.Text = this.ipDescriptions[this.ipDescriptionPos];
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.tcpServer.Start(15555);

      IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
      int cntr = 0;
      foreach (IPAddress addr in host.AddressList)
        this.ipDescriptions.Add(string.Format("IP {2} ({0}/{1})", ++cntr, host.AddressList.Length, addr.ToString()));

      this.ipDescriptionPos = -1;
      this.lblInfo_Click(this, e);
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.tcpServer.Stop();
      Close();
    }

    private void Form1_Closing(object sender, CancelEventArgs e)
    {
      if (this.tcpServer.IsWorking) this.tcpServer.Stop();
    }
  }
}