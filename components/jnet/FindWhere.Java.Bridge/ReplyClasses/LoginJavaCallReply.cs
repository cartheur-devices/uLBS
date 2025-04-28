using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;
using System.Net;
using com.teleca.fleetonline.repository;
using com.teleca.fleetonline.charging;

namespace JNetBridge.ReplyClasses
{
    public class LoginJavaCallReply : JavaCallReply
    {
        private int USER_LEVEL;
        private List<GroupsAndMembersData> groupsAndMembers;
        private UserData fleetonline_user;

        private string GLOBAL_ERROR;

        public string Global_Error
        {
            get { return GLOBAL_ERROR; }
            set { GLOBAL_ERROR = value; }
        }

        private Boolean gsm;
        private Boolean trimtrac;
        private Boolean tt15;
        private Boolean ocellus;
        private Boolean enfora;
        private Boolean wi;
        private Boolean M_TT_USER;
        private string map_implementation;
        private Boolean map_labels;
        private Boolean nitro;        

        private ImplementerDataBean implementerDataBean;

        public ImplementerDataBean ImplementerDataBean
        {
            get { return implementerDataBean; }
            set { implementerDataBean = value; }
        }  

        private EnforaIO enforaIO;

        public EnforaIO EnforaIO
        {
            get { return enforaIO; }
            set { enforaIO = value; }
        }

        private Boolean tm3000;

        public Boolean Tm3000
        {
            get { return tm3000; }
            set { tm3000 = value; }
        }

        public Boolean Nitro
        {
            get { return nitro; }
            set { nitro = value; }
        }


        private string showSetup;

        public string ShowSetup
        {
            get { return showSetup; }
            set { showSetup = value; }
        }

        private string notifierRead;

        public string NotifierRead
        {
            get { return notifierRead; }
            set { notifierRead = value; }
        }


        private MiscContentHelper fleetonline_error_content;

        public MiscContentHelper Fleetonline_error_content
        {
            get { return fleetonline_error_content; }
            set { fleetonline_error_content = value; }
        }


        public Boolean Map_labels
        {
            get { return map_labels; }
            set { map_labels = value; }
        }
        private Cookie m_JsessionID;
        private string initial_map_data;

        public LoginJavaCallReply()
        {
        }

        public LoginJavaCallReply(Cookie JsessionID)
        {
            m_JsessionID = JsessionID;
        }

        private LinkedList<MemberBalanceData> memberBalances;

        public LinkedList<MemberBalanceData> MemberBalances
        {
            get { return memberBalances; }
            set { memberBalances = value; }
        }

        public LoginJavaCallReply(Cookie JsessionID, Boolean TT_USER, int level, List<GroupsAndMembersData> group, UserData usrData, String reply, Boolean gsm, Boolean trimtrac, Boolean tt15, Boolean ocellus, Boolean enfora, Boolean wi, string map_implementation, string initialmapdata)
        {
            groupsAndMembers = group;
            level = level;
            Reply = reply;
            fleetonline_user = usrData;
            TT_USER = TT_USER;

            gsm = gsm;
            trimtrac = trimtrac;
            tt15 = tt15;
            ocellus = ocellus;
            enfora = enfora;
            wi = wi;
            map_implementation = map_implementation;
            m_JsessionID = JsessionID;
            initial_map_data = initialmapdata;

        }

        public List<GroupsAndMembersData> Group { get { return groupsAndMembers; } }
        public int Level { get { return USER_LEVEL; } }

        private string userDistance;

        public string UserDistance
        {
            get { return userDistance; }
            set { userDistance = value; }
        }

        public UserData UsrData { get { return fleetonline_user; } }

        public Boolean Gsm { get { return gsm; } }
        public Boolean Trimtrac { get { return trimtrac; } }
        public Boolean Ocellus { get { return ocellus; } }
        public Boolean Enfora { get { return enfora; } }
        public Boolean Tt15 { get { return tt15; } }
        public Boolean TT_USER
        { 
            get 
            {
                return M_TT_USER; 
            }
            set
            {
                M_TT_USER = value;
            }
        }
        public Boolean Wi { get { return wi; } }
        public String Map_implementation { get { return map_implementation; } }
        public String Initial_map_data { get { return initial_map_data; } }
        public Cookie JsessionID { get { return m_JsessionID; } }

        private string error;
        private string confirm;

        public String Error { get { return error; } set { error = value; } }
        public String Confirm { get { return confirm; } set { confirm = value; } }

    }
}
