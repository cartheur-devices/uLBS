using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    /// <summary>
    /// The message coming from the reply.
    /// </summary>
    public class SymmetryResetDeviceJavaCallReply : JavaCallReply
    {    
        private string error;
        private string confirm;

        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetryResetDeviceJavaCallReply"/> class.
        /// </summary>
        public SymmetryResetDeviceJavaCallReply()   {  }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>The error.</value>
        public String Error { get { return error; } set { error = value; } }
        /// <summary>
        /// Gets or sets the confirm.
        /// </summary>
        /// <value>The confirm.</value>
        public String Confirm { get { return confirm; } set { confirm = value; } }
    }
}
