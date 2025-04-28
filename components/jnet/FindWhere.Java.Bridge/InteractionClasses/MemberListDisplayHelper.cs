using com.teleca.fleetonline.utils;
using System.Collections;
namespace com.teleca.fleetonline.web.bean
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL/src/java/web_module/src/com/teleca/fleetonline/web/bean/MemberListDisplayHelper.java,v $
    // $Revision: 1.18 $
    // $Date: 2007/12/11 15:00:34 $
    //
    // Copyright : Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************

    ///
    // *
    // 
    [System.Serializable]
    public class MemberListDisplayHelper
    {

        //    *
        //	 * 
        //	 
        private const long serialVersionUID = 1L;
        private static string timeFormatStr = "00";       
        private static FOLProperties folproperties = FOLProperties.getInstance();

        private GroupsAndMembersData data = null;      
        private string foIdStr;
        private int country = -1;
        private bool registered = false;
        private string not_allowed_msg;
        private string m_prefix;
        private bool hideFleetOwner = false;


        public MemberListDisplayHelper()
        {
            this.m_prefix = "0";
        }

        public GroupsAndMembersData Data { get { return data; } set { data = value; } }
    }
}