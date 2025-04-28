//-----------------------------------------------------------------------
// <copyright file="Segment.cs" company="Matjazev.NET">
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
  public class Segment : ISegment
  {
    private SegmentType type = SegmentType.Unknown;
    private byte[] data = null;
    private byte[] unCmprsData = null;
    private XmlDocument xmlDoc = null;

    private byte[] getUnCompressData()
    {
      if (this.unCmprsData != null) return this.unCmprsData;

      if ((this.type & SegmentType.Gzip) == SegmentType.Gzip)
        this.unCmprsData = SimpleICSharp.Compression.DeCompress(this.data);
      else
        this.unCmprsData = this.data;

      return this.unCmprsData;
    }

    private XmlDocument getXmlDocument()
    {
      if (this.xmlDoc != null) return this.xmlDoc;

      if ((this.type & SegmentType.XML) == SegmentType.XML)
      {
        string xmlString = Encoding.UTF8.GetString(this.getUnCompressData(), 0, this.getUnCompressData().Length);

        this.xmlDoc = new XmlDocument();
        this.xmlDoc.LoadXml(xmlString);
      }
      else
        this.xmlDoc = null;

      return this.xmlDoc;
    }

    /*public Segment()
    { 
    }*/

    public Segment(SegmentType type, byte[] data)
    {
      this.type = type;
      this.data = data;
    }

    public Segment(XmlDocument xmlDocument)
    {
      this.type = SegmentType.XML;
      this.data = Encoding.UTF8.GetBytes(xmlDocument.OuterXml);
    }

    public Segment(System.IO.Stream stream)
    {
      (this as ISegment).ReadFromStream(stream);
    }

    public Segment(string fileName, bool compress)
    {
      (this as ISegment).LoadFromFile(fileName, compress);
    }

    #region ITCPSegment Members
    int ISegment.Length
    {
      get { return this.data.Length; }
    }

    SegmentType ISegment.Type
    {
      get { return this.type; }
    }

    byte[] ISegment.Data
    {
      get { return this.data; }
    }

    byte[] ISegment.UnCompressData
    {
      get { return this.getUnCompressData(); }
    }

    XmlDocument ISegment.XmlDocument
    {
      get { return this.getXmlDocument(); }
    }

    void ISegment.ReadFromStream(System.IO.Stream stream)
    {
      // read len
      byte[] sizeinfo = new byte[sizeof(int)];
      if (!CommonUtils.ReadNBytes(stream, ref sizeinfo, sizeinfo.Length))
        throw new Exception("I/O error");
      int len = BitConverter.ToInt32(sizeinfo, 0);

      // read type
      byte[] type = new byte[1];
      if (!CommonUtils.ReadNBytes(stream, ref type, type.Length))
        throw new Exception("I/O error");
      this.type = (SegmentType)type[0];

      // read data
      this.data = new byte[len];
      if (!CommonUtils.ReadNBytes(stream, ref this.data, this.data.Length))
        throw new Exception("I/O error");
    }

    void ISegment.SendToStream(System.IO.Stream stream)
    {
      if (((this.type & SegmentType.XML) > 0) &&
          ((this.type & SegmentType.Gzip) == 0) &&
          (this.data.Length > 1024))
      {
        this.type |= SegmentType.Gzip;
        this.data = SimpleICSharp.Compression.Compress(this.data);
      }

      byte[] byteLen = BitConverter.GetBytes(this.data.Length);
      byte[] byteType = new byte[1];
      byteType[0] = (byte)this.type;

      stream.Write(byteLen, 0, byteLen.Length);
      stream.Write(byteType, 0, byteType.Length);
      stream.Write(this.data, 0, this.data.Length);
    }

    bool ISegment.Compress()
    {
      if (((this.type & SegmentType.Gzip) > 0) || (this.data == null)) return false;

      this.type |= SegmentType.Gzip;
      this.data = SimpleICSharp.Compression.Compress(this.data);
      return true;
    }

    void ISegment.SaveToFile(string fileName, bool createOnly)
    {
      byte[] fileData = this.getUnCompressData();
      FileMode mode = createOnly ? FileMode.CreateNew : FileMode.OpenOrCreate;
      FileStream fs = new FileStream(fileName, mode, FileAccess.Write, FileShare.None);
      fs.Write(fileData, 0, fileData.Length);
      fs.Close();
    }

    void ISegment.LoadFromFile(string fileName, bool compress)
    {
      FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
      this.data = new byte[fs.Length];
      fs.Read(this.data, 0, Convert.ToInt32(fs.Length));
      fs.Close();

      this.type |= SegmentType.Bin;
      if (compress) 
        (this as ISegment).Compress();
    }
    #endregion
  }
}

