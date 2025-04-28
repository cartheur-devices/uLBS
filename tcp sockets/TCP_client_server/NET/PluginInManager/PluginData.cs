//-----------------------------------------------------------------------
// <copyright file="PluginData.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using Matjazev.Tcp.Plugin.Interfaces;

namespace Matjazev.Tcp.Plugin
{
  public class PluginData
  {
    private IPlugin plugin;
    private string path;

    public PluginData(IPlugin plugin, string path)
    {
      this.plugin = plugin;
      this.path = path;
    }

    public IPlugin Plugin
    {
      get { return this.plugin; }
    }

    public string Path
    {
      get { return this.path; }
    }
  }
}
