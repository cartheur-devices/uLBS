//-----------------------------------------------------------------------
// <copyright file="ClientUtils.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using Matjazev.Tcp.Plugin;
using Matjazev.Tcp.Plugin.Interfaces;

namespace Matjazev.Tcp
{
  public class ClientUtils
  {
    public static XmlElement GetJob(XmlDocument xmlDoc, string jobID)
    {
      XmlElement job = (xmlDoc.SelectSingleNode(String.Format(@"//job[@id='{0}']", jobID)) as XmlElement);
      return job;
    }

    public static XmlDocument GetDirData(string ip, int port, string dir, string jobName, string logDir)
    {
      string job = string.Format(@"<?xml version='1.0' encoding='utf-8'?>
                                     <jobs><job id='{0}' action='dir' filePattern='*.*' folder='{1}' /></jobs>", jobName, dir);

      XmlDocument xmlJob = new XmlDocument();
      xmlJob.LoadXml(job);
      return JobExecuter.Execute(ip, port, new Message(xmlJob), logDir).XmlDocument;
    }

    public static IMessage GetPicture(string ip, int port, string jobName, string logDir)
    {
      string job = string.Format(@"<?xml version='1.0' encoding='utf-8'?>
                                   <jobs><job id='{0}' action='capture' format='gif' /></jobs>", jobName);

      XmlDocument xmlJob = new XmlDocument();
      xmlJob.LoadXml(job);
      return JobExecuter.Execute(ip, port, new Message(xmlJob), logDir);
    }
  }
}
