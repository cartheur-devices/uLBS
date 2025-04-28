using System;
using com.teleca.fleetonline.repository;
using System.Collections;

namespace com.teleca.fleetonline.web.bean
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL/src/java/web_module/src/com/teleca/fleetonline/web/bean/BalanceDisplayHelper.java,v $
    // $Revision: 1.14 $
    // $Date: 2008/01/08 11:08:38 $
    //
    // Copyright : Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************
        

    [System.Serializable]
    public class BalanceDisplayHelper
    {
        //parse the information within the bean
        //return html
        private System.Text.StringBuilder balanceDetails = null;

        public System.Text.StringBuilder BalanceDetails
        {
            get { return balanceDetails; }
            set { balanceDetails = value; }
        }
        private string foId;

        public string FoId
        {
            get { return foId; }
            set { foId = value; }
        }
        private string day;

        public string Day
        {
            get { return day; }
            set { day = value; }
        }
        private string month;

        public string Month
        {
            get { return month; }
            set { month = value; }
        }
        private string year;

        public string Year
        {
            get { return year; }
            set { year = value; }
        }
        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        private DateTime toDate;

        public DateTime ToDate
        {
            get { return toDate; }
            set { toDate = value; }
        }
        private double total = 0.0;

        public double Total
        {
            get { return total; }
            set { total = value; }
        }
        private double balance = 0.0;

        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        private double credits = 0.0;

        public double Credits
        {
            get { return credits; }
            set { credits = value; }
        }
        private long MILLISECONDDAY = 86399999;

        public long MILLISECONDDAY1
        {
            get { return MILLISECONDDAY; }
            set { MILLISECONDDAY = value; }
        }
        //private DecimalFormat df = new DecimalFormat("0.00");
        private UserInfoData userInfoData;

        public UserInfoData UserInfoData
        {
            get { return userInfoData; }
            set { userInfoData = value; }
        }

        private int maxRowCount;

        public int MaxRowCount
        {
            get { return maxRowCount; }
            set { maxRowCount = value; }
        }
        private string fmId, alias;

        public string FmId
        {
            get { return fmId; }
            set { fmId = value; }
        }

        public string Alias
        {
            get { return alias; }
            set { alias = value; }
        }
        private string memberType = "";

        public string MemberType
        {
            get { return memberType; }
            set { memberType = value; }
        }

        private ArrayList balanceDetailsList = null;

        public ArrayList BalanceDetailsList
        {
            get { return balanceDetailsList; }
            set { balanceDetailsList = value; }
        }

        
        public BalanceDisplayHelper()
        {
        }
               
        public virtual string getTotalCharge()
        {           
            return "" + (int)Total;
        }

        public virtual string getTotalCredits()
        {           
            return "" + (int)Credits;
        }

        public virtual string getBalance()
        {           
            return "" + (int)Balance; //credit
        }

        public virtual string getFoId()
        {
            return FoId;
        }     

        public virtual string getAlias()
        {
            return Alias;
        }

        public virtual string getMemberType()
        {
            return MemberType;
        }
    }
}