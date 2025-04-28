//-----------------------------------------------------------------------
// <copyright file="LogDir.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Matjazev.Tcp
{
  public class LogDir
  {
    private static LogDir inst = null;

    private string path;
    private bool doLog;

    protected LogDir()
    {
      this.path = Path.GetDirectoryName(Application.ExecutablePath);
      this.path = Path.Combine(this.path, @"Log");
      this.path = Path.GetFullPath(this.path);

      if (!Directory.Exists(this.path))
        Directory.CreateDirectory(this.path);

      this.doLog = false;
    }

    public static LogDir Instance
    {
      get
      {
        if (inst == null)
          inst = new LogDir();

        return inst;
      }
    }

    public string Get
    {
      get { return (Instance.doLog) ? Instance.path : string.Empty; }
    }

    public bool DoLog
    {
      get { return Instance.doLog; }
      set { Instance.doLog = value; }
    }
  }
}
