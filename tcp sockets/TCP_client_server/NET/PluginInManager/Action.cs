//-----------------------------------------------------------------------
// <copyright file="Action.cs" company="Matjazev.NET">
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
  public class Action : IAction
  {
    private XmlElement xmlJob = null;
    private ISegment segment = null;

    private Action(XmlElement xmlJob, ISegment data)
    {
      this.xmlJob = xmlJob;
      this.segment = data;
    }

    public static Action CreateAction(XmlElement xmlJob, ISegment data)
    {
      return new Action(xmlJob, data);
    }

    public static Action CreateAction(XmlElement xmlJob)
    {
      return new Action(xmlJob, null);
    }
    #region IAction Members

    ISegment IAction.Segment
    {
      get { return this.segment; }
      set { this.segment = value; }
    }

    XmlElement IAction.Job
    {
      get { return this.xmlJob; }
      set { this.xmlJob = value; }
    }

    #endregion

    public static void NoAction(IAction action, ref IAction outAction)
    {
      return;
    }
  }
}
