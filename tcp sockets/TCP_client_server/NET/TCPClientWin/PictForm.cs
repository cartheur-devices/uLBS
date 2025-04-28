//-----------------------------------------------------------------------
// <copyright file="PictForm.cs" company="Matjazev.NET">
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
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Matjazev.Tcp.Plugin;
using Matjazev.Tcp.Plugin.Interfaces;
using SimpleICSharp;

namespace Matjazev.Tcp
{
  public partial class PictForm : Form
  {
    private struct LastMouseClick
    {
      private int x, y;

      public int Y
      {
        get { return this.y; }
        set { this.y = value; }
      }

      public int X
      {
        get { return this.x; }
        set { this.x = value; }
      }

      private int duration;

      public int Duration
      {
        get { return this.duration; }
        set { this.duration = value; }
      }

      private bool dblClick;

      public bool DblClick
      {
        get { return this.dblClick; }
        set { this.dblClick = value; }
      }

      public void Set(int x, int y, int duration, bool dblClick)
      {
        this.x = x;
        this.y = y;
        this.duration = duration;
        this.dblClick = dblClick;
      }
    }

    private bool first = true;
    private string ip;
    private DateTime mouseDownTime;
    private LastMouseClick lastMS;

    public PictForm()
    {
      this.InitializeComponent();
    }

    private void ExecuteMouseClick(LastMouseClick lms)
    {
      int cur_x = lms.X * 65535 / this.pictureBox.Width;
      int cur_y = lms.Y * 65535 / this.pictureBox.Height;

      string job = string.Format("<job action='mouseClick' x='{0}' y='{1}' duration='{2}' dblClick='{3}' />",
        cur_x, cur_y, lms.Duration, (lms.DblClick ? "1" : "0"));
      job = string.Format(@"<?xml version='1.0' encoding='utf-8'?><jobs>{0}</jobs>", job);
      Utils.ExecuteWithMessageBox(this.ip, 15555, job, LogDir.Instance.Get);

      this.GetPicture(this.ip, false);

      this.lblWork.BackColor = Color.Lime;
      this.lblWork.Refresh();
    }

    public void GetPicture(string ip)
    {
      this.GetPicture(ip, this.first);
      this.first = false;
    }

    public void GetPicture(string ip, bool resize)
    {
      this.ip = ip;

      string jobName = "pict";
      Matjazev.Tcp.Plugin.Interfaces.IMessage msg = ClientUtils.GetPicture(ip, 15555, jobName, LogDir.Instance.Get);
      XmlDocument xmlDoc = msg.XmlDocument;
      if (xmlDoc == null) return;

      XmlElement job = ClientUtils.GetJob(xmlDoc, jobName);
      if (job != null)
      {
        if (Utils.IsErrorJob(job, true)) return;
        byte[] pict = msg.GetJobData(job, true);
        if (pict == null) return;

        MemoryStream stmBLOBData = new MemoryStream(pict);
        this.pictureBox.Image = Image.FromStream(stmBLOBData);
        this.pictureBox.Width = this.pictureBox.Image.Width;
        this.pictureBox.Height = this.pictureBox.Image.Height;

        if (resize)
        {
          this.ClientSize = new Size(this.pictureBox.Image.Width + this.panel2.Location.X + 20,
                                     this.pictureBox.Image.Height + this.panel2.Location.Y + 20);
        }
      }

      this.Show();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.GetPicture(this.ip, false);
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.lblWork.BackColor = Color.Red;
      this.lblWork.Refresh();

      if (this.tbSendKeys.Text.Length == 0) return;
      string job = string.Format("<job action='sendKeys' keys='{0}' />", this.tbSendKeys.Text);
      job = string.Format(@"<?xml version='1.0' encoding='utf-8'?><jobs>{0}</jobs>", job);
      Utils.ExecuteWithMessageBox(this.ip, 15555, job, LogDir.Instance.Get);

      this.GetPicture(this.ip, false);

      this.lblWork.BackColor = Color.Lime;
      this.lblWork.Refresh();
    }

    private void pictureBox_MouseDown(object sender, MouseEventArgs e)
    {
      this.lblWork.BackColor = Color.Red;
      this.lblWork.Refresh();
      this.mouseDownTime = DateTime.Now;
    }

    private void pictureBox_MouseClick(object sender, MouseEventArgs e)
    {
      TimeSpan ts = DateTime.Now - this.mouseDownTime;
      int duration = (int)Math.Round(ts.TotalMilliseconds, 0);

      this.lastMS.Set(e.X, e.Y, duration, false);

      this.mouseTimer.Enabled = true;
    }

    private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      this.mouseTimer.Enabled = false;
      this.lastMS.Set(e.X, e.Y, 0, true);
      this.ExecuteMouseClick(this.lastMS);
    }

    private void mouseTimer_Tick(object sender, EventArgs e)
    {
      this.mouseTimer.Enabled = false;
      this.ExecuteMouseClick(this.lastMS);    
    }
  }
}