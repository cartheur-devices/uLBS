using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib;

namespace SimpleICSharp
{
  public enum CompressionType
  {
    GZip,
    BZip2,
    Zip
  }

  public class Compression
  {
    private static CompressionType compressionProvider = CompressionType.GZip;
    private static int compressionLevel = 4;

    private static Stream OutputStream(Stream inputStream)
    {
      switch (compressionProvider)
      {
        case CompressionType.BZip2: 
          return new ICSharpCode.SharpZipLib.BZip2.BZip2OutputStream(inputStream);

        case CompressionType.Zip:
          {
            ICSharpCode.SharpZipLib.Zip.ZipOutputStream outStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(inputStream);
            outStream.SetLevel(compressionLevel);
            return outStream;
          }

        default:
          {
            ICSharpCode.SharpZipLib.GZip.GZipOutputStream outStream = new ICSharpCode.SharpZipLib.GZip.GZipOutputStream(inputStream);
            outStream.SetLevel(compressionLevel);
            return outStream;
          }
      }
    }

    private static Stream InputStream(Stream inputStream)
    {
      switch (compressionProvider)
      {
        case CompressionType.BZip2: return new ICSharpCode.SharpZipLib.BZip2.BZip2InputStream(inputStream);
        case CompressionType.Zip: return new ICSharpCode.SharpZipLib.Zip.ZipInputStream(inputStream);
        default: return new ICSharpCode.SharpZipLib.GZip.GZipInputStream(inputStream);
      }
    }

    public static byte[] Compress(byte[] bytesToCompress)
    {
      MemoryStream ms = new MemoryStream();
      Stream s = OutputStream(ms);
      s.Write(bytesToCompress, 0, bytesToCompress.Length);
      s.Close();
      return ms.ToArray();
    }

    public static string Compress(string stringToCompress)
    {
      byte[] compressedData = CompressToByte(stringToCompress);
      string strOut = Convert.ToBase64String(compressedData);
      return strOut;
    }

    public static byte[] CompressToByte(string stringToCompress)
    {
      byte[] bytData = Encoding.Unicode.GetBytes(stringToCompress);
      return Compress(bytData);
    }

    public static string DeCompress(string stringToDecompress)
    {
      string outString = string.Empty;
      if (stringToDecompress == null)
        throw new ArgumentNullException("stringToDecompress", "You tried to use an empty string");

      byte[] inArr = Convert.FromBase64String(stringToDecompress.Trim());
      byte[] outArr = DeCompress(inArr);
      return System.Text.Encoding.Unicode.GetString(outArr, 0, outArr.Length);
    }

    public static byte[] DeCompress(byte[] bytesToDecompress)
    {
      byte[] writeData = new byte[4096];
      Stream s2 = InputStream(new MemoryStream(bytesToDecompress));
      MemoryStream outStream = new MemoryStream();

      while (true)
      {
        int size = s2.Read(writeData, 0, writeData.Length);
        if (size > 0) 
          outStream.Write(writeData, 0, size);
        else
          break;
      }

      s2.Close();
      byte[] outArr = outStream.ToArray();
      outStream.Close();
      return outArr;
    }
  }
} 