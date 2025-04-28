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
  [TCPServerPluginAttribute]
  public class FileSystem : Plugin
  {
    #region Actions
    public void ExecuteDir(IAction action, ref IAction outAction)
    {
      bool foldersOnly = CommonUtils.IsAttrSet(action.Job, "foldersOnly");
      bool recursive = CommonUtils.IsAttrSet(action.Job, "recursive");
      string folder = CommonUtils.GetAttribute(action.Job, "folder") ?? string.Empty;
      string filePattern = CommonUtils.GetAttribute(action.Job, "filePattern") ?? string.Empty;

      if (folder.Length == 0) throw new Exception("Folder not specified");
      if (filePattern.Length == 0) filePattern = "*.*";

      string dir = foldersOnly ? Utils.GetDirectories(folder, recursive) : Utils.GetFiles(folder, filePattern, recursive);

      XmlDocument xmlDoc = new XmlDocument();
      xmlDoc.LoadXml(dir);

      XmlElement retValue = outAction.Job.OwnerDocument.CreateElement("value");
      retValue.AppendChild(outAction.Job.OwnerDocument.ImportNode(xmlDoc.DocumentElement, true));
      outAction.Job.AppendChild(retValue);
    }

    public void ExecuteRemoveFolder(IAction action, ref IAction outAction)
    {
      bool recursive = CommonUtils.IsAttrSet(action.Job, "recursive");
      string folder = CommonUtils.GetAttribute(action.Job, "folder") ?? string.Empty;
      if (folder.Length == 0) throw new Exception("Folder not specified");

      Directory.Delete(folder, recursive);
      if (Directory.Exists(folder))
        throw new Exception("Unknown error");

      CommonUtils.AddElement(outAction.Job, "value", "OK");
    }

    public void ExecuteCreateFolder(IAction action, ref IAction outAction)
    {
      string folder = CommonUtils.GetAttribute(action.Job, "folder") ?? string.Empty;
      if (folder.Length == 0) throw new Exception("Folder not specified");

      Directory.CreateDirectory(folder);
      if (!Directory.Exists(folder))
        throw new Exception("Unknown error");

      CommonUtils.AddElement(outAction.Job, "value", "OK");
    }

    public void ExecuteDeleteFile(IAction action, ref IAction outAction)
    {
      string file = CommonUtils.GetAttribute(action.Job, "file") ?? string.Empty;
      if (file.Length == 0) throw new Exception("File not specified");

      File.Delete(file);
      if (File.Exists(file))
        throw new Exception("Unknown error");

      CommonUtils.AddElement(outAction.Job, "value", "OK");
    }

    public void ExecutePutFile(IAction action, ref IAction outAction)
    {
      string outFile = CommonUtils.GetAttribute(action.Job, "outFile") ?? string.Empty;
      if (outFile.Length == 0) throw new Exception("outFile not specified");

      action.Segment.SaveToFile(outFile, CommonUtils.IsAttrSet(action.Job, "createOnly"));
      action.Segment = null;
      CommonUtils.AddElement(outAction.Job, "value", "OK");
    }

    public void ExecuteGetFile(IAction action, ref IAction outAction)
    {
      string inFile = CommonUtils.GetAttribute(action.Job, "inFile") ?? string.Empty;
      if (inFile.Length == 0) throw new Exception("inFile not specified");

      if (!File.Exists(inFile))
        throw new Exception(string.Format("{0} does not exists", inFile));

      bool compress = CommonUtils.IsAttrSet(action.Job, "compress");
      outAction.Segment = new Segment(inFile, compress);
    }
    #endregion

    public override IBasicPluginData Description()
    {
      return new BasicPluginData("File System Server", "1.0 beta", "Matjaž Prtenjak");
    }

    public FileSystem()
    {
      addServerAction("getFile", this.ExecuteGetFile);
      addServerAction("putFile", this.ExecutePutFile);
      addServerAction("deleteFile", this.ExecuteDeleteFile);
      addServerAction("createFolder", this.ExecuteCreateFolder);
      addServerAction("removeFolder", this.ExecuteRemoveFolder);
      addServerAction("dir", this.ExecuteDir);
    }
  }
}
