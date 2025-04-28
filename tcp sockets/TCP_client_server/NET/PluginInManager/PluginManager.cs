//-----------------------------------------------------------------------
// <copyright file="PluginManager.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Matjazev.Tcp.Plugin.Interfaces;

namespace Matjazev.Tcp.Plugin
{
  public class PluginsManager
  {
    private static PluginsManager inst = null;

    private List<PluginData> serverPlugins = new List<PluginData>();
    private List<PluginData> clientPlugins = new List<PluginData>();
    private Dictionary<ExecuteTime, Dictionary<string, ExecuteAction>> functions = new Dictionary<ExecuteTime, Dictionary<string, ExecuteAction>>();
    private string programPath = string.Empty;

    private PluginData getPlugin(Dictionary<string, PluginData> dict, string action)
    {
      if (dict.ContainsKey(action))
        return dict[action];

      return null;
    }

    protected PluginsManager()
    {
    }

    private bool addAssembly(string pluginFile, Assembly assembly)
    {
      bool found = false;
      foreach (Type type in assembly.GetTypes())
      {
        if (type.IsAbstract) continue;

        bool server = type.IsDefined(typeof(TCPServerPluginAttribute), true);
        bool client = type.IsDefined(typeof(TCPClientPluginAttribute), true);

        if (server || client)
        {
          PluginData data = new PluginData(Activator.CreateInstance(type) as IPlugin, pluginFile);
          if (server) 
            this.serverPlugins.Add(data);
          else
            this.clientPlugins.Add(data);

          foreach (string action in data.Plugin.Actions)
          {
            string uAction = action.ToUpper();
            if (server)
              this.functions[ExecuteTime.OnServer][uAction] = data.Plugin.GetExecuteFunction(ExecuteTime.OnServer, uAction);
            else
            {
              this.functions[ExecuteTime.BeforeServer][uAction] = data.Plugin.GetExecuteFunction(ExecuteTime.BeforeServer, uAction);
              this.functions[ExecuteTime.AfterServer][uAction] = data.Plugin.GetExecuteFunction(ExecuteTime.AfterServer, uAction);
            }
          }

          found = true;
        }
      }

      return found;
    }

    private bool addPlugin(string pluginFile)
    {
      if (!File.Exists(pluginFile))
        return false;

      Assembly assembly = Assembly.LoadFrom(pluginFile);
      if (assembly == null)
        return false;

      return this.addAssembly(pluginFile, assembly);
    }

    public static PluginsManager Inst
    {
      get
      {
        if (inst == null)
          inst = new PluginsManager();

        return inst;
      }
    }

    public void ReLoadPlugins()
    {
      this.LoadPlugins(this.programPath);
    }

    public void LoadPlugins(string programPath)
    {
      this.programPath = programPath;

      string pluginsPath = Path.Combine(programPath, @"PlugIns");
      pluginsPath = Path.GetFullPath(pluginsPath);
      if (!Directory.Exists(pluginsPath))
      {
        pluginsPath = Path.Combine(programPath, @"..\..\PlugIns");
        pluginsPath = Path.GetFullPath(pluginsPath);
      }

      this.Clear();

      this.addAssembly(string.Empty, Assembly.GetExecutingAssembly());
      if (!Directory.Exists(pluginsPath)) return;
      foreach (string f in Directory.GetFiles(pluginsPath))
      {
        FileInfo fi = new FileInfo(f);

        if (fi.Extension.Equals(".dll"))
          this.addPlugin(f);
      }
    }

    public void Clear()
    {
      this.serverPlugins.Clear();
      this.clientPlugins.Clear();

      this.functions.Clear();
      this.functions[ExecuteTime.BeforeServer] = new Dictionary<string, ExecuteAction>();
      this.functions[ExecuteTime.OnServer] = new Dictionary<string, ExecuteAction>();
      this.functions[ExecuteTime.AfterServer] = new Dictionary<string, ExecuteAction>();
    }

    public IList<PluginData> ServerPlugins
    {
      get { return this.serverPlugins; }
    }

    public IList<PluginData> ClientPlugins
    {
      get { return this.clientPlugins; }
    }

    public ExecuteAction GetExecuteFunction(ExecuteTime executeTime, string action)
    {
      try
      {
        return this.functions[executeTime][action];
      }
      catch (Exception)
      {
      }

      return null;
    }
  }
}
