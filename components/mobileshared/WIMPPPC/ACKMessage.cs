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
    public class ACKMessage : Message
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ACKMessage()
        {
            _messagetype = MessageTypes.ACK;
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
                case "G":
                    _messagesubtype = MessageSubTypes.G;
                    break;
                case "F":
                    _messagesubtype = MessageSubTypes.F;
                    break;
                case "C":
                    _messagesubtype = MessageSubTypes.C;
                    break;
                case "J":
                    _messagesubtype = MessageSubTypes.J;
                    break;
                case "L":
                    _messagesubtype = MessageSubTypes.L;
                    break;
                case "N":
                    _messagesubtype = MessageSubTypes.N;
                    break;
                case "E":
                    _messagesubtype = MessageSubTypes.E;
                    break;
            }

            if (InData[4] == "1")
                _servercommand = "1";
            else
                _servercommand = "0";
        }
    }
}
