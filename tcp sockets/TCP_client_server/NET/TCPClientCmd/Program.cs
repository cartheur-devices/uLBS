//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using Matjazev.Tcp;
using Matjazev.Tcp.Plugin;

namespace TCPClientCmd
{
  public class Program
  {
    private static void help()
    {
      Console.WriteLine(string.Format(
@"USAGE: {0} <server IP> <xml file with actions to execute> [result XML file] [1 if you wand log files]",
Environment.GetCommandLineArgs()[0]));
    }

    public static void Main(string[] args)
    {
      PerformanceTimer pTimer = new PerformanceTimer();
      try
      {
        if (args.Length < 2)
        {
          help();
          return;
        }

        string programPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        PluginsManager.Inst.LoadPlugins(programPath);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(args[1]);

        string log = string.Empty;
        if (args.Length > 3)
        {
          log = Path.Combine(programPath, @"log");
          if (!Directory.Exists(log)) Directory.CreateDirectory(log);
        }

        XmlDocument xmlNewDoc = JobExecuter.Execute(args[0], 15555, new Message(xmlDoc), log).XmlDocument;

        if (args.Length > 2)
        {
          using (XmlTextWriter writer = new XmlTextWriter(args[2], null))
          {
            writer.Formatting = Formatting.Indented;
            xmlNewDoc.Save(writer);
          }
        }
        else
          Console.WriteLine(xmlNewDoc.OuterXml.ToString());
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);        
      }

      Console.WriteLine("WorkTime: " + pTimer.Stop().ToString());
    }
  }
}
