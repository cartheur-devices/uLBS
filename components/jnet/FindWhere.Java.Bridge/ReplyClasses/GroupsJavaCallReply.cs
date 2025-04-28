using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class GroupsJavaCallReply : JavaCallReply
    {

        private MiscContentHelper m_MiscContentHelper;
        private MapUrlHelper m_MapUrlHelper;
        private ResponseTypeHelper m_ResponseTypeHelper;
        private MapDisplayHelper m_MapDisplayHelper;

        private string error;
        private string confirm;

        public String Error { get { return error; } set { error = value; } }
        public String Confirm { get { return confirm; } set { confirm = value; } }

        public GroupsJavaCallReply()
        {
        }
      
        public MiscContentHelper MiscContentHelper { get { return m_MiscContentHelper; } set { m_MiscContentHelper = value; } }
        public MapUrlHelper MapUrlHelper { get { return m_MapUrlHelper; } set { m_MapUrlHelper = value; } }
        public ResponseTypeHelper ResponseTypeHelper { get { return m_ResponseTypeHelper; } set { m_ResponseTypeHelper = value; } }
        public MapDisplayHelper SmListDisplayHelper { get { return m_MapDisplayHelper; } set { m_MapDisplayHelper = value; } }


    }
}
