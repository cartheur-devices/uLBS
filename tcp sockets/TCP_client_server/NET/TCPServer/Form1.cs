//-----------------------------------------------------------------------
// <copyright file="Form1.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Matjazev.Tcp.Plugin;

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
      this.lblInfo.Text = (working) ? "Server running" : "Server stopped";
      this.lblIP.Visible = working;
    }

    public Form1()
    {
      this.InitializeComponent();

      string programPath = Path.GetDirectoryName(Application.ExecutablePath);
      this.tcpServer = new Matjazev.Tcp.TcpServer(programPath);

      this.tcpServer.StatusChanged += this.showLabel;
      this.showLabel(this.tcpServer, null);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.tcpServer.Start(15555);

      int cntr = 0;
      IList<string> ips = CommonUtils.GetAllPublicIPs();
      foreach (string ip in ips)
        this.ipDescriptions.Add(string.Format("IP ({0}/{1}): {2}", ++cntr, ips.Count, ip));

      this.ipDescriptionPos = -1;
      this.lblIP_Click(this, e);
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.tcpServer.Stop();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.tcpServer.IsWorking) this.tcpServer.Stop();
    }

    private void lblIP_Click(object sender, EventArgs e)
    {
      this.ipDescriptionPos++;
      if (this.ipDescriptionPos >= this.ipDescriptions.Count) this.ipDescriptionPos = 0;
      this.lblIP.Text = this.ipDescriptions[this.ipDescriptionPos];
    }
  }
}