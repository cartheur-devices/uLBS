using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    /// <summary>
    /// The server reply for Geocoding.
    /// </summary>
    public class GeoCodingJavaCallReply : JavaCallReply
    {
        private MiscContentHelper m_MiscContentHelper;
        private MapUrlHelper m_MapUrlHelper;
        private ResponseTypeHelper m_ResponseTypeHelper;
        private MapDisplayHelper m_MapDisplayHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoCodingJavaCallReply"/> class.
        /// </summary>
        public GeoCodingJavaCallReply()  { }

        /// <summary>
        /// Gets or sets the misc content helper.
        /// </summary>
        /// <value>The misc content helper.</value>
        public MiscContentHelper MiscContentHelper { get { return m_MiscContentHelper; } set { m_MiscContentHelper = value; } }
        /// <summary>
        /// Gets or sets the map URL helper.
        /// </summary>
        /// <value>The map URL helper.</value>
        public MapUrlHelper MapUrlHelper { get { return m_MapUrlHelper; } set { m_MapUrlHelper = value; } }
        /// <summary>
        /// Gets or sets the response type helper.
        /// </summary>
        /// <value>The response type helper.</value>
        public ResponseTypeHelper ResponseTypeHelper { get { return m_ResponseTypeHelper; } set { m_ResponseTypeHelper = value; } }
        /// <summary>
        /// Gets or sets the sm list display helper.
        /// </summary>
        /// <value>The sm list display helper.</value>
        public MapDisplayHelper SmListDisplayHelper { get { return m_MapDisplayHelper; } set { m_MapDisplayHelper = value; } }

    }
}
