//-----------------------------------------------------------------------
// <copyright file="RemoteDesktop.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Matjazev.Tcp.Plugin;
using Matjazev.Tcp.Plugin.Interfaces;

namespace Matjazev.Tcp.Plugin
{
  [TCPClientPluginAttribute]
  public class RemoteDesktop : Plugin
  {
    #region Actions
    public void ExecuteCaptureBefore(IAction action, ref IAction outAction)
    {
      string outFile = CommonUtils.GetAttribute(action.Job, "outFile") ?? string.Empty;
      if ((outFile.Length != 0) && (CommonUtils.IsAttrSet(action.Job, "createOnly")))
      {
        if (File.Exists(outFile))
          throw new Exception(string.Format("{0} exists", outFile));
      }
    }

    private void ExecuteCaptureAfter(IAction action, ref IAction outAction)
    {
      string outFile = CommonUtils.GetAttribute(action.Job, "outFile") ?? string.Empty;
      if (outFile.Length != 0)
      {
        bool createOnly = CommonUtils.IsAttrSet(action.Job, "createOnly");
        action.Segment.SaveToFile(outFile, createOnly);
        CommonUtils.AddElement(outAction.Job, "value", "OK");
      }
    }
    #endregion

    public override IBasicPluginData Description()
    {
      return new BasicPluginData("Simple Remote Desktop Client", "1.0 beta", "Matjaž Prtenjak");
    }

    public RemoteDesktop()
    {
      addClientAction("capture", this.ExecuteCaptureBefore, this.ExecuteCaptureAfter);
      addClientAction("mouseClick", Action.NoAction, Action.NoAction);
      addClientAction("sendKeys", Action.NoAction, Action.NoAction);
    }
  }
}
