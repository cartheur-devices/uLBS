using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JNETBridge.InteractionClasses;
using System.Xml.Serialization;
using System.IO;
using com.teleca.fleetonline.web.bean;
using System.Collections;
using com.teleca.fleetonline.mapping;
using JNETBridge;

namespace LibTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private JNETBridge.Classes.JnetBridgeLoginUnit   m_jSessionCookie = null;

        private void button1_Click(object sender, EventArgs e)
        {
            m_jSessionCookie = new JNETBridge.Classes.JnetBridgeLoginUnit(null, "Fancy");
            
            //JNETBridge.ReplyClasses.LoginJavaCallReply loginJavaCallReply = JNETBridge.LoginJavaCall.DoLogin("0629592233", "M@$ter");
            //JNETBridge.ReplyClasses.LoginJavaCallReply loginJavaCallReply = JNETBridge.LoginJavaCall.DoLogin("0629592233", "M@$ter");
            JNETBridge.ReplyClasses.LoginJavaCallReply loginJavaCallReply = JNETBridge.LoginJavaCall.DoLogin(m_jSessionCookie,"0629592233", "1234567");
            //JNETBridge.ReplyClasses.LoginJavaCallReply loginJavaCallReply = JNETBridge.LoginJavaCall.DoLogin("0629592233", "OnZiN");
            //JNETBridge.ReplyClasses.LoginJavaCallReply loginJavaCallReply = JNETBridge.LoginJavaCall.DoLogin("08732832421", "M@$ter");
            //JNETBridge.ReplyClasses.LoginJavaCallReply loginJavaCallReply = JNETBridge.LoginJavaCall.DoLogin("0629592233", "M@$ter1");

            //JNETBridge.ReplyClasses.LoginJavaCallReply loginJavaCallReply = JNETBridge.LoginJavaCall.DoLogin(m_jSessionCookie, "00447798853347", "brooksid");

            
            label1.Text = "login succeeded: level = " + loginJavaCallReply.Level.ToString();
            textBox1.Text = loginJavaCallReply.Reply;

            // Set Cookie
            m_jSessionCookie.JSESSIONID = loginJavaCallReply.JsessionID;

            tbSessionID.Text = m_jSessionCookie.JSESSIONID.Value;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
            progressBar1.Value = 0;
            progressBar1.Maximum = int.Parse(numericUpDown1.Value.ToString());
            int n = 1;

            while (n < numericUpDown1.Value)
            {
                btnGeofenceAction_Click(this, null);
                progressBar1.Value = n;
                n++;
            }

            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

        }

        private void button3_Click(object sender, EventArgs e)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(UserData));
            UserData ud = new UserData();
            ud.Membertype = 24;
            ud.Uid = "12324";
            StringWriter sw = new StringWriter();
            serializer.Serialize(sw, ud);
            string result = sw.ToString();
            string xmlPrefix = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";
            string usrData = xmlPrefix + "<UserData xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n  <uid>25377</uid>\n  <uidSet>true</uidSet>\n  <msisdn>44629592233</msisdn>\n  <password>testing</password>\n  <membertype>24</membertype>\n  <operator>0</operator>\n  <country>44</country>\n  <active>1</active>\n  <language>en_GB</language>\n  <isUserFo>true</isUserFo>\n  <autoLbs>true</autoLbs>\n  <userTimeout>90</userTimeout>\n  <userType>0</userType>\n  <newProfile>0</newProfile>\n  <currentProfile>0</currentProfile>\n  <timeZoneId>Europe/London</timeZoneId>\n  <onDemand>0</onDemand>\n  <getMispos>1</getMispos>\n  <level>4</level>\n  <geocodingAvailable>true</geocodingAvailable>\n</UserData>";

            StringReader tr = new StringReader(usrData);

            UserData b = (UserData)serializer.Deserialize(tr);
            tr.Close();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GroupsAndMembersData));
            GroupsAndMembersData ud = new GroupsAndMembersData();
            //ud.Active = 1;
            StringWriter sw = new StringWriter();
            serializer.Serialize(sw, ud);
            string result = sw.ToString();
            string xmlPrefix = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";
            string usrData = xmlPrefix + "<com.teleca.fleetonline.repository.GroupsAndMembersData xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><keys /><groupnames /><memberlists /><allMembers><com.teleca.fleetonline.repository.MemberData><foId>25377</foId><userid>28185</userid><alias>2T002014</alias><status>0</status><blocked>false</blocked><msisdn>15137289522</msisdn><availableWeekdays>true</availableWeekdays><availableWeekends>true</availableWeekends><timeStartH>1</timeStartH><timeStartM>0</timeStartM><timeEndH>0</timeEndH><timeEndM>59</timeEndM><timeUTCStartH>0</timeUTCStartH><timeUTCStartM>0</timeUTCStartM><timeUTCEndH>23</timeUTCEndH><timeUTCEndM>59</timeUTCEndM><trackable>true</trackable><userType>10</userType><operatorId>0</operatorId><newProfileNr>0</newProfileNr><curProfileNr>0</curProfileNr><onDemand>0</onDemand><getMisPos>1</getMisPos><iconId>0</iconId><geocodingAvailable>true</geocodingAvailable></com.teleca.fleetonline.repository.MemberData><com.teleca.fleetonline.repository.MemberData><foId>25377</foId><userid>28184</userid><alias>2X000065</alias><status>0</status><blocked>false</blocked><msisdn>15133780146</msisdn><availableWeekdays>true</availableWeekdays><availableWeekends>true</availableWeekends><timeStartH>1</timeStartH><timeStartM>0</timeStartM><timeEndH>0</timeEndH><timeEndM>59</timeEndM><timeUTCStartH>0</timeUTCStartH><timeUTCStartM>0</timeUTCStartM><timeUTCEndH>23</timeUTCEndH><timeUTCEndM>59</timeUTCEndM><trackable>true</trackable><userType>10</userType><operatorId>0</operatorId><newProfileNr>0</newProfileNr><curProfileNr>0</curProfileNr><onDemand>0</onDemand><getMisPos>1</getMisPos><iconId>0</iconId><geocodingAvailable>true</geocodingAvailable></com.teleca.fleetonline.repository.MemberData><com.teleca.fleetonline.repository.MemberData><foId>25377</foId><userid>27932</userid><alias>TE000042</alias><status>0</status><blocked>true</blocked><msisdn>447825387027</msisdn><availableWeekdays>true</availableWeekdays><availableWeekends>true</availableWeekends><timeStartH>1</timeStartH><timeStartM>0</timeStartM><timeEndH>0</timeEndH><timeEndM>59</timeEndM><timeUTCStartH>0</timeUTCStartH><timeUTCStartM>0</timeUTCStartM><timeUTCEndH>23</timeUTCEndH><timeUTCEndM>59</timeUTCEndM><trackable>true</trackable><userType>11</userType><operatorId>0</operatorId><newProfileNr>0</newProfileNr><curProfileNr>0</curProfileNr><onDemand>2</onDemand><getMisPos>1</getMisPos><iconId>0</iconId><geocodingAvailable>true</geocodingAvailable></com.teleca.fleetonline.repository.MemberData><com.teleca.fleetonline.repository.MemberData><foId>25377</foId><userid>27946</userid><alias>TE000043</alias><status>0</status><blocked>false</blocked><msisdn>447825387028_1</msisdn><availableWeekdays>true</availableWeekdays><availableWeekends>true</availableWeekends><timeStartH>1</timeStartH><timeStartM>0</timeStartM><timeEndH>0</timeEndH><timeEndM>59</timeEndM><timeUTCStartH>0</timeUTCStartH><timeUTCStartM>0</timeUTCStartM><timeUTCEndH>23</timeUTCEndH><timeUTCEndM>59</timeUTCEndM><trackable>true</trackable><userType>11</userType><operatorId>6</operatorId><newProfileNr>0</newProfileNr><curProfileNr>0</curProfileNr><onDemand>0</onDemand><getMisPos>1</getMisPos><iconId>0</iconId><geocodingAvailable>true</geocodingAvailable></com.teleca.fleetonline.repository.MemberData><com.teleca.fleetonline.repository.MemberData><foId>25377</foId><userid>28124</userid><alias>V3CJOP</alias><status>0</status><blocked>false</blocked><msisdn>447825387026</msisdn><availableWeekdays>true</availableWeekdays><availableWeekends>true</availableWeekends><timeStartH>1</timeStartH><timeStartM>0</timeStartM><timeEndH>0</timeEndH><timeEndM>59</timeEndM><timeUTCStartH>0</timeUTCStartH><timeUTCStartM>0</timeUTCStartM><timeUTCEndH>23</timeUTCEndH><timeUTCEndM>59</timeUTCEndM><trackable>true</trackable><userType>4</userType><operatorId>0</operatorId><newProfileNr>-3</newProfileNr><curProfileNr>0</curProfileNr><onDemand>0</onDemand><getMisPos>1</getMisPos><iconId>0</iconId><geocodingAvailable>true</geocodingAvailable></com.teleca.fleetonline.repository.MemberData><com.teleca.fleetonline.repository.MemberData><foId>25377</foId><userid>28267</userid><alias>Wouter</alias><status>0</status><blocked>true</blocked><msisdn>31624999350</msisdn><availableWeekdays>true</availableWeekdays><availableWeekends>true</availableWeekends><timeStartH>1</timeStartH><timeStartM>0</timeStartM><timeEndH>0</timeEndH><timeEndM>59</timeEndM><timeUTCStartH>0</timeUTCStartH><timeUTCStartM>0</timeUTCStartM><timeUTCEndH>23</timeUTCEndH><timeUTCEndM>59</timeUTCEndM><trackable>true</trackable><userType>40</userType><operatorId>1</operatorId><newProfileNr>0</newProfileNr><curProfileNr>0</curProfileNr><onDemand>0</onDemand><getMisPos>1</getMisPos><iconId>0</iconId><geocodingAvailable>true</geocodingAvailable></com.teleca.fleetonline.repository.MemberData><com.teleca.fleetonline.repository.MemberData><foId>25377</foId><userid>25377</userid><alias>me</alias><status>0</status><blocked>false</blocked><msisdn>44629592233</msisdn><availableWeekdays>true</availableWeekdays><availableWeekends>true</availableWeekends><timeStartH>1</timeStartH><timeStartM>0</timeStartM><timeEndH>0</timeEndH><timeEndM>59</timeEndM><timeUTCStartH>0</timeUTCStartH><timeUTCStartM>0</timeUTCStartM><timeUTCEndH>23</timeUTCEndH><timeUTCEndM>59</timeUTCEndM><trackable>true</trackable><userType>0</userType><operatorId>0</operatorId><newProfileNr>0</newProfileNr><curProfileNr>0</curProfileNr><onDemand>0</onDemand><getMisPos>1</getMisPos><iconId>0</iconId><geocodingAvailable>true</geocodingAvailable></com.teleca.fleetonline.repository.MemberData></allMembers></com.teleca.fleetonline.repository.GroupsAndMembersData>";

            StringReader tr = new StringReader(usrData);

            UserData b = (UserData)serializer.Deserialize(tr);
            tr.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

            JNETBridge.ReplyClasses.MessagesJavaCallReply MessagesJavaCallReply = JNETBridge.MessagesJavaCall.getTextMessages(m_jSessionCookie, "all", new int[]{1,5,9} , 10, "",false );
            dataGridView1.DataSource = MessagesJavaCallReply.SmListDisplayHelper.AllMessages;            

            textBox1.Text = MessagesJavaCallReply.Reply;


            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

            tbStartTime2.Text = MessagesJavaCallReply.StartTime.ToString("HH:mm:ss.ffffzzz");
            tbEndTime2.Text = MessagesJavaCallReply.EndTime.ToString("HH:mm:ss.ffffzzz");
        }

        private void btnClearCookie_Click(object sender, EventArgs e)
        {
            tbSessionID.Clear();
            m_jSessionCookie = null;
        }

        private void btnGeofenceAction_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

            JNETBridge.ReplyClasses.GeofenceActionJavaCallReply GeofenceActionJavaCallReply = JNETBridge.GeofenceActionJavaCall.GetGeofenceAction(m_jSessionCookie, "GETLIST");
            textBox1.Text = GeofenceActionJavaCallReply.Reply;
            dataGridView1.DataSource = GeofenceActionJavaCallReply.GeoFenceListDisplayHelper.DataList;

            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

        }

        private void btnGetGeofencemembList_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

            JNETBridge.ReplyClasses.GeofenceMembListJavaCallReply GeofenceMembListnJavaCallReply = JNETBridge.GeofenceMembListJavaCall.GetGeofenceMembList(m_jSessionCookie);
            textBox1.Text = GeofenceMembListnJavaCallReply.Reply;
            dataGridView1.DataSource = GeofenceMembListnJavaCallReply.GeoFenceMembListDisplayHelper.DataList;
            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

        }

        private void btnGeofenceMemberListADD_Click(object sender, EventArgs e)
        {
            //String fmIdLst = "27932";
            string[] fmIdLst = new string[] { "27932" };

            String fenceId = "927";
            string t = DateTime.Now.ToString();
            DateTime startDate = DateTime.Parse("01-05-2008 5:30");
            DateTime endDate = DateTime.Parse("02-05-2009 6:30");

            string starthr = startDate.ToString("dd");
            //int startHr = 1;
            //int startMin = 12;


            //int endHr = 2;
            //int endMin = 5;


            int fType = 2;
            int alertOnce = 2;
            Boolean deviceBased = false;

            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

//            JNETBridge.ReplyClasses.GeofenceMembListJavaCallReply getGeofenceMembListnJavaCallReply = JNETBridge.GeofenceMembListJavaCall.ADDMember(m_jSessionCookie, fmIdLst, fenceId, startDate, endDate, fType, alertOnce, deviceBased);
            //textBox1.Text = getGeofenceMembListnJavaCallReply.Reply;

            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

        }



        private void btnGeofenceMemberListDEL_Click(object sender, EventArgs e)
        {
            string[] fenceidlst = new string[] { "1466" };
            Boolean deviceBased = true;

            JNETBridge.ReplyClasses.GeofenceMembListJavaCallReply getGeofenceMembListnJavaCallReply = JNETBridge.GeofenceMembListJavaCall.DeleteMember(m_jSessionCookie, fenceidlst, deviceBased);
            textBox1.Text = getGeofenceMembListnJavaCallReply.Reply;

            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

            JNETBridge.ReplyClasses.NotificationJavaCallReply NotificationJavaCallReply = JNETBridge.NotificationJavaCall.GetNotifications(m_jSessionCookie,-1,"",false, "all");
            com.teleca.fleetonline.repository.NotificationData[] notifications = (com.teleca.fleetonline.repository.NotificationData[])NotificationJavaCallReply.NotificationListDisplayHelper.DataList.ToArray(typeof(com.teleca.fleetonline.repository.NotificationData));

            dataGridView1.DataSource = NotificationJavaCallReply.NotificationListDisplayHelper.DataList;
            textBox1.Text = NotificationJavaCallReply.Reply;

            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

        }

        private void StoreNotification_Click(object sender, EventArgs e)
        {


            //String eventType = "1";
            //int sendByEmail = 4;
            //int sendBySms = 4;
            //int[] emailList = new int[]{1,2,3,4};
            //int[] smsList = new int[] { 1, 2, 3, 4 };
            //String[] memberList = new string[] { "28184","28267" }; //"28184","28267"
            //String fmId = "0";
            //int alertOnceStr = -1;


            //tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

            //JNETBridge.ReplyClasses.NotificationJavaCallReply NotificationJavaCallReply = JNETBridge.NotificationJavaCall.StoreNotidication(m_jSessionCookie, eventType, emailList, smsList, memberList, fmId, alertOnceStr);
            //textBox1.Text = NotificationJavaCallReply.Reply;


          
            //tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
        }

        private void btnGetPositions_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

            DateTime fromDate = DateTime.Parse("20-08-2008 5:30");
            DateTime toDate = DateTime.Parse("02-10-2008 6:30");

            //var queryString = "";
            //queryString += "type=historical";
            //queryString += "&mapWidth=" + mapWidth;
            //queryString += "&mapHeight=" + mapHeight;
            //queryString += "&member=all";
            //var requestUrl = getLastKnownPositionUrl + "?" + queryString;
            //request(requestUrl);   

            JNETBridge.ReplyClasses.LastPositionJavaCallReply PositionsJavaCallReply = JNETBridge.LastPositionJavaCall.GetLastKnownPositions(m_jSessionCookie,JNETBridge.LastPositionJavaCall.postionType.historical ,new string[] { "28267" },false);
            textBox1.Text = PositionsJavaCallReply.Reply;
            if (PositionsJavaCallReply.Fleetonline_error_content !=null)
            MessageBox.Show(PositionsJavaCallReply.Fleetonline_error_content.Content );

            //JNETBridge.ReplyClasses.InterfaceJavaCallReply irep = JNETBridge.InterfaceJavaCall.getReply(m_jSessionCookie, "getLbsResult.do");

            //textBox1.Text = irep.Reply;
            if (PositionsJavaCallReply.MapDisplayHelper!=null)
            dataGridView1.DataSource = PositionsJavaCallReply.MapDisplayHelper.AllPos;
            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

        }

        private void btngetPositionsList_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

            DateTime fromDate = DateTime.Parse("01-05-2008 5:30");
            DateTime toDate = DateTime.Parse("02-05-2009 6:30");
        

            JNETBridge.ReplyClasses.ListPositionJavaCallReply ListPositionsJavaCallReply = JNETBridge.ListPositionsJavaCall.RequestPositionList(m_jSessionCookie, 500, "", true, "all");
            dataGridView1.DataSource = ListPositionsJavaCallReply.PositionsDisplayHelper.AllPositions;
            dataGridView1.Refresh();


            textBox1.Text = ListPositionsJavaCallReply.Reply;
            
            
            //double  spd = requestData[0].getLocationX();
            //double spd2 = requestData[0].LocationX ;
            tbStartTime2.Text = ListPositionsJavaCallReply.StartTime.ToString("HH:mm:ss.ffffzzz");
            tbEndTime2.Text = ListPositionsJavaCallReply.EndTime.ToString("HH:mm:ss.ffffzzz");

            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

        }

        private void btnrequestNotificationMsg_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

            DateTime fromDate = DateTime.Parse("01-05-2008 5:30");
            DateTime toDate = DateTime.Parse("02-05-2009 6:30");

            //var isUpdate = (isUpdateOnly) ? true : false;
            //var queryString = "isUpdate=" + isUpdate;
            //for (i = 0; i < newlyReadNotifMsg.length; i++)
            //{
            //    queryString += "&msgId=" + newlyReadNotifMsg[i];
            //}
            //var requestUrl = getNotificationMsgUrl + "?" + queryString + "&screenwidth=" + screen.width + "&listSize=" + commCenterSize + "&orderby=" + curSortColNotif + "&desc=" + sortDesc + "&user=" + escape(userFilter);
            //request(requestUrl);
            //document.getElementById("commCenterPanelNotificationMsgContent").innerHTML = "<br><br><br><center><img src='images/icons/clock.gif'></center>";


            JNETBridge.ReplyClasses.NotificationMsgJavaCallReply NotificationMsgJavaCallReply = JNETBridge.NotificationMsgJavaCall.RequestNotificationMsg (m_jSessionCookie, "false",  200,"", false, "all");
            dataGridView1.DataSource = NotificationMsgJavaCallReply.NotifMsgListDisplayHelper.dataList;
            dataGridView1.Refresh();
            textBox1.Text = NotificationMsgJavaCallReply.Reply;
            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
        }

        private void btnGetVars_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

            DateTime fromDate = DateTime.Parse("01-05-2008 5:30");
            DateTime toDate = DateTime.Parse("02-05-2009 6:30");

            JNETBridge.ReplyClasses.GetVarsJavaCallReply getVarsJavaCallReply = JNETBridge.GetVarsJavaCall.DoLogin(m_jSessionCookie);
            string urlImages = getVarsJavaCallReply.FOLProperties.getProperty("url.images");
            textBox1.Text = getVarsJavaCallReply.Reply;

            string[] strAvailibleSizes = getVarsJavaCallReply.FOLProperties.getProperty("label.CommCenterAvailibleSizes").Split(',');
            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
        }

        private void btnrequestOcellusAddVars_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

            DateTime fromDate = DateTime.Parse("01-05-2008 5:30");
            DateTime toDate = DateTime.Parse("02-05-2009 6:30");

            //var isUpdate = (isUpdateOnly) ? true : false;
            //var queryString = "isUpdate=" + isUpdate;
            //for (i = 0; i < newlyReadNotifMsg.length; i++)
            //{
            //    queryString += "&msgId=" + newlyReadNotifMsg[i];
            //}
            //var requestUrl = getNotificationMsgUrl + "?" + queryString + "&screenwidth=" + screen.width + "&listSize=" + commCenterSize + "&orderby=" + curSortColNotif + "&desc=" + sortDesc + "&user=" + escape(userFilter);
            //request(requestUrl);
            //document.getElementById("commCenterPanelNotificationMsgContent").innerHTML = "<br><br><br><center><img src='images/icons/clock.gif'></center>";


            JNETBridge.ReplyClasses.OcellusAddVarsJavaCallReply OcellusAddVarsReply = JNETBridge.OcellusAddVarsJavaCall.RequestOcellusAddVars(m_jSessionCookie, 300, "", false, new string[] { "all" });
            dataGridView1.DataSource = OcellusAddVarsReply.OcellusAdditionalVariablesDisplayHelper.AllAddVars;
            textBox1.Text = OcellusAddVarsReply.Reply;
            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
        }

        private void btngetTeams_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
            JNETBridge.ReplyClasses.MemberListJavaCallReply members = JNETBridge.MemberListJavaCall.GetTeams(m_jSessionCookie);
            //string urlImages = getVarsJavaCallReply.FOLProperties.getProperty("url.images");
            textBox1.Text = members.Reply;
            dataGridView1.DataSource = members.MemberListDisplayHelper.Data.AllMembers ;
            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //JNETBridge.ReplyClasses.LoginJavaCallReply loginJavaCallReply = JNETBridge.LoginJavaCall.DoLogin("0629592233", "M@$ter");

            //System.Net.Cookie Koekkie =  loginJavaCallReply.JsessionID;

            //JNETBridge.ReplyClasses.GetVarsJavaCallReply getVarsJavaCallReply = JNETBridge.GetVarsJavaCall.DoLogin(Koekkie );

            
        }

        private void btnGetMembers_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
            JNETBridge.ReplyClasses.MemberListJavaCallReply  members = JNETBridge.MemberListJavaCall.GetMembers(m_jSessionCookie);
            //string urlImages = getVarsJavaCallReply.FOLProperties.getProperty("url.images");
            textBox1.Text = members.Reply;
            dataGridView1.DataSource = members.MemberListDisplayHelper.Data.AllMembers ;
            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
            JNETBridge.ReplyClasses.GroupsJavaCallReply grpReply = JNETBridge.GroupsJavaCall.AddGroup(m_jSessionCookie, textBox2.Text, new string[] { "28184","28267"  });

            textBox1.Text = grpReply.Reply;
            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
        }

        private void btnChangeGroup_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
            JNETBridge.ReplyClasses.GroupsJavaCallReply grpReply = JNETBridge.GroupsJavaCall.EditGroup(m_jSessionCookie, 0, new string[] { "27694" });
            textBox1.Text = grpReply.Reply;
            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
        }

        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
            JNETBridge.ReplyClasses.GroupsJavaCallReply grpReply = JNETBridge.GroupsJavaCall.DeleteGroup(m_jSessionCookie, 0);
            textBox1.Text = grpReply.Reply;
            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
            JNETBridge.ReplyClasses.GeoCodingJavaCallReply grpReply = JNETBridge.GeoCodingJavaCall.Change(m_jSessionCookie, JNETBridge.GeoCodingJavaCall.GeoSwitch.on, new string[] { "31629592233" });
            textBox1.Text = grpReply.Reply;
            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
            //JNETBridge.ReplyClasses.MemberJavaCallReply grpReply = JNETBridge.MemberJavaCall.SaveMember(m_jSessionCookie, 27694, "Test");
            //textBox1.Text = grpReply.Reply;
            //tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

            DateTime fromDate = DateTime.Parse("01-05-2008 5:30");
            DateTime toDate = DateTime.Parse("02-05-2009 6:30");


            JNETBridge.ReplyClasses.LastPositionJavaCallReply LastPositionsJavaCallReply = JNETBridge.LastPositionJavaCall.RequestPosition(m_jSessionCookie,Int32.Parse(tbPosition.Text) );
            dataGridView1.DataSource = LastPositionsJavaCallReply.MapDisplayHelper.AllPos;
            dataGridView1.Refresh();


            textBox1.Text = LastPositionsJavaCallReply.Reply;


            //double  spd = requestData[0].getLocationX();
            //double spd2 = requestData[0].LocationX ;



            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.InterfaceJavaCallReply reply = JNETBridge.InterfaceJavaCall.Positioningdo(m_jSessionCookie, "previous", 539770, 600, 480,null, null, null, null, null);
            textBox1.Text = reply.Reply;
            string s = reply.Reply.Substring(0,reply.Reply.IndexOf("<?xml version")) + reply.Reply.Substring(reply.Reply.IndexOf("/main")+6);
            textBox1.Text = s;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.AccountDetailsJavaCallReply reply = JNETBridge.AccountDetailsJavaCall.Get(m_jSessionCookie);
            textBox1.Text = reply.Reply;
            //string s = reply.Reply.Substring(0, reply.Reply.IndexOf("<?xml version")) + reply.Reply.Substring(reply.Reply.IndexOf("/main") + 6);
            //textBox1.Text = s;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.PopupNotificationJavaCallReply reply = JNETBridge.PopupNotificationJavaCall.GetNotifications(m_jSessionCookie, true, "undefined");
            textBox1.Text = reply.Reply;
            string s = reply.Reply.Substring(0, reply.Reply.IndexOf("<?xml version")) + reply.Reply.Substring(reply.Reply.IndexOf("/main") + 6);
            textBox1.Text = s;
        }

        private void btnGetBalance_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.BalanceJavaCallReply reply = JNETBridge.BalanceJavaCall.Get(m_jSessionCookie, "01-05-2007", "01-12-2008", 28267, "Wouter");
            textBox1.Text = reply.Reply;
        }

        private void btnEnforaConfig_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.EnforaJavaCallReply reply = JNETBridge.EnforaJavaCall.GetConfig(m_jSessionCookie);
            textBox1.Text = reply.Reply;
        }

        private void btnWIconfig_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.WIJavaCallReply reply = JNETBridge.WIJavaCall.GetConfig(m_jSessionCookie);
            textBox1.Text = reply.Reply;
        }

        private void btnOcellusConfig_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.OcellusJavaCallReply  reply = JNETBridge.OcellusJavaCall.GetConfig(m_jSessionCookie);
            textBox1.Text = reply.Reply;
        }

        private void btnTTConfig_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.TTJavaCallReply reply = JNETBridge.TTJavaCall.GetConfig(m_jSessionCookie, true);
            textBox1.Text = reply.Reply;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int fmid = int.Parse(tbTTID.Text);
            JNETBridge.ReplyClasses.TTJavaCallReply reply = JNETBridge.TTJavaCall.GetConfig(m_jSessionCookie, fmid,3);
            textBox1.Text = reply.Reply;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.OcellusJavaCallReply reply = JNETBridge.OcellusJavaCall.Set(m_jSessionCookie, -11, new string[]{"28124"});
            textBox1.Text = reply.Reply;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //JNETBridge.ReplyClasses.LastPositionJavaCallReply reply = JNETBridge.LastPositionJavaCall.GetPositions(m_jSessionCookie, "28267", 10, "01-07-2006", "26-07-2008");
            JNETBridge.ReplyClasses.LastPositionJavaCallReply reply = JNETBridge.LastPositionJavaCall.GetPositions(m_jSessionCookie, "28267", 10, null, "26-07-2008");
            textBox1.Text = reply.Reply;
        }

        private void button16_Click(object sender, EventArgs e)
        {
//            JNETBridge.ReplyClasses.LoginJavaCallReply loginJavaCallReply = JNETBridge.LoginJavaCall.DoLogin("0629592233", "M@$ter");
//            label1.Text = "login succeeded: level = " + loginJavaCallReply.Level.ToString();
//            textBox1.Text = loginJavaCallReply.Reply;

//            JNETBridge.ReplyClasses.LoginJavaCallReply jrep = new JNETBridge.ReplyClasses.LoginJavaCallReply();

//            string reply = loginJavaCallReply.Reply;
//            int pos2 = reply.IndexOf("<?xml");

//            string xml = reply.Substring(pos2);
////
//            //Xstream.Core.XStream xs = new Xstream.Core.XStream();

//            //string voorbeeldxml = xs.ToXml(loginJavaCallReply);

            

//            //jrep = (JNETBridge.ReplyClasses.LoginJavaCallReply)xs.FromXml(xml);
           
        }

        private void button17_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.OcellusProfilesJavaCallReply reply = JNETBridge.OcellusProfilesJavaCall.Get(m_jSessionCookie);
            textBox1.Text = reply.Reply;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.TTProfileJavaCallReply reply = JNETBridge.TTProfileJavaCall.Get(m_jSessionCookie);
            textBox1.Text = reply.Reply;

        }

        private void button19_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.MemberDetailsJavaCallReply  reply = JNETBridge.MemberDetailsJavaCall.Get(m_jSessionCookie);
            textBox1.Text = reply.Reply;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

            DateTime fromDate = DateTime.Parse("01-05-2008 5:30");
            DateTime toDate = DateTime.Parse("02-05-2009 6:30");

            JNETBridge.ReplyClasses.TemplatesJavaCallReply getTemplatesJavaCallReply = JNETBridge.TemplatesJavaCall.Get(m_jSessionCookie);
            //string urlImages = getTemplatesJavaCallReply.FOLProperties.getProperty("url.images");
            textBox1.Text = getTemplatesJavaCallReply.Reply;

            //string[] strAvailibleSizes = getVarsJavaCallReply.FOLProperties.getProperty("label.CommCenterAvailibleSizes").Split(',');
            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            tbStartTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

            JNETBridge.ReplyClasses.MessagesJavaCallReply MessagesJavaCallReply = JNETBridge.MessagesJavaCall.DeleteTextMessage(m_jSessionCookie, 123456);
            //dataGridView1.DataSource = MessagesJavaCallReply.SmListDisplayHelper.AllMessages;

            textBox1.Text = MessagesJavaCallReply.Reply;


            tbEndTime.Text = DateTime.Now.ToString("HH:mm:ss.ffffzzz");

            tbStartTime2.Text = MessagesJavaCallReply.StartTime.ToString("HH:mm:ss.ffffzzz");
            tbEndTime2.Text = MessagesJavaCallReply.EndTime.ToString("HH:mm:ss.ffffzzz");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //TODO: create a new call to get only one fence from java
            JNETBridge.ReplyClasses.GeofenceActionJavaCallReply reply = JNETBridge.GeofenceActionJavaCall.GetGeofenceAction(m_jSessionCookie , "GETLIST");
            com.teleca.fleetonline.repository.GeoFenceData gfdSelected = null;

            ArrayList list = new ArrayList();
            foreach (com.teleca.fleetonline.repository.GeoFenceData gfd in reply.GeoFenceListDisplayHelper.DataList)
            {
                if (927 == int.Parse(gfd.FenceId)) { }
                //FillGeoFenceArray(gfd, list);
                gfdSelected = gfd;
            }

            // get the mapdata for the selected geofence
            if (gfdSelected != null)
                reply = JNETBridge.GeofenceActionJavaCall.getMap(m_jSessionCookie , gfdSelected.PostCode, gfdSelected.Radius, gfdSelected.LocType, gfdSelected.LocationX, gfdSelected.LocationY, 0);
            LatLong[] CooordinatenVoorRenepaul = reply.Fleetonline_trace_data.GeofenceCornerCoordinates;

            int zoomlevel = reply.Fleetonline_zoom_level.Content;
            textBox1.Text = reply.Reply;
            //if (reply.GeoFenceListDisplayHelper.DataList.Count != 0)
            //{
                //FillGeoFenceArray(reply, list);
            //}
            //return list;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.TTProfileJavaCallReply reply = JNETBridge.TTProfileJavaCall.deleteRelation(m_jSessionCookie, new string[] { "27932" });

          

        }

        private void button24_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.HelpJavaCallReply reply = JNETBridge.HelpJavaCall.GetHelp (m_jSessionCookie, "2.1.1");
            textBox1.Text = reply.Fleetonline_help.getHelpContent();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            string name ="Jurriaan";
            double radius = 0;
            int loctype = 1;
            double locx = 0;
            double locy = 0;

        //    JNETBridge.ReplyClasses.GeofenceActionJavaCallReply reply = GeofenceActionJavaCall.GetGeofenceAction(m_jSessionCookie, GeofenceActionJavaCall.GeofenceAction.STORE, -1, name, null, radius, loctype, locx, locy);

         JNETBridge.ReplyClasses.GeofenceActionJavaCallReply reply = GeofenceActionJavaCall.GetGeofenceAction(m_jSessionCookie, GeofenceActionJavaCall.GeofenceAction.STORE, 99999, name, null, 0, loctype, 0, 0);
         if (reply.AnswerHelper.get() == "success")
             textBox1.Text = "";
         else
             textBox1.Text = "";
        }

        private void button26_Click(object sender, EventArgs e)
        {
            //JNETBridge.ReplyClasses.ForgotEmailJavaCallReply rep = JNETBridge.ForgotEmailJavaCall.SendEmail("jurriaan.struijk@findwhere.com", 1);
            //textBox1.Text = rep.Reply;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            // 552375
            JNETBridge.ReplyClasses.GetReverseGeoceodeJavaCallReply rep = JNETBridge.GetReverseeGeocodeJavaCall.getAddress(m_jSessionCookie, 552375);
            textBox1.Text = rep.Reply;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.SymmetrySetProfileJavaCallReply rep = JNETBridge.SymmetrySetProfileJavaCall.Get(m_jSessionCookie);
            textBox1.Text = rep.Reply;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            JNETBridge.ReplyClasses.SymmetryJavaCallReply rep = JNETBridge.SymmetryJavaCall.GetConfig(m_jSessionCookie);
            textBox1.Text = rep.Reply;
        }
    }
}
