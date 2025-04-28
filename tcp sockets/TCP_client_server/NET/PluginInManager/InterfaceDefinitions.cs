//-----------------------------------------------------------------------
// <copyright file="InterfaceDefinitions.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Matjazev.Tcp.Plugin.Interfaces
{
  public enum ExecuteTime
  {
    BeforeServer,
    OnServer,
    AfterServer
  }

  [Flags]
  public enum SegmentType
  {
    Unknown = 0x00,
    Gzip = 0x01,
    Bin = 0x02,
    XML = 0x04
  }

  public interface IBasicPluginData
  {
    string Name 
    { 
      get; 
    }

    string Version 
    { 
      get; 
    }

    string Author 
    { 
      get; 
    }
  }

  public interface IPlugin
  {
    IBasicPluginData BasicData
    {
      get;
    }

    IList<string> Actions
    {
      get;
    }

    ExecuteAction GetExecuteFunction(ExecuteTime executeTime, string action);
  }

  public interface IAction
  {
    ISegment Segment 
    { 
      get; 
      set; 
    }

    XmlElement Job 
    { 
      get; 
      set; 
    }
  }

  public interface ISegment
  {
    int Length { get; }

    SegmentType Type { get; }

    byte[] Data { get; }

    byte[] UnCompressData { get; }

    XmlDocument XmlDocument { get; }

    void ReadFromStream(Stream stream);

    void SendToStream(Stream stream);

    bool Compress();
    
    void SaveToFile(string fileName, bool createOnly);
    
    void LoadFromFile(string fileName, bool compress);
  }

  public interface IMessage
  {
    int Version { get; }

    void ReadFromStream(Stream stream);

    void SendToStream(Stream stream);

    IMessage Execute(ExecuteTime executeTime);

    byte[] GetJobData(XmlElement xmlJob, bool uncompress);

    XmlDocument XmlDocument { get; }

    List<ISegment> Segments { get; }
  }

  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
  public sealed class TCPClientPluginAttribute : Attribute
  {
  }

  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
  public sealed class TCPServerPluginAttribute : Attribute
  {
  }

  public delegate void ExecuteAction(IAction inAction, ref IAction outAction);
}
