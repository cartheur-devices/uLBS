using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using com.teleca.fleetonline.web.bean;
using com.teleca.fleetonline.repository;
using System.Collections;
using System.Xml.XPath;
using com.teleca.fleetonline.businessmanager;
using com.teleca.fleetonline.web.view;
using com.teleca.fleetonline.mapping;
using com.teleca.fleetonline.charging;
using com.teleca.fleetonline.utils;

namespace JNetBridge.InteractionClasses
{
    /// <summary>
    /// Utilities for the bridge.
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Fills the properties of an object from xml 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="InputNode"></param>
        /// <returns></returns>
        public static object FillProperties(Object o, XmlNode InputNode)
        {
            //1st get fields and properties
            FieldInfo[] myFields = o.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] myProperties = o.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            Boolean done = false;
            // Loop the xml nodes and fill the properties / fields
            foreach (XmlNode subNode in InputNode.ChildNodes)
            {
                try
                {
                    done = false;
                    foreach (FieldInfo fi in myFields)
                    {
                        if (subNode.Name == fi.Name)
                        {
                            try
                            {
                                fi.SetValue(o, GetFieldValue(fi.FieldType.ToString(), subNode));
                                done = true;
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("FillProperties, fi.setValue", ex);
                            }
                        }
                    }


                    if (done == false)
                    {
                        foreach (PropertyInfo pi in myProperties)
                        {
                            if (subNode.Name == pi.Name)
                            {
                                try
                                {
                                    pi.SetValue(o, GetFieldValue(pi.PropertyType.ToString(), subNode), null);
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("FillProperties, pi.setValue", ex);
                                }

                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed parsing node:" + subNode.Name, ex);
                }
            }

            return o;
        }
        /// <summary>
        /// Gets the field value.
        /// </summary>
        /// <param name="fieldType">Type of the field.</param>
        /// <param name="subNode">The sub node.</param>
        /// <returns></returns>
        private static object GetFieldValue(string fieldType, XmlNode subNode)
        {
            string value = null;

            switch (fieldType)
            {
                case "System.Boolean":

                    if (subNode.HasChildNodes)
                    {
                        if (subNode.ChildNodes[0].ChildNodes.Count > 0)
                        {
                            return Boolean.Parse(subNode.ChildNodes[0].InnerText);
                        }
                    }
                    return Boolean.Parse(subNode.InnerXml);
                case "System.Nullable`1[System.DateTime]":
                    // TODO: convert "2008-03-29 16:41:50.0 CET" to datetime
                    if (string.IsNullOrEmpty(subNode.InnerXml))
                    {
                        return null;
                    }
                    else
                    {

                        if (subNode.InnerXml.EndsWith("CET")) 
                        {
                            return DateTime.Parse(subNode.InnerXml.Substring(0, subNode.InnerXml.Length - 3)); 
                        }
                        if
                            (subNode.InnerXml.EndsWith("CEST")) 
                        {
                            return DateTime.Parse(subNode.InnerXml.Substring(0, subNode.InnerXml.Length - 4)); 
                        }

                        //if (!string.IsNullOrEmpty(subNode.InnerXml))
                        //{
                            return DateTime.Parse(subNode.InnerXml); 
                        //}
                        //else
                            //return null;

                    }

                case "System.DateTime":
                    //TODO: check DateTime conversion
                    if (subNode.InnerXml.EndsWith("CEST"))
                    {
                        return DateTime.Parse(subNode.InnerXml.Substring(0, subNode.InnerXml.Length - 4));
                    }
                    if (subNode.InnerXml.EndsWith("CET"))
                    {
                        return DateTime.Parse(subNode.InnerXml.Substring(0, subNode.InnerXml.Length - 3));
                    }
                    return DateTime.Parse(subNode.InnerXml);
                case "System.String":

                    if (subNode.HasChildNodes)
                    {
                        if (subNode.ChildNodes[0].ChildNodes.Count > 0)
                            return subNode.ChildNodes[0].InnerText;
                        //<string> sgfsfgsdfg </string>
                        return subNode.FirstChild.Value;

                    }
                    return  subNode.InnerXml;

                case "System.String[]":
                    string[] returnValue = new String[subNode.ChildNodes.Count];
                    for (int i = 0; i < subNode.ChildNodes.Count; i++)
                    {
                        returnValue[i] = subNode.ChildNodes[i].InnerText;
                    }
                    return returnValue;
                case "System.Int32":
                    //TODO replace the ./, withe a better solution!
                    //value = value.Replace(".", ",");
                    if (subNode.HasChildNodes)
                    {
                        if (subNode.ChildNodes[0].ChildNodes.Count > 0)
                        {
                            return Int32.Parse(subNode.ChildNodes[0].InnerText);
                        }
                    }
                    if (System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator == ",")
                    {
                        return Int32.Parse(subNode.InnerXml.Replace(".", ","));
                    }

                    return Int32.Parse(subNode.InnerXml);

                case "System.Int64":
                    //TODO replace the ./, withe a better solution!
                    if (System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator == ",")
                    {
                        return Int64.Parse( subNode.InnerXml.Replace(".", ","));
                    }
                    return Int64.Parse(subNode.InnerXml);
                case "System.Double":
                    //TODO replace the ./, withe a better solution!
                    //value = value.Replace(".", ",");
                    if (System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator == ",")
                    {
                        return Double.Parse( subNode.InnerXml.Replace(".", ","));
                    }
                    return Double.Parse(subNode.InnerXml);
                case "System.Single":
                    //TODO replace the ./, withe a better solution!
                    if (System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator == ",")
                    {
                        return Single.Parse(subNode.InnerXml.Replace(".", ","));
                    }
                    else
                    {
                        return Single.Parse(subNode.InnerXml);
                    }
                    
                case "System.Collections.ArrayList":
                    //TODO: FILL the arraylist!
                    //fi.SetValue(o, Int64.Parse(value));
                    //get arraylist-type from the first node
                    ArrayList al = new ArrayList();


                    if (subNode.HasChildNodes)
                    {
                        string aType = subNode.FirstChild.Name;

                        switch (aType)
                        {
                            case "int":
                                aType = "System.Int32";
                                break;
                            case "string":
                                aType = "System.String";
                                break;
                            default:
                                // aType = aType.Substring(0, 1).ToUpper() + aType.Substring(1);

                                break;

                        }

                        //XmlNodeList nl = subNode.SelectNodes(aType);
                        foreach (XmlNode arrayNode in subNode.ChildNodes)
                        {
                            System.Type oType = System.Type.GetType(aType);
                            object o2 = System.Activator.CreateInstance(oType);

                            //MemberData md = new MemberData();
                            Utils.FillProperties(o2, arrayNode);
                            al.Add(o2);
                        }

                    }
                    return al;

                case "System.Collections.IDictionary":
                    //TODO: FILL the arraylist!
                    //fi.SetValue(o, Int64.Parse(value));
                    //get arraylist-type from the first node
                    // IDictionary id
                    if (subNode.HasChildNodes)
                    {
                        //throw new NotImplementedException("Not Implementated: System.Collections.IDictionary in Utils");
                        //string aType = subNode.ChildNodes[0].ChildNodes[0].Name; // always integer

                        #region bewaren
                        //switch (aType)
                        //{
                        //    case "int":
                        //        aType = "System.Int32";
                        //        break;
                        //    case "string":
                        //        aType = "System.String";
                        //        break;
                        //}
                        #endregion
                        switch (subNode.ChildNodes[0].ChildNodes[1].Name)
                        {
                            case "string":
                                Dictionary<int, string> entries = new Dictionary<int, string>();
                                //XmlNodeList nl = subNode.SelectNodes("entry");
                                foreach (XmlNode arrayNode in subNode.ChildNodes)
                                {
                                    int key = int.Parse(arrayNode.ChildNodes[0].InnerText);
                                    string val = arrayNode.ChildNodes[1].InnerText;

                                    //MemberData md = new MemberData();
                                    //Utils.FillProperties(o2, arrayNode);
                                    //id.Add(o2);
                                    entries.Add(key, val);
                                }
                                return entries;
                            case "linked-list":
                                //type of objects in the dictionary...
                                //TODO: upgrade code!!!! (WERKT NU ALLEEN VOOR MEMBERDATA; werkend maken met reflectie?)
                                //string tst = subNode.ChildNodes[0].ChildNodes[1].ChildNodes[0].Name;

                                Dictionary<int, MemberData[]> entries2 = new Dictionary<int, MemberData[]>();
                                //foreach (XmlNode arrayNode in subNode.SelectNodes("entry")) 
                                foreach (XmlNode arrayNode in subNode.ChildNodes)
                                {
                                    int key = int.Parse(arrayNode.ChildNodes[0].InnerText);

                                    XmlNodeList memNodes = arrayNode.ChildNodes[1].ChildNodes;

                                    MemberData[] memBrs = new MemberData[memNodes.Count];
                                    for (int i = 0; i < memNodes.Count; i++)
                                    {
                                        memBrs[i] = new MemberData();
                                        Utils.FillProperties(memBrs[i], memNodes[i]);
                                    }



                                    //MemberData md = new MemberData();
                                    //Utils.FillProperties(o2, arrayNode);
                                    //id.Add(o2);
                                    entries2.Add(key, memBrs);
                                }
                                return entries2;
                            default:
                                throw new NotImplementedException("IDictionary not implemented:" + subNode.ChildNodes[0].ChildNodes[1].Name);
                        }
                    }

                    break;
                case "com.teleca.fleetonline.web.bean.GroupsAndMembersData":
                    return GroupsAndMembersData.DeSerializeSingle(subNode);
                case "EnforaIO":
                    EnforaIO EIO = new EnforaIO();
                    Utils.FillProperties(EIO, subNode.FirstChild);
                    return EIO;
                case "System.Collections.Generic.List`1[com.teleca.fleetonline.repository.SMData]":
                    return SMData.FromNode(subNode);
                case "com.teleca.fleetonline.repository.UserPositionData[]":
                    //TODO: fill the userposition data
                    return UserPositionData.ArrayFromNode(subNode);
                case "com.teleca.fleetonline.web.bean.SymmetryProfileDisplayHelper":
                    SymmetryProfileDisplayHelper symprodh = new SymmetryProfileDisplayHelper();
                    Utils.FillProperties(symprodh, subNode.FirstChild);
                    return symprodh;
                case "com.teleca.fleetonline.repository.SymConfigData":
                    SymConfigData scd = new SymConfigData();
                    Utils.FillProperties(scd, subNode.FirstChild);
                    return scd;
                case "System.Collections.Hashtable":
                    Hashtable ht = new Hashtable();
                    //foreach (XmlNode entryNode in subNode.ChildNodes[0].SelectNodes("entry")) 
                    foreach (XmlNode entryNode in subNode.ChildNodes[0].ChildNodes)
                    {
                        XmlNode s1 = entryNode.SelectSingleNode("string[1]");
                        XmlNode s2 = entryNode.SelectSingleNode("string[2]");
                        ht.Add(s1.InnerText, s2.InnerText);
                    }
                    return ht;
                case "JNetBridge.Classes.Cells":
                    //XmlNodeList nl = subNode.ChildNodes; //.SelectNodes("string-array");

                    JNetBridge.Classes.Cells c = new JNetBridge.Classes.Cells();
                    c.List = new ArrayList();

                    foreach (XmlNode node in subNode.ChildNodes)
                    {
                        XmlNodeList nodes = node.ChildNodes; //.SelectNodes("string");
                        string[] resuls = new string[nodes.Count];
                        for (int i = 0; i < nodes.Count; i++)
                        {
                            resuls[i] = nodes[i].InnerText;
                        }

                        c.List.Add(resuls);
                    }

                    return c;

                case "JNetBridge.InteractionClasses.UserData":
                    UserData ud = new UserData();
                    Utils.FillProperties(ud, subNode.FirstChild);
                    return ud;
                case "com.teleca.fleetonline.web.bean.AnswerHelper":
                    AnswerHelper ah = new AnswerHelper();
                    Utils.FillProperties(ah, subNode.FirstChild);
                    return ah;
                case "com.teleca.fleetonline.web.bean.MiscContentHelper":
                    MiscContentHelper mch = new MiscContentHelper();
                    Utils.FillProperties(mch, subNode.FirstChild);
                    return mch;
                case "com.teleca.fleetonline.web.bean.SmListDisplayHelper":
                    SmListDisplayHelper sld = new SmListDisplayHelper();
                    sld.DeSerialize(subNode.InnerXml);
                    return sld;
                case "com.teleca.fleetonline.web.view.Definitions":
                    Definitions defs = new Definitions();
                    Utils.FillProperties(defs, subNode);
                    return defs;
                case "com.teleca.fleetonline.web.bean.ResponseTypeHelper":
                    ResponseTypeHelper rth = new ResponseTypeHelper();
                    Utils.FillProperties(rth, subNode.FirstChild);
                    return rth;
                case "com.teleca.fleetonline.web.bean.GeoFenceListDisplayHelper":
                    GeoFenceListDisplayHelper gfdh = new GeoFenceListDisplayHelper();
                    Utils.FillProperties(gfdh, subNode.FirstChild);
                    return gfdh;
                case "com.teleca.fleetonline.web.bean.NotificationListDisplayHelper":
                    NotificationListDisplayHelper ndh = new NotificationListDisplayHelper();
                    Utils.FillProperties(ndh, subNode.FirstChild);
                    return ndh;
                case "com.teleca.fleetonline.web.bean.GeoFenceMembListDisplayHelper":
                    GeoFenceMembListDisplayHelper gmldh = new GeoFenceMembListDisplayHelper();
                    Utils.FillProperties(gmldh, subNode.FirstChild);
                    return gmldh;
                case "com.teleca.fleetonline.web.bean.MapUrlHelper":
                    MapUrlHelper muh = new MapUrlHelper();
                    Utils.FillProperties(muh, subNode.FirstChild);
                    return muh;
                case "com.teleca.fleetonline.web.bean.MapDisplayHelper":
                    MapDisplayHelper mdh = new MapDisplayHelper();
                    Utils.FillProperties(mdh, subNode.FirstChild);
                    return mdh;
                case "com.teleca.fleetonline.web.bean.ZoomLevelHelper":
                    ZoomLevelHelper zlh = new ZoomLevelHelper();
                    Utils.FillProperties(zlh, subNode.FirstChild);
                    return zlh;
                case "com.teleca.fleetonline.web.bean.PositionsDisplayHelper":
                    PositionsDisplayHelper pdh = new PositionsDisplayHelper();
                    Utils.FillProperties(pdh, subNode.FirstChild);
                    return pdh;
                case "com.teleca.fleetonline.web.bean.NotifMsgListDisplayHelper":
                    NotifMsgListDisplayHelper nldh = new NotifMsgListDisplayHelper();
                    Utils.FillProperties(nldh, subNode.FirstChild);
                    return nldh;
                case "com.teleca.fleetonline.utils.FOLProperties":
                    FOLProperties fp = new FOLProperties();
                    Utils.FillProperties(fp, subNode.FirstChild);
                    return fp;
                case "com.teleca.fleetonline.web.bean.OcellusAdditionalVariablesDisplayHelper":
                    OcellusAdditionalVariablesDisplayHelper oavd = new OcellusAdditionalVariablesDisplayHelper();
                    Utils.FillProperties(oavd, subNode.FirstChild);
                    return oavd;
                case "com.teleca.fleetonline.web.bean.MemberListDisplayHelper":
                    MemberListDisplayHelper mdh2 = new MemberListDisplayHelper();
                    Utils.FillProperties(mdh2, subNode.FirstChild);
                    return mdh2;
                case "com.teleca.fleetonline.web.bean.AccountDetailsDisplayHelper":
                    AccountDetailsDisplayHelper adh = new AccountDetailsDisplayHelper();
                    Utils.FillProperties(adh, subNode.FirstChild);
                    return adh;
                case "com.teleca.fleetonline.web.bean.ContactsDisplayHelper":
                    ContactsDisplayHelper cdh = new ContactsDisplayHelper();
                    Utils.FillProperties(cdh, subNode.FirstChild);
                    return cdh;
                case "HelpDisplayHelper":
                    HelpDisplayHelper hdh = new HelpDisplayHelper();
                    Utils.FillProperties(hdh, subNode.FirstChild);
                    return hdh;
                case "com.teleca.fleetonline.web.bean.TimewindowDisplayHelper":
                    TimewindowDisplayHelper twdh = new TimewindowDisplayHelper();
                    Utils.FillProperties(twdh, subNode.FirstChild);
                    return twdh;
                case "System.Collections.Generic.LinkedList`1[com.teleca.fleetonline.repository.ContactData]":
                    LinkedList<ContactData> returnListContactData = new LinkedList<ContactData>();
                    for (int i = 0; i < subNode.ChildNodes[0].ChildNodes.Count; i++)
                    {
                        ContactData cd = new ContactData();
                        Utils.FillProperties(cd, subNode.ChildNodes[0].ChildNodes[i]);
                        returnListContactData.AddLast(cd);
                    }
                    return returnListContactData;

                case "System.Collections.Generic.LinkedList`1[com.teleca.fleetonline.repository.MemberData]":
                    LinkedList<MemberData> returnListMemberData = new LinkedList<MemberData>();
                    if (subNode.HasChildNodes)
                    {
                        for (int i = 0; i < subNode.ChildNodes[0].ChildNodes.Count; i++)
                        {
                            MemberData md = new MemberData();
                            Utils.FillProperties(md, subNode.ChildNodes[0].ChildNodes[i]);
                            returnListMemberData.AddLast(md);
                        }
                    }
                    return returnListMemberData;
                case "System.Collections.Generic.LinkedList`1[com.teleca.fleetonline.repository.MemberBalanceData]":
                    LinkedList<MemberBalanceData> returnListMemberBalanceData = new LinkedList<MemberBalanceData>();
                    if (subNode.HasChildNodes)
                    {
                        for (int i = 0; i < subNode.ChildNodes[0].ChildNodes.Count; i++)
                        {
                            MemberBalanceData mbd = new MemberBalanceData();
                            Utils.FillProperties(mbd, subNode.ChildNodes[0].ChildNodes[i]);
                            returnListMemberBalanceData.AddLast(mbd);
                        }
                    }
                    return returnListMemberBalanceData;
                case "System.Collections.Generic.List`1[com.teleca.fleetonline.web.bean.GroupsAndMembersData]":
                    List<GroupsAndMembersData> retListGroupsAndMembersData = new List<GroupsAndMembersData>();
                    if (subNode.HasChildNodes)
                    {

                        retListGroupsAndMembersData = GroupsAndMembersData.FromNode(subNode);
                    }
                    return retListGroupsAndMembersData;

                case "com.teleca.fleetonline.web.bean.BalanceDisplayHelper":
                    BalanceDisplayHelper bdh = new BalanceDisplayHelper();
                    Utils.FillProperties(bdh, subNode.FirstChild);
                    return bdh;

                case "System.Text.StringBuilder":
                    StringBuilder sb = new StringBuilder();
                    sb.Append(subNode.FirstChild.ToString());
                    return sb;
                case "com.teleca.fleetonline.repository.UserInfoData":
                    UserInfoData uid = new UserInfoData();
                    Utils.FillProperties(uid, subNode);
                    return uid;
                case "com.teleca.fleetonline.repository.WIConfigDBObject":
                    WIConfigDBObject WIO = new WIConfigDBObject();
                    Utils.FillProperties(WIO, subNode.FirstChild);
                    return WIO;
                case "com.teleca.fleetonline.web.bean.OcellusProfileDisplayHelper":
                    OcellusProfileDisplayHelper OPDH = new OcellusProfileDisplayHelper();
                    Utils.FillProperties(OPDH, subNode.FirstChild);
                    return OPDH;
                case "com.teleca.fleetonline.repository.TTConfigDBObject":
                    TTConfigDBObject TTDO = new TTConfigDBObject();
                    Utils.FillProperties(TTDO, subNode.FirstChild);
                    return TTDO;
                case "com.teleca.fleetonline.web.bean.TemplatesDisplayHelper":
                    TemplatesDisplayHelper tdh = new TemplatesDisplayHelper();
                    Utils.FillProperties(tdh, subNode.FirstChild);
                    return tdh;
                case "com.teleca.fleetonline.repository.MemberData[]":
                    //backwards....
                    if (subNode.FirstChild.Name == "com.teleca.fleetonline.repository.MemberData-array")
                    {
                        MemberData[] returnMembers = new MemberData[subNode.FirstChild.ChildNodes.Count];
                        for (int i = 0; i < subNode.FirstChild.ChildNodes.Count; i++)
                        {
                            returnMembers[i] = new MemberData();
                            Utils.FillProperties(returnMembers[i], subNode.FirstChild.ChildNodes[i]);
                        }
                        return returnMembers;
                    }
                    else
                    {

                        MemberData[] returnMembers = new MemberData[subNode.ChildNodes.Count];
                        for (int i = 0; i < subNode.ChildNodes.Count; i++)
                        {
                            returnMembers[i] = new MemberData();
                            Utils.FillProperties(returnMembers[i], subNode.ChildNodes[i]);
                        }
                        return returnMembers;
                    }
                case "EnforaConfigDBObject":
                    EnforaConfigDBObject ECO = new EnforaConfigDBObject();
                    Utils.FillProperties(ECO, subNode.FirstChild);
                    return ECO;
                case "com.teleca.fleetonline.charging.ImplementerDataBean":
                    ImplementerDataBean IDB = new ImplementerDataBean();
                    Utils.FillProperties(IDB, subNode.FirstChild);
                    return IDB;
                case "com.teleca.fleetonline.businessmanager.TTMessageData":
                    TTMessageData tmm = new TTMessageData();
                    Utils.FillProperties(tmm, subNode.FirstChild);
                    return tmm;
                case "com.teleca.fleetonline.repository.GeoFenceMembData":
                    GeoFenceMembData gmd = new GeoFenceMembData();
                    Utils.FillProperties(gmd, subNode.FirstChild);
                    return gmd;
                case "com.teleca.fleetonline.repository.ProfileData[]":
                    //zitten in een vector
                    if (string.Compare(subNode.FirstChild.Name, "vector")==0)
                    {
                        ProfileData[] returnProfiledata = new ProfileData[subNode.ChildNodes[0].ChildNodes.Count];
                        for (int i = 0; i < subNode.FirstChild.ChildNodes.Count; i++)
                        {
                            returnProfiledata[i] = new ProfileData();
                            Utils.FillProperties(returnProfiledata[i], subNode.FirstChild.ChildNodes[i]);
                        }
                        return returnProfiledata;
                    }
                    else throw new NotImplementedException("profilearray");

                case "com.teleca.fleetonline.mapping.LatLong[]":
                    //zitten in een vector
                    if (string.Compare(subNode.FirstChild.Name, "vector")==0)
                    {
                        LatLong[] returnLatLongdata = new LatLong[subNode.ChildNodes[0].ChildNodes.Count];
                        for (int i = 0; i < subNode.FirstChild.ChildNodes.Count; i++)
                        {
                            returnLatLongdata[i] = new LatLong();
                            Utils.FillProperties(returnLatLongdata[i], subNode.FirstChild.ChildNodes[i]);
                        }
                        return returnLatLongdata;
                    }
                    else
                    {
                        LatLong[] returnLatLong = new LatLong[subNode.ChildNodes.Count];
                        for (int i = 0; i < subNode.ChildNodes.Count; i++)
                        {
                            returnLatLong[i] = new LatLong();
                            Utils.FillProperties(returnLatLong[i], subNode.ChildNodes[i]);
                        }
                        return returnLatLong;
                    }
                default:
                    throw new NotImplementedException("Property:" + fieldType + " unknown");

            }
            return null;
        }

        /// <summary>
        /// Seperateds the list from string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string SeperatedListFromString(string[] input)
        {
            if (input == null)
            {
                return "";
            }
            else
            {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < input.Length; i++)
                {
                    if (i > 0)
                        result.Append(",");
                    result.Append(input[i]);
                }
                return result.ToString();
            }
        }

        /// <summary>
        /// Semis the colon seperated list from string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string SemiColonSeperatedListFromString(string[] input)
        {
            if (input == null)
                return "";

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (i > 0)
                    result.Append(";");
                result.Append(input[i]);
            }
            return result.ToString();
        }

        public static string SeperatedListFromString2(string[] input)
        {
            if (input == null)
            {
                return "";
            }
            else
            {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < input.Length; i++)
                {
                    //if (i > 0)
                    //result = result + ";";
                    result.Append(input[i]);
                    result.Append(";");
                }

                return result.ToString();
            }
        }

        public static string SeperatedList2FromString(string[] input)
        {
            if (input == null)
            {
                return "";
            }
            else
            {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < input.Length; i++)
                {
                    if (i > 0)
                    {
                        result.Append(",");
                    }
                    result.Append(input[i]);
                }
                return result.ToString();
            }
        }

        public static string SeperatedListFromInt(int[] input)
        {
            if (input == null)
            {
                return "";
            }
            else
            {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < input.Length; i++)
                {
                    if (i > 0)
                    {
                        result.Append(",");
                    }
                    result.Append(input[i].ToString());
                }
                return result.ToString();
            }
        }

        public static string SemiColonSeperatedListFromInt(int[] input)
        {
            if (input == null)
                return "";

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (i > 0)
                    result.Append(";");
                result.Append(input[i].ToString());
            }
            return result.ToString();
        }
    }
}
