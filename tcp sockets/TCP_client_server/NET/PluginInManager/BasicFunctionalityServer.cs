//-----------------------------------------------------------------------
// <copyright file="BasicFunctionalityServer.cs" company="Matjazev.NET">
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
  [TCPServerPluginAttribute]
  public class BasicFunctionalityServer : Plugin
  {
    #region Actions
    public void ExecuteReloadPlugins(IAction action, ref IAction outAction)
    {
      PluginsManager.Inst.ReLoadPlugins();
      CommonUtils.AddSubElement(outAction.Job, "value", "server", "OK");
    }

    public void ExecuteGetPluginsData(IAction action, ref IAction outAction)
    {
      CommonUtils.PluginDataToXML(outAction.Job, true);
      CommonUtils.AddSubElement(outAction.Job, "value", "server", "OK");
    }
    #endregion

    public override IBasicPluginData Description()
    {
      return new BasicPluginData("Basic Server Functionality", "1.0 beta", "Matjaž Prtenjak");
    }

    public BasicFunctionalityServer()
    {
      addServerAction("getPluginsData", this.ExecuteGetPluginsData);
      addServerAction("reloadPlugins", this.ExecuteReloadPlugins);
    }
  }
}
