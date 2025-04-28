//-----------------------------------------------------------------------
// <copyright file="CommonUtils.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using Matjazev.Tcp.Plugin.Interfaces;

namespace Matjazev.Tcp.Plugin
{
  public class CommonUtils
  {
    private CommonUtils()
    {
      // prevent users from creating an instance
    }

    public static readonly byte EOT = 0x03;

    #region Network
    private static string readFromNetwork_delimeted(NetworkStream clientStream)
    {
      byte[] message = new byte[1024];
      MemoryStream memory = new MemoryStream(message.Length);

      int bytesRead;
      do
      {
        bytesRead = clientStream.Read(message, 0, message.Length);
        if (bytesRead == 0)
          return string.Empty;

        memory.Write(message, 0, bytesRead);
      }
      while (message[bytesRead - 1] != EOT);

      return Encoding.UTF8.GetString(memory.GetBuffer(), 0, (int)memory.Length - 1); // cut off EOT
    }

    private static string readFromNetwork_lenPrefix(NetworkStream clientStream)
    {
      byte[] sizeinfo = new byte[sizeof(int)];
      if (!ReadNBytes(clientStream, ref sizeinfo, sizeof(int)))
        return string.Empty;

      int wholeSize = BitConverter.ToInt32(sizeinfo, 0);
      byte[] message = new byte[wholeSize];
      if (!ReadNBytes(clientStream, ref message, wholeSize))
        return string.Empty;

      return Encoding.UTF8.GetString(message, 0, wholeSize);
    }

    private static void sendToNetwork_delimeted(NetworkStream clientStream, string data)
    {
      byte[] byteData = Encoding.UTF8.GetBytes(data);
      clientStream.Write(byteData, 0, byteData.Length);

      byte[] eot = new byte[1] { EOT };
      clientStream.Write(eot, 0, 1);
      clientStream.Flush();
    }

    private static void sendToNetwork_lenPrefix(NetworkStream clientStream, string data)
    {
      byte[] byteData = Encoding.UTF8.GetBytes(data);
      byte[] byteLen = BitConverter.GetBytes(byteData.Length);
      clientStream.Write(byteLen, 0, byteLen.Length);
      clientStream.Write(byteData, 0, byteData.Length);

      clientStream.Flush();
    }

    public static bool ReadNBytes(Stream stream, ref byte[] buffer, int noBytes)
    {
      int bytesReadCurr = 0, bytesReadFull = 0;
      do
      {
        bytesReadCurr = stream.Read(buffer, bytesReadFull, noBytes - bytesReadFull);
        bytesReadFull += bytesReadCurr;
      }
      while ((bytesReadFull < noBytes) && (bytesReadCurr > 0));

      return (bytesReadCurr > 0);
    }

    public static string ReadFromNetwork(NetworkStream clientStream)
    {
      // return readFromNetwork_delimeted(clientStream);
      return readFromNetwork_lenPrefix(clientStream);
    }

    public static void SendToNetwork(NetworkStream clientStream, string data)
    {
      // sendToNetwork_delimeted(clientStream, data);
      sendToNetwork_lenPrefix(clientStream, data);
    }

    public static IList<string> GetAllPublicIPs()
    {
      IList<string> ips = new List<string>();

      IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
      foreach (IPAddress addr in host.AddressList)
        ips.Add(addr.ToString());

      return ips;
    }
    #endregion

    #region XML
    public static string EscapeXml(string s)
    {
#if WindowsCE
    string xml = s;
    if (!string.IsNullOrEmpty(xml))
    {
      // replace literal values with entities
      xml = xml.Replace("&", "&amp;");
      xml = xml.Replace("&lt;", "&lt;");
      xml = xml.Replace("&gt;", "&gt;");
      xml = xml.Replace("\"", "&quot;");
      xml = xml.Replace("'", "&apos;");
    }

    return xml;
#else
      return System.Security.SecurityElement.Escape(s);
#endif
    }

    public static bool IsAttrSet(XmlElement node, string attribute)
    {
      if (!node.HasAttribute(attribute)) return false;
      string value = node.Attributes[attribute].Value.ToUpper();

      return ((value == "1") || (value == "DA") || (value == "OK") || (value == "YES") || (value == "TRUE"));
    }

    public static string GetAttribute(XmlElement element, string attribute)
    {
      if ((element == null) || (!element.HasAttribute(attribute)))
        return null;

      return element.Attributes[attribute].Value.ToString();
    }

    public static string GetAttribute(XmlElement element, string attribute, bool upperCase)
    {
      string tmp = GetAttribute(element, attribute);
      if (tmp == null) return null;

      return tmp.ToUpper();
    }

    public static int? GetIntAttribute(XmlElement element, string attribute)
    {
      return GetIntAttribute(element, attribute, null, null);
    }

    public static int? GetIntAttribute(XmlElement element, string attribute, int? minValue, int? maxValue)
    {
      try
      {
        string tmp = CommonUtils.GetAttribute(element, attribute);
        if (tmp == null) return null;

        int value = Convert.ToInt32(tmp);
        if ((minValue.HasValue) && (value < minValue.Value))
          value = minValue.Value;
        if ((maxValue.HasValue) && (value < maxValue.Value))
          value = maxValue.Value;

        return value;
      }
      catch (Exception)
      {
        return null;
      }
    }

    public static void AddElement(XmlElement node, string elementName, string elementValue)
    {
      XmlElement elem = GetOrAddSubElement(node, elementName);
      elem.AppendChild(node.OwnerDocument.CreateTextNode(elementValue));
    }

    public static void AddSubElement(XmlElement node, string elementName, string subElementName, string subElementValue)
    {
      XmlElement subElem = node.OwnerDocument.CreateElement(subElementName);
      subElem.AppendChild(node.OwnerDocument.CreateTextNode(subElementValue));

      XmlElement elem = GetOrAddSubElement(node, elementName);
      elem.AppendChild(subElem);
    }

    public static XmlElement GetOrAddSubElement(XmlElement node, string subElement)
    {
      XmlElement sub = (node.SelectSingleNode(subElement) as XmlElement);
      if (sub == null)
      {
        sub = node.OwnerDocument.CreateElement(subElement);
        node.AppendChild(sub);
      }

      return sub;
    }

    internal static void PluginDataToXML(XmlElement newNode, bool forServer)
    {
      XmlDocument root = newNode.OwnerDocument;

      IList<PluginData> list = (forServer) ? PluginsManager.Inst.ServerPlugins : PluginsManager.Inst.ClientPlugins;
      XmlElement subElement = root.CreateElement((forServer) ? "server" : "client");

      foreach (PluginData data in list)
      {
        XmlElement plugin = root.CreateElement("plugin");

        StringBuilder actions = new StringBuilder();
        foreach (string act in data.Plugin.Actions)
        {
          if (actions.Length > 0) actions.Append(", ");
          actions.Append(act);
        }

        AddElement(plugin, "name", data.Plugin.BasicData.Name);
        AddElement(plugin, "author", data.Plugin.BasicData.Author);
        AddElement(plugin, "version", data.Plugin.BasicData.Version);
        AddElement(plugin, "actions", actions.ToString());
        AddElement(plugin, "path", data.Path);

        subElement.AppendChild(plugin);
      }

      newNode.AppendChild(subElement);
    }
    #endregion

    #region EncodeDecode
    public static string ZipStringToBase64(string value)
    {
      return SimpleICSharp.Compression.Compress(value);
    }

    public static string UnZipStringFromBase64(string value)
    {
      return SimpleICSharp.Compression.DeCompress(value);
    }

    public static string EncodeFileToBase64(string fileName, bool zip)
    {
      FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
      byte[] filebytes = new byte[fs.Length];
      fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
      fs.Close();

      if (zip)
        return Convert.ToBase64String(SimpleICSharp.Compression.Compress(filebytes));
      else
        return Convert.ToBase64String(filebytes);
    }

    public static byte[] DecodeFromBase64(string base64FileData, bool zip)
    {
      byte[] filebytes = Convert.FromBase64String(base64FileData);
      if (zip) filebytes = SimpleICSharp.Compression.DeCompress(filebytes);

      return filebytes;
    }

    public static void DecodeFileFromBase64(string base64FileData, string fileName, bool createOnly, bool zip)
    {
      byte[] filebytes = DecodeFromBase64(base64FileData, zip);
      FileMode mode = createOnly ? FileMode.CreateNew : FileMode.OpenOrCreate;
      FileStream fs = new FileStream(fileName, mode, FileAccess.Write, FileShare.None);
      fs.Write(filebytes, 0, filebytes.Length);
      fs.Close();
    }
    #endregion
  }
}
