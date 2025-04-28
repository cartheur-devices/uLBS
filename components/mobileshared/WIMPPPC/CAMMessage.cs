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
    public class CAMMessage : Message
    {
        protected string _convalue;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CAMMessage()
        {
            _messagetype = MessageTypes.CAM;
        }

        /// <summary>
        ///  Generate textual representation of this object.
        /// </summary>
        /// <returns>String containing WIMP message : 29;204080661358242;313;CAM;1%267EBB03BE</returns>
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
            WIMPMessage.Append("1");

            Message = WIMPMessage.ToString();

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
        public override void SetData(String[] InData)
        {
            _convalue = InData[0];
            _messagesequencenumber = Convert.ToInt32(InData[1]);
        }

        public string CONValue
        {
            get { return _convalue; }
            set { _convalue = value; }
        }
    }
}

