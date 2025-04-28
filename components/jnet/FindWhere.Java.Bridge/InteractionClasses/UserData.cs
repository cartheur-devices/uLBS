//*************************************************************************
//
// $Source: D:/Data/cvs/FOL/src/java/ejb_module/src/com/teleca/fleetonline/repository/UserData.java,v $
// $Revision: 1.11 $
// $Date: 2006/11/21 11:23:01 $
//
// Copyright (c) Teydo BV, Bilthoven, all rights reserved worldwide.
//
//*************************************************************************
using System;
using System.Xml;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;
using com.teleca.fleetonline.web.bean;
using com.teleca.fleetonline.utils;
using GlobalConstants = com.teleca.fleetonline.utils.GlobalConstants;
namespace JNetBridge.InteractionClasses
{


    /// <summary> This class holds User data (username, password, membertype, operator, country, language, isUserFo, autoLbs)</summary>

    [Serializable]
    public class UserData : RepositoryData
    {
        private void InitBlock()
        {
            membertype = GlobalConstants.MEMBERTYPE_UNREGISTERED;
            operator_Renamed = GlobalConstants.OPERATOR_UNDEFINED;
            active = GlobalConstants.USER_ACTIVE_UNDEFINED;
        }
        /// <summary> Returns true if the user is an FO</summary>
        /// <returns> boolean
        /// </returns>
        virtual public bool Fo
        {
            get
            {
                return this.isUserFo;
            }

        }
        //UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
        /// <summary> Returns the country.</summary>
        /// <returns> int
        /// </returns>
        /// <summary> Sets the country.</summary>
        /// <param name="country">The country to set
        /// </param>
        virtual public int Country
        {
            get
            {
                return country;
            }

            set
            {
                this.country = value;
            }

        }
        //UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
        /// <summary> Returns the membertype.</summary>
        /// <returns> int
        /// </returns>
        /// <summary> Sets the membertype.</summary>
        /// <param name="membertype">The membertype to set
        /// </param>
        virtual public int Membertype
        {
            get
            {
                return membertype;
            }

            set
            {
                this.membertype = value;
            }

        }
        //UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
        /// <summary> Returns the msisdn.</summary>
        /// <returns> String
        /// </returns>
        /// <summary> Sets the msisdn.</summary>
        /// <param name="msisdn">The msisdn to set
        /// </param>
        virtual public System.String Msisdn
        {
            get
            {
                return msisdn;
            }

            set
            {
                this.msisdn = value;
            }

        }
        //UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
        /// <summary> Returns the operator.</summary>
        /// <returns> int
        /// </returns>
        /// <summary> Sets the operator.</summary>
        /// <param name="operator">The operator to set
        /// </param>
        virtual public int Operator
        {
            get
            {
                return operator_Renamed;
            }

            set
            {
                this.operator_Renamed = value;
            }

        }
        //UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
        /// <summary> Returns the password.</summary>
        /// <returns> String
        /// </returns>
        /// <summary> Sets the password.</summary>
        /// <param name="password">The password to set
        /// </param>
        virtual public System.String Password
        {
            get
            {
                return password;
            }

            set
            {
                this.password = value;
            }

        }
        //UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
        /// <summary> Returns the uid.</summary>
        /// <returns> String
        /// </returns>
        /// <summary> Sets the uid.</summary>
        /// <param name="uid">The uid to set
        /// </param>
        virtual public System.String Uid
        {
            get
            {
                return uid;
            }

            set
            {
                this.uid = value;
                uidSet = true;
            }

        }
        /// <summary> Returns the uidSet.</summary>
        /// <returns> boolean
        /// </returns>
        virtual public bool UidSet
        {
            get
            {
                return uidSet;
            }

        }
        //UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
        /// <summary> Returns the active.</summary>
        /// <returns> int
        /// </returns>
        /// <summary> Sets the active.</summary>
        /// <param name="active">The active to set
        /// </param>
        virtual public int Active
        {
            get
            {
                return active;
            }

            set
            {
                this.active = value;
            }

        }
        //UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
        /// <summary> Returns the language.</summary>
        /// <returns> String
        /// </returns>
        /// <summary> Sets the language.</summary>
        /// <param name="language">The language to set
        /// </param>
        virtual public System.String Language
        {
            get
            {
                return language;
            }

            set
            {
                this.language = value;
            }

        }
        /// <summary> Sets the isFo.</summary>
        /// <param name="isFo">The isFo to set
        /// </param>
        virtual public bool IsFo
        {
            set
            {
                this.isUserFo = value;
            }

        }
        /// <summary> Sets the isFo.</summary>
        /// <param name="isFo">The isFo to set
        /// </param>
        virtual public bool AutoLbs
        {
            set
            {
                this.autoLbs = value;
            }

        }
        //UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
        /// <summary> returns the userTimeout.</summary>
        /// <returns> int
        /// </returns>
        /// <summary> Sets the userTimeout.</summary>
        /// <param name="userTimeout">The userTimeout to set
        /// </param>
        virtual public int UserTimeout
        {
            get
            {
                return userTimeout;
            }

            set
            {
                this.userTimeout = value;
            }

        }
        /// <summary> returns the userTimeout in milliseconds.</summary>
        /// <returns> long
        /// </returns>
        virtual public long LongUserTimeout
        {
            get
            {
                return userTimeout * 60 * 1000;
            }

        }
        //UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
        /// <summary> Returns the currently set device profile</summary>
        /// <returns>
        /// </returns>
        /// <summary> Sets the currently applied device profile</summary>
        /// <param name="l">
        /// </param>
        virtual public long CurrentProfile
        {
            get
            {
                return this.currentProfile;
            }

            set
            {
                this.currentProfile = value;
            }

        }
        //UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
        /// <summary> Returns the device profile that is being applied</summary>
        /// <returns>
        /// </returns>
        /// <summary> Sets the profile that needs to be applied</summary>
        /// <param name="l">
        /// </param>
        virtual public long NewProfile
        {
            get
            {
                return this.newProfile;
            }

            set
            {
                this.newProfile = value;
            }

        }
        //UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
        /// <summary> Returns the user type.</summary>
        /// <returns>
        /// </returns>
        /// <summary> Sets the user type</summary>
        /// <param name="i">
        /// </param>
        virtual public int UserType
        {
            get
            {
                return this.userType;
            }

            set
            {
                this.userType = value;
            }

        }
        //UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
        /// <summary> Gets the timezone for this user</summary>
        /// <returns> String specifying a timezone
        /// </returns>
        /// <summary> Set the timezone for this user</summary>
        /// <param name="timeZoneId">String which specifies a timezone
        /// </param>
        virtual public System.String TimeZoneId
        {
            get
            {
                return this.timeZoneId;
            }

            set
            {
                this.timeZoneId = value;
            }

        }
        //UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
        /// <returns> Returns the onDemand.
        /// </returns>
        /// <param name="onDemand">The onDemand to set.
        /// </param>
        virtual public int OnDemand
        {
            get
            {
                return onDemand;
            }

            set
            {
                this.onDemand = value;
            }

        }
        //UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
        /// <returns> Returns the getMispos.
        /// </returns>
        /// <param name="getMispos">The getMispos to set.
        /// </param>
        virtual public int GetMispos
        {
            get
            {
                return getMispos;
            }

            set
            {
                this.getMispos = value;
            }

        }
        virtual public int Level
        {
            get
            {
                return level;
            }

            set
            {
                this.level = value;
            }

        }
        virtual public bool GeocodingAvailable
        {
            get
            {
                return geocodingAvailable;
            }

            set
            {
                this.geocodingAvailable = value;
            }

        }
        private const long serialVersionUID = 1L;

        private System.String uid = null;
        private bool uidSet = false; // no set function for uidSet

        private System.String msisdn = null;
        private System.String password = null;
        //UPGRADE_NOTE: The initialization of  'membertype' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
        private int membertype;
        //UPGRADE_NOTE: The initialization of  'operator_Renamed' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
        private int operator_Renamed;
        private int country = 0;
        //UPGRADE_NOTE: The initialization of  'active' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
        private int active;
        private System.String language = null;
        private bool isUserFo = false; // not used in UpdateUserData
        private bool autoLbs = false;
        private int userTimeout = 20; //default
        private int userType = 0;
        private long newProfile = 0;
        private long currentProfile = 0;
        private System.String timeZoneId = null;
        private int onDemand = 0; // trimtrac: live locate on/off
        private int getMispos = 1; // trimtrac: get missing positions&status on/off
        private int level;
        private bool geocodingAvailable = true;

        public const int MEMBERTYPE_FM = 1;

        /// <summary> Constructor</summary>
        public UserData()
            : base()
        {
            InitBlock();
            uidSet = false;
        }

        /// <summary> Constructor that sets User ID</summary>
        /// <param name="uid">ID of the User
        /// </param>
        public UserData(System.String uid)
            : base()
        {
            InitBlock();
            this.uid = uid;
            uidSet = true;
        }

        /// <summary> Returns true if the user has AutoLBS set, i.e. an LBS is triggered for the FM when the FM sends FM status to the system</summary>
        /// <returns> boolean
        /// </returns>
        public virtual bool hasAutoLbs()
        {
            return this.autoLbs;
        }  
      
    }
}