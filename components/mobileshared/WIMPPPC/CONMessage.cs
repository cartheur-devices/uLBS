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
    public class CONMessage : Message
    {
        protected string[] _convalue;
        protected CONTypes[] _contype;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CONMessage()
        {
            _messagetype = MessageTypes.CON;
        }

        /// <summary>
        ///  Generate textual representation of this object.
        /// </summary>
        /// <returns>String containing WIMP message.</returns>
        public override string ToString()
        {
            string Message = "";
            StringBuilder WIMPMessage = new StringBuilder();

            WIMPMessage.Append(_messagesequencenumber);
            WIMPMessage.Append(_fieldseparator);
            WIMPMessage.Append(_messagetype.ToString());
            WIMPMessage.Append(_fieldseparator);

            // First 3 elements aren't configurations.
            int NumberOfConfigs = _convalue.Length;

            for (int i = 0; i < NumberOfConfigs; i++)
            {
                WIMPMessage.Append(_contype[i]);
                WIMPMessage.Append(_convalue[i]);
                if (i < NumberOfConfigs-1)
                    WIMPMessage.Append(_fieldseparator);
            }

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
            // First 3 elements aren't configurations.
            int NumberOfConfigs = InData.Length - 3;

            _contype = new CONTypes[NumberOfConfigs];
            _convalue = new string[NumberOfConfigs];

            for (int i = 0; i < NumberOfConfigs; i++)
            {
                _contype[i] = (Message.CONTypes)Enum.Parse(typeof(Message.CONTypes), InData[i+3].Substring(0,1), true);
                _convalue[i] = InData[i + 3].Substring(1);
            }
        }

        public string[] CONValue
        {
            get { return _convalue; }
            set { _convalue = value; }
        }

        public CONTypes[] CONType
        {
            get { return _contype; }
            set { _contype = value; }
        }
    }
}
