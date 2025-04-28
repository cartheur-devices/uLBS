//-----------------------------------------------------------------------
// <copyright file="Utils.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Matjazev.Tcp.Plugin;

namespace Matjazev.Tcp
{
  internal class Utils
  {
    public static Matjazev.Tcp.Plugin.Interfaces.IMessage ExecuteWithMessageBox(string ip, int port, string strJobs, string logDir)
    {
      XmlDocument xmlDoc = new XmlDocument();
      xmlDoc.LoadXml(strJobs);
      return ExecuteWithMessageBox(ip, port, new Matjazev.Tcp.Plugin.Message(xmlDoc), logDir);
    }

    public static Matjazev.Tcp.Plugin.Interfaces.IMessage ExecuteWithMessageBox(string ip, int port, Matjazev.Tcp.Plugin.Message xmlJobs, string logDir)
    {
      try
      {
        return JobExecuter.Execute(ip, port, xmlJobs, logDir);
      }
      catch (ArgumentNullException ex)
      {
        MessageBox.Show(string.Format("ArgumentNullException: {0}", ex.Message));
      }
      catch (SocketException ex)
      {
        MessageBox.Show(string.Format("SocketException: {0}", ex.Message));
      }
      catch (Exception ex)
      {
        MessageBox.Show(string.Format("Exception: {0}", ex.Message));
      }

      return null;
    }

    public static bool IsErrorJob(XmlElement job, bool showMessage)
    {
      if (job != null)
      {
        if (CommonUtils.IsAttrSet(job, "error"))
        {
          if (showMessage) MessageBox.Show(job.InnerText.ToString());
          return true;
        }

        return false;
      }

      return true;
    }

    public static string SelectFolder()
    {
      string strCaption = "Select folder...";

      Shell32.ShellClass shl = new Shell32.ShellClass();
      Shell32.Folder2 fld = (Shell32.Folder2)shl.BrowseForFolder(0, strCaption, 0,
                  System.Reflection.Missing.Value);

      return (fld == null) ? string.Empty : fld.Self.Path;
    }
  }
}
