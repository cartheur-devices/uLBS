using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using com.teleca.fleetonline.repository;

namespace FindWhere.UserControls
{
    public partial class UserControl_MembersPanel : System.Web.UI.UserControl
    {

        public void LoadGroupMembers(int groupId)
        {
            cblMembers.Items.Clear();
            List<com.teleca.fleetonline.web.bean.GroupsAndMembersData> gmds = (List<com.teleca.fleetonline.web.bean.GroupsAndMembersData>)Session["UsrGroup"];
            List<MemberData> gmdsGroup = new List<MemberData>();

            com.teleca.fleetonline.web.bean.GroupsAndMembersData gmd = gmds[0];

            //if (gmd.Groupnames.Contains(groupId))
            //{

            for (int i = 0; i < gmd.AllMembers.Count; i++)
            {
                MemberData md = (MemberData)gmd.AllMembers[i];
                if (md.UserType != 0)
                    cblMembers.Items.Add(new ListItem(md.Alias, md.Userid.ToString()));
            }

            for (int i = 0; i < gmd.AllMembers.Count; i++)
            {
                MemberData md = (MemberData)gmd.AllMembers[i];
                if (gmd.Memberlists != null)
                {
                    foreach (KeyValuePair<int, com.teleca.fleetonline.repository.MemberData[]> kvpMemList in (Dictionary<int, com.teleca.fleetonline.repository.MemberData[]>)gmd.Memberlists)
                    {
                        //selected group
                        if (kvpMemList.Key == groupId)
                        {
                            foreach (com.teleca.fleetonline.repository.MemberData mdd in kvpMemList.Value)
                            {
                                // TODO: repair...
                                //if (mdd.UserType != 0) --> bug in the JAVA-layer; members in the memberlist don't have a usertype?????
                                //{
                                if (md.Userid == mdd.Userid)
                                {
                                    //checkbox selecteren:
                                    for (int cnt = 0; cnt < cblMembers.Items.Count; cnt++)
                                    {
                                        if (cblMembers.Items[cnt].Value == md.Userid)
                                        {
                                            cblMembers.Items[cnt].Selected = true;
                                        }
                                    }
                                }
                                //}
                            }
                        }

                    }
                }
                //}
            }
        }



        public string[] GetSelectedMembers()
        {
            ArrayList alusers = new ArrayList();
            for (int i = 0; i < cblMembers.Items.Count; i++)
            {
                if (cblMembers.Items[i].Selected)
                    alusers.Add(cblMembers.Items[i].Value);
            }

            string[] retStringArrray = new string[alusers.Count];
            alusers.CopyTo(retStringArrray);
            return retStringArrray;

        }

        public void LoadMembersWithDeviceTypes(string[] deviceTypes)
        {
            ArrayList DeviceTyes = new ArrayList();
            foreach (string s in deviceTypes)
            {
                DeviceTyes.Add(s);
            }

            cblMembers.Items.Clear();
            List<com.teleca.fleetonline.web.bean.GroupsAndMembersData> gmds = (List<com.teleca.fleetonline.web.bean.GroupsAndMembersData>)Session["UsrGroup"];
            List<MemberData> gmdsGroup = new List<MemberData>();

            com.teleca.fleetonline.web.bean.GroupsAndMembersData gmd = gmds[0];

            //if (gmd.Groupnames.Contains(groupId))
            //{

            for (int i = 0; i < gmd.AllMembers.Count - 1; i++)
            {
                MemberData md = (MemberData)gmd.AllMembers[i];
                if (((IList)deviceTypes).Contains(md.UserType.ToString()))
                {
                    if (md.UserType != 0)
                        cblMembers.Items.Add(new ListItem(md.Alias, md.Userid.ToString()));
                }
            }

            //for (int i = 0; i < gmd.AllMembers.Capacity - 1; i++)
            //{
            //    MemberData md = (MemberData)gmd.AllMembers[i];
            //    foreach (KeyValuePair<int, com.teleca.fleetonline.repository.MemberData[]> kvpMemList in (Dictionary<int, com.teleca.fleetonline.repository.MemberData[]>)gmd.Memberlists)
            //    {
            //        //selected group
            //        if (kvpMemList.Key == groupId)
            //        {
            //            foreach (com.teleca.fleetonline.repository.MemberData mdd in kvpMemList.Value)
            //            {
            //                if (md.Userid == mdd.Userid)
            //                {
            //                    cblMembers.Items[i].Selected = true;
            //                }
            //            }
            //        }

            //    }
            //    //}
            //}

        }

    }
}

