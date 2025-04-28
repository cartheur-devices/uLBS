using System;
using System.Collections;
using com.teydo.fleetonline.utils;
using com.teleca.fleetonline.utils;

namespace com.teleca.fleetonline.web.bean
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL5/web_module/src/com/teleca/fleetonline/web/bean/AccountDetailsDisplayHelper.java,v $
    // $Revision: 1.1 $
    // $Date: 2008/04/10 14:28:22 $
    //
    // Copyright (c) Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************
    
    ///
    // * This class assist with outputting all the miscallaneous data about a user
    // * 
    // 
    [System.Serializable]
    public class AccountDetailsDisplayHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private string personalMemberType;
        /// <summary>
        /// 
        /// </summary>
        public string PersonalMemberType
        {
            get { return personalMemberType; }
            set { personalMemberType = value; }
        }
        private string personalEmailAddress;
        /// <summary>
        /// 
        /// </summary>
        public string PersonalEmailAddress
        {
            get { return personalEmailAddress; }
            set { personalEmailAddress = value; }
        }
        private string personalPostcode;
        /// <summary>
        /// 
        /// </summary>
        public string PersonalPostcode
        {
            get { return personalPostcode; }
            set { personalPostcode = value; }
        }
        private string personalCountry;
        /// <summary>
        /// 
        /// </summary>
        public string PersonalCountry
        {
            get { return personalCountry; }
            set { personalCountry = value; }
        }
        private string personalCompanyName;
        /// <summary>
        /// 
        /// </summary>
        public string PersonalCompanyName
        {
            get { return personalCompanyName; }
            set { personalCompanyName = value; }
        }
        private int personalDisplayMapLabels;
        /// <summary>
        /// 
        /// </summary>
        public int PersonalDisplayMapLabels
        {
            get { return personalDisplayMapLabels; }
            set { personalDisplayMapLabels = value; }
        }
               
        private string countryName = null;
        /// <summary>
        /// 
        /// </summary>
        public string CountryName
        {
            get { return countryName; }
            set { countryName = value; }
        }
        private int distanceInUnits = -1;

        public int DistanceInUnits
        {
            get { return distanceInUnits; }
            set { distanceInUnits = value; }
        }
        //JAVA TO VB & C# CONVERTER TODO TASK: Octal literals cannot be represented in C#:
        private float balance = 0.00f;

        public float Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        private int userTimeout = 0;

        public int UserTimeout
        {
            get { return userTimeout; }
            set { userTimeout = value; }
        }
       
        private string showOperators = "false";

        public string ShowOperators
        {
            get { return showOperators; }
            set { showOperators = value; }
        }
        private string timeZoneId = null;

        public string TimeZoneId
        {
            get { return timeZoneId; }
            set { timeZoneId = value; }
        }
        private ArrayList memberDatas = new ArrayList();

        public ArrayList MemberDatas
        {
            get { return memberDatas; }
            set { memberDatas = value; }
        }
        private ArrayList userAccountDatas = new ArrayList();

        public ArrayList UserAccountDatas
        {
            get { return userAccountDatas; }
            set { userAccountDatas = value; }
        }
        private string[] smsActionNameDatas;

        public string[] SmsActionNameDatas
        {
            get { return smsActionNameDatas; }
            set { smsActionNameDatas = value; }
        }
        private string[] smsActionValueDatas;

        public string[] SmsActionValueDatas
        {
            get { return smsActionValueDatas; }
            set { smsActionValueDatas = value; }
        }
        private string[] smsActionKeyDatas;

        public string[] SmsActionKeyDatas
        {
            get { return smsActionKeyDatas; }
            set { smsActionKeyDatas = value; }
        }
        private string[] smsActionUseDatas;

        public string[] SmsActionUseDatas
        {
            get { return smsActionUseDatas; }
            set { smsActionUseDatas = value; }
        }

        private String[] timeZoneIDS;

        public String[] TimeZoneIDS
        {
            get { return timeZoneIDS; }
            set { timeZoneIDS = value; }
        }
               
        //    *
        //	 * Empty. Called by the JSP if no details retrieved. Usually after an error
        //	 * @see java.lang.Object#Object()
        //	
        /// <summary>
        /// 
        /// </summary>
        public AccountDetailsDisplayHelper()
        {
        }        
 
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string getShowOperators()
        {
            return ShowOperators;
        }       

        // used in jsp to prepopulate userTimeout dropdown list
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int getUserTimeout()
        {
            return this.UserTimeout;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string getTimeZoneId()
        {
            return this.TimeZoneId;
        }              

        //    *
        //	 * @param timeZoneId The timeZoneId to set.
        //	 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeZoneId"></param>
        public virtual void setTimeZoneId(string timeZoneId)
        {
            this.TimeZoneId = timeZoneId;
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual ArrayList getMemberDatas()
        {
            return MemberDatas;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberDatas"></param>
        public virtual void setMemberDatas(ArrayList memberDatas)
        {
            this.MemberDatas = memberDatas;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual ArrayList getUserAccountDatas()
        {
            return UserAccountDatas;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccountDatas"></param>
        public virtual void setUserAccountDatas(ArrayList userAccountDatas)
        {
            this.UserAccountDatas = userAccountDatas;
        }

        //    *
        //	 * @return the smsActionKeyDatas
        //	 
        /// <summary>
        /// @return the smsActionKeyDatas
        /// </summary>
        /// <returns></returns>
        public virtual string[] getSmsActionKeyDatas()
        {
            return SmsActionKeyDatas;
        }

        /// <summary>
        ///  @param smsActionKeyDatas the smsActionKeyDatas to set
        /// </summary>
        /// <param name="smsActionKeyDatas"></param>
        public virtual void setSmsActionKeyDatas(string[] smsActionKeyDatas)
        {
            this.SmsActionKeyDatas = smsActionKeyDatas;
        }


        /// <summary>
        /// * @return the smsActionNameDatas
        /// </summary>
        /// <returns></returns>
        public virtual string[] getSmsActionNameDatas()
        {
            return SmsActionNameDatas;
        }


        /// <summary>
        ///  * @param smsActionNameDatas the smsActionNameDatas to set
        /// </summary>
        /// <param name="smsActionNameDatas"></param>
        public virtual void setSmsActionNameDatas(string[] smsActionNameDatas)
        {
            this.SmsActionNameDatas = smsActionNameDatas;
        }

       
        /// <summary>
        ///  * @return the smsActionValueDatas
        /// </summary>
        /// <returns></returns>
        public virtual string[] getSmsActionValueDatas()
        {
            return SmsActionValueDatas;
        }

       
        /// <summary>
        ///  * @param smsActionValueDatas the smsActionValueDatas to set
        /// </summary>
        /// <param name="smsActionValueDatas"></param>
        public virtual void setSmsActionValueDatas(string[] smsActionValueDatas)
        {
            this.SmsActionValueDatas = smsActionValueDatas;
        }

       
        /// <summary>
        /// * @return the smsActionUseDatas
        /// </summary>
        /// <returns></returns>
        public virtual string[] getSmsActionUseDatas()
        {
            return SmsActionUseDatas;
        }

       
        /// <summary>
        /// * @param smsActionUseDatas the smsActionUseDatas to set
        /// </summary>
        /// <param name="smsActionUseDatas"></param>
        public virtual void setSmsActionUseDatas(string[] smsActionUseDatas)
        {
            this.SmsActionUseDatas = smsActionUseDatas;
        }
    }
}