//-----------------------------------------------------------------------
// <copyright file="FSRep.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Matjazev.Tcp
{
  public class FSRep
  {
    public class File
    {
      private string name;
      private string ext;
      private long size;
      private DateTime lastWrite;

      public File(XmlNode xmlNodeFile)
      {
        this.name = xmlNodeFile.Attributes["name"].InnerText;
        this.ext = xmlNodeFile.Attributes["ext"].InnerText;
        this.size = Convert.ToInt64(xmlNodeFile.Attributes["length"].InnerText);
        this.lastWrite = Convert.ToDateTime(xmlNodeFile.Attributes["last_write_time"].InnerText);
      }

      public DateTime LastWrite
      {
        get { return this.lastWrite; }
      }

      public long Size
      {
        get { return this.size; }
      }

      public string Ext
      {
        get { return this.ext; }
      }

      public string Name
      {
        get { return this.name; }
      }
    }

    public class Directory
    {
      private string path;
      private string name;
      private bool reread;
      private List<File> files;

      public Directory(string path)
      {
        string[] parts = path.Split(System.IO.Path.DirectorySeparatorChar);
        this.name = parts[parts.Length - 1];
        this.path = path;
        this.reread = false;
        this.files = new List<File>();
      }

      public List<File> Files
      {
        get { return this.files; }
        set { this.files = value; }
      }

      public bool Readed
      {
        get { return this.reread; }
        set { this.reread = value; }
      }

      public string Name
      {
        get { return this.name; }
      }

      public string Path
      {
        get { return this.path; }
      }
    }
  }
}
