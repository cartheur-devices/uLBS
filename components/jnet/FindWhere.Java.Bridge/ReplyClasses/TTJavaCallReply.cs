using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;
using com.teleca.fleetonline.repository;

namespace JNetBridge.ReplyClasses
{
    public class TTJavaCallReply : JavaCallReply
    {
        private int level;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        private string dmp;

        public string Dmp
        {
            get { return dmp; }
            set { dmp = value; }
        }
        private string dsto;

        public string Dsto
        {
            get { return dsto; }
            set { dsto = value; }
        }
        private string dsrm;

        public string Dsrm
        {
            get { return dsrm; }
            set { dsrm = value; }
        }
        private ResponseTypeHelper fleetonline_request_type;

        public ResponseTypeHelper Fleetonline_request_type
        {
            get { return fleetonline_request_type; }
            set { fleetonline_request_type = value; }
        }
        private string dsvValue;

        public string DsvValue
        {
            get { return dsvValue; }
            set { dsvValue = value; }
        }
        private string dlpagOnOff;

        public string DlpagOnOff
        {
            get { return dlpagOnOff; }
            set { dlpagOnOff = value; }
        }
        private string dlpa;

        public string Dlpa
        {
            get { return dlpa; }
            set { dlpa = value; }
        }
        private string drtmValue;

        public string DrtmValue
        {
            get { return drtmValue; }
            set { drtmValue = value; }
        }
        private GeoFenceMembData geofence1;

        public GeoFenceMembData Geofence11
        {
            get { return geofence1; }
            set { geofence1 = value; }
        }
        private string dlpag;

        public string Dlpag
        {
            get { return dlpag; }
            set { dlpag = value; }
        }
        private string drtmOnOff;

        public string DrtmOnOff
        {
            get { return drtmOnOff; }
            set { drtmOnOff = value; }
        }
        private string timeZone;

        public string TimeZone
        {
            get { return timeZone; }
            set { timeZone = value; }
        }
        private LinkedList<MemberData> memberList;

        public LinkedList<MemberData> MemberList
        {
            get { return memberList; }
            set { memberList = value; }
        }
        private Boolean tt15;

        public Boolean Tt15
        {
            get { return tt15; }
            set { tt15 = value; }
        }
        private TTConfigDBObject dbObject;

        //public TTConfigDBObject dbObject
        //{
        //    get { return dbObject; }
        //    set { dbObject = value; }
        //}
        private string dmpa;

        public string Dmpa
        {
            get { return dmpa; }
            set { dmpa = value; }
        }
        private string dlpagr;

        public string Dlpagr
        {
            get { return dlpagr; }
            set { dlpagr = value; }
        }
        private string dmo;

        public string Dmo
        {
            get { return dmo; }
            set { dmo = value; }
        }
        private string tt15menu;

        public string Tt15menu
        {
            get { return tt15menu; }
            set { tt15menu = value; }
        }
        private string dsvOnOff;

        public string DsvOnOff
        {
            get { return dsvOnOff; }
            set { dsvOnOff = value; }
        }
        private string dap;

        public string Dap
        {
            get { return dap; }
            set { dap = value; }
        }
        private GeoFenceMembData geofence2;
        private string dsrt;

        public string Dsrt
        {
            get { return dsrt; }
            set { dsrt = value; }
        }
        private string dhpa;

        public string Dhpa
        {
            get { return dhpa; }
            set { dhpa = value; }
        }
        


        private string error;
        private string confirm;

        public String Error { get { return error; } set { error = value; } }
        public String Confirm { get { return confirm; } set { confirm = value; } }


        public TTJavaCallReply()
        {
        }

        public LinkedList<MemberData> LinkedList { get { return MemberList; } set { MemberList = value; } }
        public ResponseTypeHelper ResponseTypeHelper { get { return Fleetonline_request_type; } set { Fleetonline_request_type = value; } }
        public TTConfigDBObject TTConfigDBObject { get { return dbObject; } set { dbObject = value; } }

 
        public GeoFenceMembData Geofence1
        {
            get { return Geofence11; }
            set { Geofence11 = value; }
        }
        public GeoFenceMembData Geofence2
        {
            get { return geofence2; }
            set { geofence2 = value; }
        }

    }
}
