//-----------------------------------------------------------------------
// <copyright file="BasicFunctionalityClient.cs" company="Matjazev.NET">
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
  [TCPClientPluginAttribute]
  public class BasicFunctionalityClient : Plugin
  {
    #region Actions
    public void ExecuteReloadPluginsAfter(IAction action, ref IAction outAction)
    {
      PluginsManager.Inst.ReLoadPlugins();
      CommonUtils.AddSubElement(outAction.Job, "value", "client", "OK");
    }

    private void ExecuteGetPluginsDataAfter(IAction action, ref IAction outAction)
    {
      CommonUtils.PluginDataToXML(outAction.Job, false);
      CommonUtils.AddSubElement(outAction.Job, "value", "client", "OK");
    }
    #endregion

    public override IBasicPluginData Description()
    {
      return new BasicPluginData("Basic Client Functionality", "1.0 beta", "Matjaž Prtenjak");
    }

    public BasicFunctionalityClient()
    {
      addClientAction("getPluginsData", Action.NoAction, this.ExecuteGetPluginsDataAfter);
      addClientAction("reloadPlugins", Action.NoAction, this.ExecuteReloadPluginsAfter);
    }
  }
}
