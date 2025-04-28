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
    public class NAKMessage : Message
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NAKMessage()
        {
            _messagetype = MessageTypes.NAK;
        }

        /// <summary>
        ///  Generate textual representation of this object.
        /// </summary>
        /// <returns>String containing WIMP message.</returns>
        public override string ToString()
        {
            return "";
        }


        /// <summary>
        /// Set specific data for this object.
        /// </summary>
        public override void SetData(String[] InData)
        {
            switch (InData[2])
            {
                case "A":
                    _messagesubtype = MessageSubTypes.A;
                    break;
                case "B":
                    _messagesubtype = MessageSubTypes.B;
                    break;
                case "D":
                    _messagesubtype = MessageSubTypes.D;
                    break;
                case "H":
                    _messagesubtype = MessageSubTypes.H;
                    break;
                case "I":
                    _messagesubtype = MessageSubTypes.I;
                    break;
            }

            this.ServerCommand = InData[3];
        }
    }
}
