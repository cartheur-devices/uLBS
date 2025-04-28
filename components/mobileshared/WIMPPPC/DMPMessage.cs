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
    public class DMPMessage : TLMMessage
    {
        /// <summary>
        /// Constructor for TLM message.
        /// </summary>
        public DMPMessage()
        {
            _messagetype = MessageTypes.DMP;
        }
    }
}
