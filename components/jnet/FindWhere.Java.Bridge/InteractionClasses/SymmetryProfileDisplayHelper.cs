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


namespace com.teleca.fleetonline.web.bean
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL511/web_module/src/com/teleca/fleetonline/web/bean/SymmetryProfileDisplayHelper.java,v $
    // $Revision: 1.1 $
    // $Date: 2008/11/25 11:09:06 $
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
    public class SymmetryProfileDisplayHelper
    {

        private ArrayList dataList;

        public ArrayList DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }
        private GroupsAndMembersData groups;

        public GroupsAndMembersData Groups
        {
            get { return groups; }
            set { groups = value; }
        }
        private MemberData[] allMembers;

        public MemberData[] AllMembers
        {
            get { return allMembers; }
            set { allMembers = value; }
        }

        private readonly int[] hour_options = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
        private readonly int[] minute_options = { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };

        public SymmetryProfileDisplayHelper()
        {
        }

        //    *
        //	 * Construct a helper
        //	 * @param dataList the config data to use for outputting
        //	 
        public SymmetryProfileDisplayHelper(ArrayList dataList)
        {
            this.dataList = dataList;
        }

        //    *
        //	 * Construct a helper
        //	 * @param dataList the config data to use for outputting
        //	 
        public SymmetryProfileDisplayHelper(ArrayList dataList, GroupsAndMembersData groups)
        {
        }


        //    *
        //	 * Get the line specified
        //	 * @param data the OcellusProfileData object to output
        //	 * @return String formatted output
        //	 
        private string getLine(SymmetryProfileData data)
        {

            System.Text.StringBuilder result = new System.Text.StringBuilder();

            result.Append("symmetryProfileData[");
            result.Append(data.getRecId());
            result.Append("]=new symmetryProfileObject(");
            result.Append(data.getRecId() + ",");
            result.Append(data.getUserId() + ",\"");
            result.Append(data.getProfileName() + "\",");
            result.Append(booleanize(data.getIsProfile()) + ",");
            result.Append(data.getInterval() + ",");
            result.Append(data.getStartHour() + ",");
            result.Append(data.getStartMinute() + ",");
            result.Append(data.getEndHour() + ",");
            result.Append(data.getEndMinute() + ",");
            result.Append(booleanize(data.getMONDAY()) + ",");
            result.Append(booleanize(data.getTUESDAY()) + ",");
            result.Append(booleanize(data.getWEDNESDAY()) + ",");
            result.Append(booleanize(data.getTHURSDAY()) + ",");
            result.Append(booleanize(data.getFRIDAY()) + ",");
            result.Append(booleanize(data.getSATURDAY()) + ",");
            result.Append(booleanize(data.getSUNDAY()) + ",");
            result.Append(booleanize(data.getBatterySaveMode()) + ",");
            result.Append(data.getSpeed() == null ? "-" : data.getSpeed().ToString() + ");");

            return result.ToString();
        }

        //    *
        //	 * Get the line specified
        //	 * @param data the OcellusProfileData object to output
        //	 * @return String formatted output
        //	 
        private string getLineForDevice(SymmetryProfileData data)
        {

            System.Text.StringBuilder result = new System.Text.StringBuilder();

            result.Append("symmetryProfileByFmId[");
            result.Append(data.getUserId());
            result.Append("]=new symmetryProfileObject(");
            result.Append(data.getRecId() + ",");
            result.Append(data.getUserId() + ",\"");
            result.Append(data.getProfileName() + "\",");
            result.Append(booleanize(data.getIsProfile()) + ",");
            result.Append(data.getInterval() + ",");
            result.Append(data.getStartHour() + ",");
            result.Append(data.getStartMinute() + ",");
            result.Append(data.getEndHour() + ",");
            result.Append(data.getEndMinute() + ",");
            result.Append(booleanize(data.getMONDAY()) + ",");
            result.Append(booleanize(data.getTUESDAY()) + ",");
            result.Append(booleanize(data.getWEDNESDAY()) + ",");
            result.Append(booleanize(data.getTHURSDAY()) + ",");
            result.Append(booleanize(data.getFRIDAY()) + ",");
            result.Append(booleanize(data.getSATURDAY()) + ",");
            result.Append(booleanize(data.getSUNDAY()) + ",");
            result.Append(booleanize(data.getBatterySaveMode()) + ",");
            result.Append(data.getSpeed() == null ? "-" : data.getSpeed().ToString() + ");");

            return result.ToString();
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