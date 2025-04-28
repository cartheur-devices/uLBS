//-----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Matjazev.Tcp.Plugin.Interfaces;

namespace Matjazev.Tcp.Plugin
{
  public class BasicPluginData : IBasicPluginData
  {
    private string name;
    private string version;
    private string author;

    public BasicPluginData(string name, string version, string author)
    {
      this.name = name;
      this.version = version;
      this.author = author;
    }

    #region ITCBasicPluginData Members
    string IBasicPluginData.Name
    {
      get { return this.name; }
    }

    string IBasicPluginData.Version
    {
      get { return this.version; }
    }

    string IBasicPluginData.Author
    {
      get { return this.author; }
    }
    #endregion
  }

  public abstract class Plugin : IPlugin
  {
    private IBasicPluginData basicData = null;
    private Dictionary<string, ExecuteAction> executeFunctions = new Dictionary<string, ExecuteAction>();
    private List<string> actions = new List<string>();

    private string key(ExecuteTime executeTime, string action)
    {
      return string.Format("{0}_{1}", action, executeTime.ToString());
    }

    private void add(string action, ExecuteTime executeTime, ExecuteAction actionFunction)
    {
      if (!this.actions.Contains(action))
        this.actions.Add(action);

      action = action.ToUpper();
      string k = this.key(executeTime, action);
      this.executeFunctions[k] = actionFunction;
    }

    protected void addClientAction(string action, ExecuteAction actionFunction1, ExecuteAction actionFunction2)
    {
      this.add(action, ExecuteTime.BeforeServer, actionFunction1);
      this.add(action, ExecuteTime.AfterServer, actionFunction2);
    }

    protected void addServerAction(string action, ExecuteAction actionFunction)
    {
      this.add(action, ExecuteTime.OnServer, actionFunction);
    }

    public Plugin()
    {
      this.basicData = this.Description();
    }

    #region ITCPPlugin Members
    IBasicPluginData IPlugin.BasicData
    {
      get { return this.basicData; }
    }

    IList<string> IPlugin.Actions
    {
      get { return this.actions; }
    }

    ExecuteAction IPlugin.GetExecuteFunction(ExecuteTime executeTime, string action)
    {
      try
      {
        action = action.ToUpper();
        string k = this.key(executeTime, action);
        return this.executeFunctions[k];
      }
      catch (Exception)
      {
      }

      return null;
    }
    #endregion

    public abstract IBasicPluginData Description();
  }
}
