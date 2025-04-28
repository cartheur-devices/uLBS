/*
    Copyright (c) 2009, FindWhere

    All rights reserved.

    This composite work is copyrighted in its entirety. Permission to use this material may be granted only after we receive a written request from you, and you receive written permission from us.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace WIMP
{
    public class TLMMessage : Message
    {
        /// <summary>
        /// Constructor for TLM message.
        /// </summary>
        public TLMMessage()
        {
            _messagetype = MessageTypes.TLM;
        }

        /// <summary>
        ///  Generate textual representation of this object.
        /// </summary>
        /// <returns>String containing WIMP message, for example: 1;12;284011234567890;1234;1104537600;TLM;1104537610;N51.25546;E005.76591;B2EE575B07D90254582E91C9C57A686F.</returns>
        public override string ToString()
        {
            string Message = "";
            StringBuilder WIMPMessage = new StringBuilder();

            WIMPMessage.Append(_appid);
            WIMPMessage.Append(_fieldseparator);
            WIMPMessage.Append(_userid);
            WIMPMessage.Append(_fieldseparator);
            WIMPMessage.Append(_messagesequencenumber);
            WIMPMessage.Append(_fieldseparator);
            WIMPMessage.Append(_messagetype.ToString());
            WIMPMessage.Append(_fieldseparator);

            // Discard time is no GPS is available
            if (_longitude != null)
                WIMPMessage.Append(Math.Round(ToUnixTime(_telemetrytime)).ToString());
            else
                WIMPMessage.Append(Math.Round(ToUnixTime(DateTime.UtcNow)).ToString());

            WIMPMessage.Append(_fieldseparator);
            if (_latitude != null)
                WIMPMessage.Append(_latitude.ToString().Replace(",", "."));
            WIMPMessage.Append(_fieldseparator);
            if (_longitude != null)
                WIMPMessage.Append(_longitude.ToString().Replace(",", "."));
            WIMPMessage.Append(_fieldseparator);
            if (_cellid != 0)
                WIMPMessage.Append(_cellid.ToString());
            WIMPMessage.Append(_fieldseparator);
            if (_lacid != 0)
                WIMPMessage.Append(_lacid.ToString());
            WIMPMessage.Append(_fieldseparator);
            if (_mccid != 0)
                WIMPMessage.Append(_mccid.ToString());
            WIMPMessage.Append(_fieldseparator);
            if (_mncid != 0)
                WIMPMessage.Append(_mncid.ToString());
            WIMPMessage.Append(_fieldseparator);
            WIMPMessage.Append(_signal.ToString());
            WIMPMessage.Append(_fieldseparator);
            if (_networktype != 0)
                WIMPMessage.Append(_networktype.ToString());
            WIMPMessage.Append(_fieldseparator);
            WIMPMessage.Append(BatteryLevel);
            WIMPMessage.Append(_fieldseparator);
            if (_speedinkmhour != 0)
                WIMPMessage.Append(_speedinkmhour);
            WIMPMessage.Append(_fieldseparator);
            if (_headingindegrees != 0)
                WIMPMessage.Append(_headingindegrees);

            Message = WIMPMessage.ToString();

            // Calculate the CRC checksum;
            Crc32 Crc = new Crc32();
            Crc.Reset();
            Crc.Update(Encoding.UTF8.GetBytes(Message)); // Message minus the checksum
            string Checksum = String.Format("{0:X8}", Crc.Value);

            return String.Concat(Message, "&", Checksum); //MD5Helper.getMd5Hash(Message)
        }

        /// <summary>
        /// Set specific data for this object.
        /// </summary>
        public override void SetData(string[] InData)
        {
            _signal         = Convert.ToInt32(InData[0]);
            _networktype    = Convert.ToInt32(InData[1]);
            BatteryLevel = Convert.ToInt32(InData[2]);
        }
    }
}
