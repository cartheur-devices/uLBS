using System.Collections;
using com.teleca.fleetonline.repository;
using System.Collections.Generic;
namespace com.teleca.fleetonline.web.bean
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL/src/java/web_module/src/com/teleca/fleetonline/web/bean/OcellusProfileDisplayHelper.java,v $
    // $Revision: 1.4 $
    // $Date: 2007/01/16 14:30:34 $
    //
    // Copyright : Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************

    ///
    // * Helper deals with outputting the timewindows in the correct format
    // * <PRE>
    // * // format: recId, userId, profName, altitude, speed, hae, vae, battery, secInMotion, reportInterval, initCredSms, remCredSms, initCredGprs, remCredGprs, gprsTimeRunning, gpsThrottle, motionSensorFilter, periodicReports, reportIntervalWithoutMotion, reportIntervalWithMotion
    // * occProfData[0]=new Array(2, 3982, "Profile A", 1, 1, 0, 0, 1, 1, 0, 0, 0, );
    // * occProfData[1]=new Array("Jim", 4, 15, true, false,"12");
    // * occProfData[2]=new Array("Mum", 12, 24, false, true,"13");
    // * </PRE>
    // * 
    // 
    public class OcellusProfileDisplayHelper
    {

        private ArrayList dataList;

        public ArrayList DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }
        public GroupsAndMembersData groups;

        public GroupsAndMembersData Groups
        {
            get { return groups; }
            set { groups = value; }
        }
        LinkedList<ContactData> returnListContactData = new LinkedList<ContactData>();

        public LinkedList<ContactData> ReturnListContactData
        {
            get { return returnListContactData; }
            set { returnListContactData = value; }
        }
        private MemberData[] allMembers;

        public MemberData[] AllMembers
        {
            get { return allMembers; }
            set { allMembers = value; }
        }



        private MemberData[] ocellusMembers;

        public MemberData[] OcellusMembers
        {
            get { return ocellusMembers; }
            set { ocellusMembers = value; }
        }

        public OcellusProfileDisplayHelper()
        {
        }

        //    *
        //	 * Construct a helper
        //	 * @param dataList the config data to use for outputting
        //	 
        public OcellusProfileDisplayHelper(ArrayList dataList)
        {
            this.dataList = dataList;
        }

      
        //    *
        //	 * Creates boolean version of int; 0 == false; 1 == true; else == false
        //	 * @param b
        //	 * @return boolean version of b; 0 == false; 1 == true; else == false
        //	 
        private string booleanize(int b)
        {

            switch (b)
            {
                case 0:
                    return "false";
                case 1:
                    return "true";
                default:
                    return "false";
            }
        }
    }
}