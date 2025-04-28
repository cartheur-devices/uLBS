//-----------------------------------------------------------------------
// <copyright file="PerformanceTimer.cs" company="Matjazev.NET">
//     Copyright (c) www.matjazev.net. All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;

namespace Matjazev.Tcp
{
  public class PerformanceTimer
  {
    #region DLLImports
#if WindowsCE
    [DllImport("coredll.dll")]
    public static extern int QueryPerformanceCounter(ref long perfCounter);
    [DllImport("coredll.dll")]
    public static extern int QueryPerformanceFrequency(ref long frequency);
#else
    [DllImport("Kernel32.dll")]
    private static extern int QueryPerformanceFrequency(ref long frequency);
    [DllImport("Kernel32.dll")]
    private static extern int QueryPerformanceCounter(ref long performanceCount);
#endif
    #endregion

    private static long frequency = 0;

    private long start;

    private void init(bool start)
    {
      if (frequency == 0)
      {
        if (QueryPerformanceFrequency(ref frequency) == 0)
        {
          throw new ApplicationException();
        }

        // Convert to ms.
        frequency /= 1000;
      }

      if (start) this.Start();
    }

    public PerformanceTimer(bool start)
    {
      this.init(start);
    }

    public PerformanceTimer()
    {
      this.init(true);
    }

    public void Start()
    {
      if (QueryPerformanceCounter(ref this.start) == 0)
      {
        throw new ApplicationException();
      }
    }

    public long Stop()
    {
      long stop = 0;
      if (QueryPerformanceCounter(ref stop) == 0)
      {
        throw new ApplicationException();
      }

      return (stop - this.start) / frequency;
    }
  }
}
