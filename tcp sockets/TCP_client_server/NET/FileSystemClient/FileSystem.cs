//-----------------------------------------------------------------------
// <copyright file="FileSystem.cs" company="Matjazev.NET">
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
  public class FileSystem : Plugin
  {
    #region Actions
    public void ExecutePutFileBefore(IAction action, ref IAction outAction)
    {
      string inFile = CommonUtils.GetAttribute(action.Job, "inFile") ?? string.Empty;
      if (inFile.Length == 0) throw new Exception("inFile not specified");

      if (!File.Exists(inFile))
          throw new Exception(string.Format("{0} does not exists", inFile));

      bool compress = CommonUtils.IsAttrSet(action.Job, "compress");
      outAction.Segment = new Segment(inFile, compress);
    }

    public void ExecuteGetFileBefore(IAction action, ref IAction outAction)
    {
      string outFile = CommonUtils.GetAttribute(action.Job, "outFile") ?? string.Empty;
      if ((outFile.Length != 0) && (CommonUtils.IsAttrSet(action.Job, "createOnly")))
      {
        if (File.Exists(outFile))
          throw new Exception(string.Format("{0} already exists", outFile));
      }
    }

    public void ExecuteGetFileAfter(IAction action, ref IAction outAction)
    {
      string outFile = CommonUtils.GetAttribute(action.Job, "outFile") ?? string.Empty;
      if (outFile.Length != 0) 
      {
        action.Segment.SaveToFile(outFile, CommonUtils.IsAttrSet(action.Job, "createOnly"));
        action.Segment = null;
        CommonUtils.AddElement(outAction.Job, "value", "OK");
      }
    }
    #endregion

    public override IBasicPluginData Description()
    {
      return new BasicPluginData("File System Client", "1.0 beta", "Matjaž Prtenjak");
    }

    public FileSystem()
    {
      addClientAction("getFile", this.ExecuteGetFileBefore, this.ExecuteGetFileAfter);
      addClientAction("putFile", this.ExecutePutFileBefore, Action.NoAction);
      addClientAction("deleteFile", Action.NoAction, Action.NoAction);
      addClientAction("createFolder", Action.NoAction, Action.NoAction);
      addClientAction("removeFolder", Action.NoAction, Action.NoAction);
      addClientAction("dir", Action.NoAction, Action.NoAction);
    }
  }
}