//-----------------------------------------------------------------------
// <copyright file="Message.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Matjazev.Tcp.Plugin.Interfaces;

namespace Matjazev.Tcp.Plugin
{
  public class Message : IMessage
  {
    private int version = 0;
    private XmlDocument xmlDoc = null;
    private List<ISegment> segments = null;

    #region Private Members
    #region Static Members
    private static Message createNewMessage(string errorText)
    {
      XmlDocument retDoc = new XmlDocument();
      retDoc.AppendChild(retDoc.CreateXmlDeclaration("1.0", "utf-8", string.Empty));
      XmlElement root = retDoc.CreateElement("jobs");
      if (errorText != null) CommonUtils.AddElement(root, "error", errorText);
      retDoc.AppendChild(root);

      return new Message(retDoc);
    }

    public static Message ErrorMessage(string errorText)
    {
      return createNewMessage(errorText);
    }
    #endregion

    private XmlElement getJobsElement()
    {
      XmlDocument xmlJobs = this.xmlDoc;
      XmlElement jobs = xmlJobs.DocumentElement;
      if (jobs.LocalName != "jobs")
        return null;

      return jobs;
    }

    private void executeHelper(IMessage retMsg, XmlElement xmlJobToExecute, ExecuteTime executeTime, ExecuteAction executeActionFunction)
    {
      PerformanceTimer pTimer = new PerformanceTimer();

      int? segNum = CommonUtils.GetIntAttribute(xmlJobToExecute, "segNum");
      Segment seg = (segNum.HasValue) ? (Segment)this.segments[segNum.Value] : null;

      IAction inAction = Action.CreateAction(xmlJobToExecute, seg);
      IAction outAction = Action.CreateAction(xmlJobToExecute, seg);
/*      outAction.Job = outAction.Job.OwnerDocument.ImportNode(inAction.Job, true) as XmlElement;
      if (inAction.Segment != null) outAction.Segment = inAction.Segment;*/
      executeActionFunction(inAction, ref outAction);
      if (outAction.Segment != null)
      {
        retMsg.Segments.Add(outAction.Segment);
        outAction.Job.SetAttribute("segNum", (retMsg.Segments.Count - 1).ToString());
      }
      else
        outAction.Job.RemoveAttribute("segNum");

      if (CommonUtils.IsAttrSet(outAction.Job, "info"))
      {
        string strAction = CommonUtils.GetAttribute(xmlJobToExecute, "action", true);
        CommonUtils.AddSubElement(outAction.Job, "info", executeTime.ToString() + "_duration", pTimer.Stop().ToString());
      }

      retMsg.XmlDocument.DocumentElement.AppendChild(retMsg.XmlDocument.ImportNode(outAction.Job, true) as XmlElement);
    }
    #endregion

    #region Public Members
    public Message(XmlDocument xmlDoc)
    {
      this.version = 2;
      this.xmlDoc = xmlDoc;
      this.segments = new List<ISegment>();
    }

    public Message(Stream stream)
    {
      (this as IMessage).ReadFromStream(stream);
    }
    #endregion

    #region ITCPMessage Members
    int IMessage.Version
    {
      get { return this.version; }
    }

    void IMessage.ReadFromStream(System.IO.Stream stream)
    {
      // read prefix
      byte[] prefix = new byte[1];
      if (!CommonUtils.ReadNBytes(stream, ref prefix, prefix.Length))
        throw new Exception("I/O error");
      if (prefix[0] != 'X')
        throw new Exception("I/O error");

      // read version
      byte[] version = new byte[1];
      if (!CommonUtils.ReadNBytes(stream, ref version, version.Length))
        throw new Exception("I/O error");
      this.version = (int)(version[0] - '0');

      // segments
      byte[] noSegments = new byte[sizeof(int)];
      if (!CommonUtils.ReadNBytes(stream, ref noSegments, noSegments.Length))
        throw new Exception("I/O error");
      int numSegments = BitConverter.ToInt32(noSegments, 0);

      // first segment is XML
      ISegment segment = new Segment(stream);
      if ((segment.Type & SegmentType.XML) != SegmentType.XML)
        throw new Exception("I/O error: wrong first segment type");
      this.xmlDoc = segment.XmlDocument;

      if (this.segments == null) this.segments = new List<ISegment>();
      this.segments.Clear();
      for (int i = 1; i < numSegments; i++)
        this.segments.Add(new Segment(stream));
    }

    void IMessage.SendToStream(System.IO.Stream stream)
    {
      byte[] segmentsCnt = BitConverter.GetBytes(this.segments.Count + 1);
      byte[] tmp = new byte[1];

      tmp[0] = (byte) 'X';
      stream.Write(tmp, 0, tmp.Length);

      tmp[0] = (byte)('0' + this.version);
      stream.Write(tmp, 0, tmp.Length);

      stream.Write(segmentsCnt, 0, segmentsCnt.Length);

      ISegment xmlSegment = new Segment(this.xmlDoc);
      xmlSegment.SendToStream(stream);
      
      foreach (ISegment segment in this.segments)
        segment.SendToStream(stream);
    }

    IMessage IMessage.Execute(ExecuteTime executeTime)
    {
      XmlElement jobs = this.getJobsElement();
      if (jobs == null) return createNewMessage("Unknown XML structure");

      IMessage retMsg = createNewMessage(null);

      XmlNode xmlJobNode = jobs.FirstChild;
      while (xmlJobNode != null)
      {
        if (xmlJobNode.LocalName == "job")
        {
          XmlElement xmlJobToExecute = xmlJobNode as XmlElement;

          string errAttribute = string.Empty, errMessage = string.Empty;
          bool isError = CommonUtils.IsAttrSet(xmlJobToExecute, "error");
          string action = CommonUtils.GetAttribute(xmlJobToExecute, "action", true);
          if ((!isError) && (action != null))
          {
            try
            {
              ExecuteAction executeActionFunction = PluginsManager.Inst.GetExecuteFunction(executeTime, action);
              if (executeActionFunction != null)
                this.executeHelper(retMsg, xmlJobToExecute, executeTime, executeActionFunction);
              else
                errAttribute = "unknownAction";
            }
            catch (Exception ex)
            {
              errAttribute = "error";
              errMessage = ex.Message;
            }
          }
          else
          {
            if (!isError) errAttribute = "actionNotSpecified";
          }

          if ((isError) || (errAttribute != string.Empty))
          {
            XmlElement newNode = retMsg.XmlDocument.ImportNode(xmlJobToExecute, true) as XmlElement;
            if (errAttribute != string.Empty) newNode.SetAttribute(errAttribute, "1");
            if (errMessage != string.Empty) CommonUtils.AddElement(newNode, "value", errMessage);
            retMsg.XmlDocument.DocumentElement.AppendChild(newNode);
          }
        }

        xmlJobNode = xmlJobNode.NextSibling;
      }

      return retMsg;
    }

    byte[] IMessage.GetJobData(XmlElement xmlJob, bool uncompress)
    {
      int? segNum = CommonUtils.GetIntAttribute(xmlJob, "segNum");
      if (!segNum.HasValue) return null;

      byte[] data = this.segments[segNum.Value].Data;
      if ((uncompress) && (CommonUtils.IsAttrSet(xmlJob, "compress")))
        data = SimpleICSharp.Compression.DeCompress(data);

      return data;
    }

    XmlDocument IMessage.XmlDocument 
    {
      get { return this.xmlDoc;  }  
    }

    List<ISegment> IMessage.Segments 
    { 
      get
      {
        return this.segments;
      }
    }
    #endregion
  }
}
