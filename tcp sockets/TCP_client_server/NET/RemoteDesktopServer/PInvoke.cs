//-----------------------------------------------------------------------
// <copyright file="PInvoke.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;

namespace Matjazev.Tcp
{
  public static class PInvoke
  {
    #region Consts
    public const int SRCCOPY = 0x00CC0020;

    public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
    public const int MOUSEEVENTF_LEFTUP = 0x0004;
    public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
    public const int MOUSEEVENTF_RIGHTUP = 0x0010;
    public const int MOUSEEVENTF_ABSOLUTE = 0x8000;
    public const int MOUSEEVENTF_MOVE = 0x0001;
    #endregion

    #region DLLImports
#if WindowsCE
    [DllImport("Coredll.dll", EntryPoint = "GetDC")]
    public static extern IntPtr GetDC(IntPtr hWnd);

    [DllImportAttribute("coredll.dll", EntryPoint = "GetWindowDC")]
    internal static extern IntPtr GetWindowDC(IntPtr hWnd);

    [DllImport("Coredll.dll")]
    public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [DllImport("coredll.dll")]
    public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

    [DllImport("coredll.dll")]
    public static extern IntPtr CreateDIBSection(IntPtr hdc, IntPtr hdr, uint colors, ref IntPtr pBits, IntPtr hFile, uint offset);

    [DllImport("coredll.dll")]
    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

    [DllImport("coredll.dll")]
    public static extern int DeleteDC(IntPtr hdc);

    [DllImportAttribute("coredll.dll", EntryPoint = "ReleaseDC")]
    internal static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("coredll.dll")]
    public static extern IntPtr DeleteObject(IntPtr hObject); 

    [DllImport("coredll.dll")]
    public static extern int BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

    [DllImport("coredll.dll")]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, IntPtr dwExtraInfo);
#else
    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr hWnd);

    [DllImportAttribute("user32.dll", EntryPoint = "GetWindowDC")]
    internal static extern IntPtr GetWindowDC(IntPtr hWnd);

    [DllImport("GDI32.dll")]
    public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateDIBSection(IntPtr hdc, IntPtr hdr, uint colors, ref IntPtr pBits, IntPtr hFile, uint offset);

    [DllImport("gdi32.dll")]
    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

    [DllImport("gdi32.dll")]
    public static extern int DeleteDC(IntPtr hdc);

    [DllImportAttribute("user32.dll", EntryPoint = "ReleaseDC")]
    internal static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll")]
    public static extern IntPtr DeleteObject(IntPtr hObject);

    [DllImport("gdi32.dll")]
    public static extern int BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

    [DllImport("user32.dll")]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, IntPtr dwExtraInfo);

#endif
    #endregion
  }
}
