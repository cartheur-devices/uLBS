/*
    Copyright (c) 2009, FindWhere

    All rights reserved.

    This composite work is copyrighted in its entirety. Permission to use this material may be granted only after we receive a written request from you, and you receive written permission from us.
*/
using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace MobileShared
{
    /// <summary>
    ///    This adds a timeout value to the recieve method
    /// </summary>
    public class UDPClientEx : UdpClient
    {
        private byte[] m_Buffer;
        private int m_Timeout = 0;
        private IPEndPoint m_Endpoint;
        private bool m_Done = false;

        /// Connection timeout in milliseconds
        public int Timeout
        {
            set { m_Timeout = value; }
        }

        /// <summary>
        /// This will attempt to receive a UDP packet for n milliseconds
        /// based on the Timeout property
        /// </summary>
        /// <param name="EP">Endpoint to receive from</param>
        /// <returns>Data received, if any</returns>
        public byte[] DoRecieve(ref IPEndPoint EP)
        {
            Thread rcvTh = new Thread(new ThreadStart(RunRecieve));

            try
            {
                m_Endpoint = EP;      // set the shared data

                TimeSpan t = TimeSpan.FromMilliseconds(m_Timeout);

                DateTime tBegin = DateTime.Now;  // get the start time
                rcvTh.Start();                   // Start the thread

                // Wait loop
                while (DateTime.Now - tBegin < t)
                {
                    if (m_Done)
                        break;

                    Thread.Sleep(100);
                }

                // If the thread is running, kill it and return an empty array
                if (m_Buffer == null)
                {
                    rcvTh.Abort();
                    rcvTh = null;
                    return new byte[0];
                }
                else
                {
                    // There should be data in the buffer; return it
                    return m_Buffer;
                }

            }
            catch (Exception e)
            {
                if (rcvTh != null)
                {
                    rcvTh.Abort();
                    rcvTh = null;
                }

                throw e;
            }
            finally
            {
                // Make sure the thread is stopped
                if (rcvTh != null)
                {
                    rcvTh.Abort();
                }
            }
        }

        // This is the method wrapper for the thread
        protected void RunRecieve()
        {
            try
            {
                m_Buffer = Receive(ref m_Endpoint);
            }
            catch
            { }
            finally
            {
                m_Done = true;
            }
        }
    }
}
