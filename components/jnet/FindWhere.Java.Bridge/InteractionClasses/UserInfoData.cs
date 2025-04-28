using System;
using com.teleca.fleetonline.web.bean;
using com.teleca.fleetonline.utils;

namespace com.teleca.fleetonline.repository
{
	
	//*************************************************************************
	//
	// $Archive: /sdt/product/fol/versions/head/src/java/ejb_module/src/com/teleca/fleetonline/repository/UserInfoData.java $
	// $Revision: 1.5 $
	// $Date: 2007/06/19 09:58:14 $
	// $Author: kim.spiritus $
	//
	//*************************************************************************
	
	/*
	* @UserInfoData
	*
	* Copyright (c) 2002 Teleca UK Ltd.
	* All rights reserved.
	*
	* This software is the confidential and proprietary information of Teleca UK Ltd.
	* ("Confidential Information").  You shall not
	* disclose such Confidential Information and shall use it only in
	* accordance with the terms of the license agreement you entered into
	* with Teleca UK Ltd.
	*/
	
	/// <summary> This class holds User Info data (company, address, postcode, email, number of vehicles, subscriptiontype, promotioncode default location, paytype, distance type etc.)</summary>
	/// <author>   $Author: kim.spiritus $
	/// </author>
	/// <version>  $Revision: 1.5 $, $Date: 2007/06/19 09:58:14 $
	/// </version>
	
	[Serializable]
	public class UserInfoData:RepositoryData
	{
        private void InitBlock()
        {
            distance = GlobalConstants.DISTANCE_MILES;
        }
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the address.</summary>
		/// <returns> String
		/// </returns>
		/// <summary> Sets the address.</summary>
		/// <param name="address">The address to set
		/// </param>
		 public System.String Address
		{
			get
			{
				return address;
			}
			
			set
			{
				this.address = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the companyName.</summary>
		/// <returns> String
		/// </returns>
		/// <summary> Sets the companyName.</summary>
		/// <param name="companyName">The companyName to set
		/// </param>
		 public System.String CompanyName
		{
			get
			{
				return companyName;
			}
			
			set
			{
				this.companyName = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the email.</summary>
		/// <returns> String
		/// </returns>
		/// <summary> Sets the email.</summary>
		/// <param name="email">The email to set
		/// </param>
		 public System.String Email
		{
			get
			{
				return email;
			}
			
			set
			{
				this.email = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the numberOfVehicles.</summary>
		/// <returns> int
		/// </returns>
		/// <summary> Sets the numberOfVehicles.</summary>
		/// <param name="numberOfVehicles">The numberOfVehicles to set
		/// </param>
		 public int NumberOfVehicles
		{
			get
			{
				return numberOfVehicles;
			}
			
			set
			{
				this.numberOfVehicles = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the postcode.</summary>
		/// <returns> String
		/// </returns>
		/// <summary> Sets the postcode.</summary>
		/// <param name="postcode">The postcode to set
		/// </param>
		 public System.String Postcode
		{
			get
			{
				return postcode;
			}
			
			set
			{
				this.postcode = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the registrationtime.</summary>
		/// <returns> java.util.Date
		/// </returns>
		/// <summary> Sets the registrationtime.</summary>
		/// <param name="registrationtime">The registrationtime to set
		/// </param>
		 public System.DateTime Registrationtime
		{
			get
			{
				return registrationtime;
			}
			
			set
			{
				this.registrationtime = value;
			}
			
		}
		/// <summary> Returns the uid.</summary>
		/// <returns> String
		/// </returns>
		 public System.String Uid
		{
			get
			{
				return uid;
			}
			
		}
		/// <summary> Returns the uidSet.</summary>
		/// <returns> boolean
		/// </returns>
		 public bool UidSet
		{
			get
			{
				return uidSet;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the hearAboutUs.</summary>
		/// <returns> String
		/// </returns>
		/// <summary> Sets the hearAboutUs.</summary>
		/// <param name="hearAboutUs">The hearAboutUs to set
		/// </param>
		 public System.String HearAboutUs
		{
			get
			{
				return hearAboutUs;
			}
			
			set
			{
				this.hearAboutUs = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the lineOfBusinessId.</summary>
		/// <returns> int
		/// </returns>
		/// <summary> Sets the lineOfBusinessId.</summary>
		/// <param name="lineOfBusinessId">The lineOfBusinessId to set
		/// </param>
		 public int LineOfBusinessId
		{
			get
			{
				return lineOfBusinessId;
			}
			
			set
			{
				this.lineOfBusinessId = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the subscriptionType.</summary>
		/// <returns> int
		/// </returns>
		/// <summary> Sets the subscriptionType.</summary>
		/// <param name="subscriptionType">The subscriptionType to set
		/// </param>
		 public int SubscriptionType
		{
			get
			{
				return subscriptionType;
			}
			
			set
			{
				this.subscriptionType = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the promotionCode.</summary>
		/// <returns> String
		/// </returns>
		/// <summary> Sets the promotionCode.</summary>
		/// <param name="promotionCode">The promotionCode to set
		/// </param>
		 public System.String PromotionCode
		{
			get
			{
				return promotionCode;
			}
			
			set
			{
				this.promotionCode = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the defaultLocationX.</summary>
		/// <returns> double
		/// </returns>
		/// <summary> Sets the defaultLocationX.</summary>
		/// <param name="defaultLocationX">The defaultLocationX to set
		/// </param>
		 public double DefaultLocationX
		{
			get
			{
				return defaultLocationX;
			}
			
			set
			{
				this.defaultLocationXSet = true;
				this.defaultLocationX = value;
			}
			
		}
		/// <summary> Returns the defaultLocationXSet.</summary>
		/// <returns> boolean
		/// </returns>
		 public bool DefaultLocationXSet
		{
			get
			{
				return defaultLocationXSet;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the defaultLocationY.</summary>
		/// <returns> double
		/// </returns>
		/// <summary> Sets the defaultLocationY.</summary>
		/// <param name="defaultLocationY">The defaultLocationY to set
		/// </param>
		 public double DefaultLocationY
		{
			get
			{
				return defaultLocationY;
			}
			
			set
			{
				this.defaultLocationYSet = true;
				this.defaultLocationY = value;
			}
			
		}
		/// <summary> Returns the defaultLocationYSet.</summary>
		/// <returns> boolean
		/// </returns>
		 public bool DefaultLocationYSet
		{
			get
			{
				return defaultLocationYSet;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the defaultLocationnadX.</summary>
		/// <returns> double
		/// </returns>
		/// <summary> Sets the defaultLocationnadX.</summary>
		/// <param name="defaultLocationnadX">The defaultLocationnadX to set
		/// </param>
		 public double DefaultLocationnadX
		{
			//	/**
			//	 * Returns the defaultLocationnadXSet.
			//	 * @return boolean
			//	 */
			//	public boolean isDefaultLocationnadXSet() {
			//		return defaultLocationnadXSet;
			//	}
			//
			//	/**
			//	 * Returns the defaultLocationnadYSet.
			//	 * @return boolean
			//	 */
			//	public boolean isDefaultLocationnadYSet() {
			//		return defaultLocationnadYSet;
			//	}
			//
			
			get
			{
				return defaultLocationnadX;
			}
			
			set
			{
				//		this.defaultLocationnadXSet = true;
				this.defaultLocationnadX = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the defaultLocationnadY.</summary>
		/// <returns> double
		/// </returns>
		/// <summary> Sets the defaultLocationnadY.</summary>
		/// <param name="defaultLocationnadY">The defaultLocationnadY to set
		/// </param>
		 public double DefaultLocationnadY
		{
			get
			{
				return defaultLocationnadY;
			}
			
			set
			{
				//		this.defaultLocationnadYSet = true;
				this.defaultLocationnadY = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the paytype.</summary>
		/// <returns> int
		/// </returns>
		/// <summary> Sets the paytype.</summary>
		/// <param name="paytype">The paytype to set
		/// </param>
		 public int Paytype
		{
			get
			{
				return paytype;
			}
			
			set
			{
				this.paytype = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the distance.</summary>
		/// <returns> int
		/// </returns>
		/// <summary> Sets the distance.</summary>
		/// <param name="distance">The distance to set
		/// </param>
		 public int Distance
		{
			get
			{
				return distance;
			}
			
			set
			{
				this.distance = value;
			}
			
		}
		 public System.String MapImplementer
		{
			get
			{
				return mapImplementer;
			}
			
			set
			{
				this.mapImplementer = value;
			}
			
		}
		 public System.DateTime? NotifierRead
		{
			get
			{
				return notifierRead;
			}
			
			set
			{
				this.notifierRead = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <returns> the displayMapLabels
		/// </returns>
		/// <param name="displayMapLabels">the displayMapLabels to set
		/// </param>
		 public int DisplayMapLabels
		{
			get
			{
				return displayMapLabels;
			}
			
			set
			{
				this.displayMapLabels = value;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		private System.String uid = null; // not set function for uid
		private bool uidSet; // no set function for uidSet
		
		private DateTime registrationtime;
		private string companyName ;
		private String address ;
		private String postcode ;
		private String email ;
		private int numberOfVehicles = 0; // can be undefined
		private int lineOfBusinessId = 0; // can be undefined
		private String hearAboutUs ; // can be undefined
		private int subscriptionType; // pre or post paid
		private String promotionCode ; // can be undefined
		private double defaultLocationX ;
		private bool defaultLocationXSet ;
		private double defaultLocationY ;
		private bool defaultLocationYSet ;
		private double defaultLocationnadX ;
		private double defaultLocationnadY ;
		//	private boolean defaultLocationnadXSet = false;
		//	private boolean defaultLocationnadYSet = false;
		private int paytype;
		//UPGRADE_NOTE: The initialization of  'distance' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private int distance;
		private String mapImplementer ;
        private int displayMapLabels;
		
		//UPGRADE_TODO: The 'System.DateTime' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
		private System.DateTime? notifierRead = null;
		
		/// <summary> Constructor</summary>
		public UserInfoData()
		{
			//InitBlock();
			uidSet = false;
		}
		
		/// <summary> Constructor that sets User ID</summary>
		/// <param name="uid">ID of the User
		/// </param>
		public UserInfoData(System.String uid)
		{
			//InitBlock();
			this.uid = uid;
			uidSet = true;
		}
	}
}