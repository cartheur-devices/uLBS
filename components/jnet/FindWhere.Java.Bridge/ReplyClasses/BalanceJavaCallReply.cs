using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;

namespace JNetBridge.ReplyClasses
{
    public class BalanceJavaCallReply : JavaCallReply
    {
        private BalanceDisplayHelper fleetonline_balance;
        //private MapUrlHelper m_MapUrlHelper;
        //private ResponseTypeHelper m_ResponseTypeHelper;
        //private MapDisplayHelper m_MapDisplayHelper;


        public BalanceJavaCallReply()
        {
        }

        public BalanceDisplayHelper BalanceDisplayHelper { get { return fleetonline_balance; } set { fleetonline_balance = value; } }
        //public MapUrlHelper MapUrlHelper { get { return m_MapUrlHelper; } set { m_MapUrlHelper = value; } }
        //public ResponseTypeHelper ResponseTypeHelper { get { return m_ResponseTypeHelper; } set { m_ResponseTypeHelper = value; } }
        //public MapDisplayHelper SmListDisplayHelper { get { return m_MapDisplayHelper; } set { m_MapDisplayHelper = value; } }

    }
}
