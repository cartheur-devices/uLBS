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
    public class LVAMessage : Message
    {
        protected string _versionvalue;
        protected string _urlvalue;

        /// <summary>
        /// Constructor for LVA message.
        /// </summary>
        public LVAMessage()
        {
            _messagetype = MessageTypes.LVA;
        }

        /// <summary>
        ///  Generate textual representation of this object.
        /// </summary>
        /// <returns>String containing WIMP message.</returns>
        public override string ToString()
        {
            StringBuilder WIMPMessage = new StringBuilder();

            WIMPMessage.Append(_appid);
            WIMPMessage.Append(_fieldseparator);
            WIMPMessage.Append(_userid);
            WIMPMessage.Append(_fieldseparator);
            WIMPMessage.Append(_messagesequencenumber);
            WIMPMessage.Append(_fieldseparator);
            WIMPMessage.Append(_messagetype.ToString());
            WIMPMessage.Append(_fieldseparator);
            WIMPMessage.Append(_versionvalue);
            WIMPMessage.Append(_fieldseparator);
            WIMPMessage.Append(_urlvalue);

            string Message = WIMPMessage.ToString();

            // Calculate the CRC checksum;
            Crc32 Crc = new Crc32();
            Crc.Reset();
            Crc.Update(Encoding.UTF8.GetBytes(Message)); // Message minus the checksum
            string Checksum = String.Format("{0:X8}", Crc.Value);

            return String.Concat(Message, "&", Checksum);
        }

        /// <summary>
        /// Set specific data for this object.
        /// </summary>
        public override void SetData(string[] InData)
        {
            _versionvalue = InData[2];
            _urlvalue = InData[3];
        }

        public string VersionValue
        {
            get { return _versionvalue; }
            set { _versionvalue = value; }
        }

        public string URLValue
        {
            get { return _urlvalue; }
            set { _urlvalue = value; }
        }
    }
}
