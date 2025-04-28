//-----------------------------------------------------------------------
// <copyright file="Utils.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Matjazev.Tcp.Plugin
{
  public class Utils
  {
    #region FileSystem
    private static string getDirAndFileList(string directory, bool directoriesOnly, string filePattern, bool recursive)
    {
      StringBuilder xmlString = new StringBuilder(1024);

      xmlString.Append(string.Format("<Dir name='{0}'>", CommonUtils.EscapeXml(directory)));
      try
      {
        foreach (string dir in Directory.GetDirectories(directory))
        {
          if (recursive)
            xmlString.Append(getDirAndFileList(dir, directoriesOnly, filePattern, recursive));
          else
          {
            string[] parts = dir.Split(Path.DirectorySeparatorChar);
            xmlString.Append(string.Format("<Dir name='{0}' />", CommonUtils.EscapeXml(parts[parts.Length - 1])));
          }
        }

        if (!directoriesOnly)
          foreach (string f in Directory.GetFiles(directory, filePattern))
          {
            FileInfo fi = new FileInfo(f);
            xmlString.Append(string.Format("<File name='{0}' ext='{1}' length='{2}' last_write_time='{3}' />",
               CommonUtils.EscapeXml(fi.Name), CommonUtils.EscapeXml(fi.Extension), fi.Length, fi.LastWriteTime.ToString("yyyy-MM-dd hh:mm:ss")));
          }
      }
      catch
      {
        // Could not open the directory
      }

      xmlString.Append("</Dir>");

      return xmlString.ToString();
    }

    internal static string GetFiles(string directory, string filePattern, bool recursive)
    {
      return "<?xml version='1.0' encoding='utf-8'?>" + getDirAndFileList(directory, false, filePattern, recursive);
    }

    internal static string GetDirectories(string directory, bool recursive)
    {
      return "<?xml version='1.0' encoding='utf-8'?>" + getDirAndFileList(directory, true, "*.*", recursive);
    }
    #endregion
  }
}
