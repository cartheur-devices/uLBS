//-----------------------------------------------------------------------
// <copyright file="RemoteDesktop.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Matjazev.Tcp.Plugin.Interfaces;

namespace Matjazev.Tcp.Plugin
{
  [TCPServerPluginAttribute]
  public class RemoteDesktop : Plugin
  {
    #region Graphics
    private Bitmap screenCapture(IntPtr hWnd, Rectangle rect)
    {
      IntPtr hBitmap;
      IntPtr hDC = PInvoke.GetDC(hWnd);
      IntPtr hMemDC = PInvoke.CreateCompatibleDC(hDC);

      hBitmap = PInvoke.CreateCompatibleBitmap(hDC, rect.Width, rect.Height);
      if (hBitmap != IntPtr.Zero)
      {
        IntPtr hOld = (IntPtr)PInvoke.SelectObject(hMemDC, hBitmap);
        PInvoke.BitBlt(hMemDC, 0, 0, rect.Width, rect.Height, hDC, 0, 0, PInvoke.SRCCOPY);

        PInvoke.SelectObject(hMemDC, hOld);
        PInvoke.DeleteDC(hMemDC);
        PInvoke.ReleaseDC(hWnd, hDC);
        Bitmap bmp = System.Drawing.Image.FromHbitmap(hBitmap);
        PInvoke.DeleteObject(hBitmap);
        return bmp;
      }

      return null;
    }

    private void captureWholeScreen(Stream stream, ImageFormat format)
    {
      Bitmap bm = this.screenCapture(IntPtr.Zero, Screen.PrimaryScreen.Bounds);
      bm.Save(stream, format);
    }
    #endregion

    #region Private
    // There is something Worng with imageFormat class on .NET CF so I wrote different function
    //
    //    ImageFormat getImageFormat(XmlElement xmlJob)
    //    {
    //      string format = "";
    //      CommonUtils.GetAttribute(xmlJob, "format", out format, true);
    //      switch (format)
    //      {
    //        case "BMP": return ImageFormat.Bmp;
    //        case "JPG": return ImageFormat.Jpeg;
    //        case "GIF": return ImageFormat.Gif;
    //        case "PNG": return ImageFormat.Png;
    //      }
    //    
    //      return ImageFormat.Bmp;
    //    }
    private string getImageFormat(XmlElement xmlJob)
    {
      string format = CommonUtils.GetAttribute(xmlJob, "format", true);
      if ((format == "BMP") || (format == "JPG") || (format == "GIF") || (format == "PNG"))
        return format;
      
      return "BMP";
    }

    private ISegment getPicture(XmlElement xmlJob, ref string format)
    {
      MemoryStream memoryPict = new MemoryStream();

      try
      {
        ImageFormat imageFormat = ImageFormat.Bmp;
        switch (format)
        {
          case "JPG": 
            imageFormat = ImageFormat.Jpeg; 
            break;

          case "GIF": 
            imageFormat = ImageFormat.Gif; 
            break;

          case "PNG": 
            imageFormat = ImageFormat.Png; 
            break;
        }

        this.captureWholeScreen(memoryPict, imageFormat);
      }
      catch
      {
        format = "BMP";
        this.captureWholeScreen(memoryPict, ImageFormat.Bmp);
      }

      ISegment retValue = new Segment(SegmentType.Bin, memoryPict.ToArray());
      if ((CommonUtils.IsAttrSet(xmlJob, "compress")) || (format == "BMP")) retValue.Compress();

      return retValue;
    }

    private void sendKeys(string keys)
    { 
#if WindowsCE
      OpenNETCF.Windows.Forms.SendKeys.Send(keys);
#else
      SendKeys.SendWait(keys);
#endif
    }
    #endregion

    #region Actions
    public void ExecuteSendKeys(IAction action, ref IAction outAction)
    {
      string keys = CommonUtils.GetAttribute(action.Job, "keys");
      if (keys == null) 
        throw new Exception("keys not specified");

      this.sendKeys(keys);
      CommonUtils.AddElement(outAction.Job, "value", "OK");
    }

    public void ExecuteMouseClick(IAction action, ref IAction outAction)
    {
      string sX = CommonUtils.GetAttribute(action.Job, "x"),
             sY = CommonUtils.GetAttribute(action.Job, "y");
      if ((sX == null) || (sY == null))
        throw new Exception("Coordinates not specified");

      uint x = Convert.ToUInt32(sX);
      uint y = Convert.ToUInt32(sY);

      if (CommonUtils.IsAttrSet(action.Job, "dblClick"))
      { 
        PInvoke.mouse_event(PInvoke.MOUSEEVENTF_MOVE | PInvoke.MOUSEEVENTF_ABSOLUTE |
                            PInvoke.MOUSEEVENTF_LEFTDOWN | PInvoke.MOUSEEVENTF_LEFTUP, x, y, 0, IntPtr.Zero);
        PInvoke.mouse_event(PInvoke.MOUSEEVENTF_MOVE | PInvoke.MOUSEEVENTF_ABSOLUTE |
                            PInvoke.MOUSEEVENTF_LEFTDOWN | PInvoke.MOUSEEVENTF_LEFTUP, x, y, 0, IntPtr.Zero);
      }
      else 
      {
        int duration = CommonUtils.GetIntAttribute(action.Job, "duration", 0, 5000) ?? 0;

        PInvoke.mouse_event(PInvoke.MOUSEEVENTF_MOVE | PInvoke.MOUSEEVENTF_ABSOLUTE |
                            PInvoke.MOUSEEVENTF_LEFTDOWN, x, y, 0, IntPtr.Zero);
        System.Threading.Thread.Sleep(duration);
        PInvoke.mouse_event(PInvoke.MOUSEEVENTF_MOVE | PInvoke.MOUSEEVENTF_ABSOLUTE |
                            PInvoke.MOUSEEVENTF_LEFTUP, x, y, 0, IntPtr.Zero);
      }

      CommonUtils.AddElement(outAction.Job, "value", "OK");
    }

    public void ExecuteCapture(IAction action, ref IAction outAction)
    {
      string format = this.getImageFormat(action.Job);
      outAction.Segment = this.getPicture(action.Job, ref format);
      if (outAction.Segment.Length == 0) 
        throw new Exception("Unknown error");

      outAction.Job.SetAttribute("format", format);
      if ((outAction.Segment.Type & SegmentType.Gzip) > 0) 
        outAction.Job.SetAttribute("compress", "1");
    } 
    #endregion

    public override IBasicPluginData Description()
    {
      return new BasicPluginData("Simple Remote Desktop Server", "1.0 beta", "Matjaž Prtenjak");
    }

    public RemoteDesktop()
    {
      addServerAction("capture", this.ExecuteCapture);
      addServerAction("mouseClick", this.ExecuteMouseClick);
      addServerAction("sendKeys", this.ExecuteSendKeys);
    }
  }
}
