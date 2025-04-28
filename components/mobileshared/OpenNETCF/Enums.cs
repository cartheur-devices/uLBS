using System;

namespace OpenNETCF.WindowsCE
{
    [Flags]
    internal enum PowerStateFlags
    {
        On = (0x00010000),        // on state
        Off = (0x00020000),        // no power, full off
        CriticalOff = (0x00040000),        // critical off
        Boot = (0x00080000),        // boot state
        Idle = (0x00100000),        // idle state
        Suspend = (0x00200000),        // suspend state
        Reset = (0x00800000),        // reset state
        PasswordProtected = (0x10000000)         // This state is password protected.
    }

    /// <summary>
    /// Available notification events
    /// </summary>
    public enum NotificationEvent : int
    {
        /// <summary>   
        /// No events�remove all event registrations for this application.   
        /// </summary>   
        None = 0x00,
        /// <summary>   
        /// When the system time is changed.   
        /// </summary>   
        TimeChange = 0x01,
        /// <summary>   
        /// When data synchronization finishes.   
        /// </summary>   
        SyncEnd = 0x02,
        /// <summary>
        /// The user turned the AC power on.
        /// </summary>
        OnACPower = 0x03,
        /// <summary>
        /// The user turned the alternating current (AC) power off.
        /// </summary>
        OffACPower = 0x04,
        /// <summary>
        /// The device connected to a network.
        /// </summary>
        NetConnect = 0x05,
        /// <summary>
        /// The device disconnected from a network.
        /// </summary>
        NetDisconnect = 0x06,
        /// <summary>   
        /// When a PC Card device is changed.   
        /// </summary>   
        DeviceChange = 0x07,
        /// <summary>
        /// The device discovered a server by using infrared communications.
        /// </summary>
        IRDiscovered = 0x08,
        /// <summary>   
        /// When an RS232 connection is made.   
        /// </summary>   
        RS232Detected = 0x09,
        /// <summary>   
        /// When a full device data restore completes.   
        /// </summary>   
        RestoreEnd = 0x0A,
        /// <summary>   
        /// When the device wakes up.   
        /// </summary>   
        Wakeup = 0x0B,
        /// <summary>   
        /// When the time zone is changed.   
        /// </summary>   
        TimeZoneChange = 0x0C,
        /// <summary>
        /// When the machines name changes.
        /// Requires Windows CE.NET 4.2.
        /// </summary>
        MachineNameChange = 0x0D,
        /// <summary>
        /// RNDISFN interface is instantiated.
        /// Requires Windows CE 5.0.
        /// </summary>
        RndisFNDetected = 0x0E,
        /// <summary>
        /// The Internet Proxy used by the device has changed.
        /// Requires Windows CE 5.0.
        /// </summary>
        InternetProxyChange = 0x0f,
    }
}
