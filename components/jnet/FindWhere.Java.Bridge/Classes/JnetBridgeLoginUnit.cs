using System;
using System.Collections.Generic;
using System.Text;

namespace JNetBridge.Classes
{
    /// <summary>
    /// The login module for the bridge.
    /// </summary>
    [System.Serializable]
    public class JnetBridgeLoginUnit
    {
        public JnetBridgeLoginUnit()  { }

        /// <summary>
        /// Initializes a new instance of the <see cref="JnetBridgeLoginUnit"/> class.
        /// </summary>
        /// <param name="JSESSIONID">The JSESSIONID.</param>
        /// <param name="WhiteLabelName">Name of the white label.</param>
        public JnetBridgeLoginUnit(System.Net.Cookie JSESSIONID, String WhiteLabelName)
        {
            m_JSESSIONID = JSESSIONID;
            m_WhiteLabelName = WhiteLabelName;
        }

        private System.Net.Cookie m_JSESSIONID;
        private String m_WhiteLabelName;

        /// <summary>
        /// Gets or sets the JSESSIONID.
        /// </summary>
        /// <value>The JSESSIONID.</value>
        public System.Net.Cookie JSESSIONID { get { return m_JSESSIONID; } set { m_JSESSIONID = value; } }
        /// <summary>
        /// Gets or sets the name of the white label.
        /// </summary>
        /// <value>The name of the white label.</value>
        public String WhiteLabelName { get { return m_WhiteLabelName; } set { m_WhiteLabelName = value; } }
    }
}
