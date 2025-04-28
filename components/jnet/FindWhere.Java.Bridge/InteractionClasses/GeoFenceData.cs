using System;
using com.teleca.fleetonline.web.bean;
namespace com.teleca.fleetonline.repository
{
	
	//*************************************************************************
	//
	// $Archive: $
	// $Revision: 1.3 $
	// $Date: 2006/09/15 09:53:39 $
	//
	//*************************************************************************
	
	/// <summary> This class holds Geofence data</summary>
	
	[Serializable]
	public class GeoFenceData:RepositoryData
	{
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <returns> Returns the fenceId.
		/// </returns>
		/// <param name="fenceId">The fenceId to set.
		/// </param>
		virtual public System.String FenceId
		{
			get
			{
				return fenceId;
			}
			
			set
			{
				this.fenceId = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <returns> Returns the foId.
		/// </returns>
		/// <param name="foId">The foId to set.
		/// </param>
		virtual public System.String FoId
		{
			get
			{
				return foId;
			}
			
			set
			{
				this.foId = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <returns> Returns the locationX.
		/// </returns>
		/// <param name="locationX">The locationX to set.
		/// </param>
		virtual public double LocationX
		{
			get
			{
				return locationX;
			}
			
			set
			{
				this.locationX = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <returns> Returns the locationY.
		/// </returns>
		/// <param name="locationY">The locationY to set.
		/// </param>
		virtual public double LocationY
		{
			get
			{
				return locationY;
			}
			
			set
			{
				this.locationY = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <returns> Returns the locType.
		/// </returns>
		/// <param name="locType">The locType to set.
		/// </param>
		virtual public int LocType
		{
			get
			{
				return locType;
			}
			
			set
			{
				this.locType = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <returns> Returns the name.
		/// </returns>
		/// <param name="name">The name to set.
		/// </param>
		virtual public System.String Name
		{
			get
			{
				return name;
			}
			
			set
			{
				this.name = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <returns> Returns the postCode.
		/// </returns>
		/// <param name="postCode">The postCode to set.
		/// </param>
		virtual public System.String PostCode
		{
			get
			{
				return postCode;
			}
			
			set
			{
				this.postCode = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <returns> Returns the radius.
		/// </returns>
		/// <param name="radius">The radius to set.
		/// </param>
		virtual public double Radius
		{
			get
			{
				return radius;
			}
			
			set
			{
				this.radius = value;
			}
			
		}
		/// <returns> Returns the fenceIdSet.
		/// </returns>
		virtual public bool FenceIdSet
		{
			get
			{
				return fenceIdSet;
			}
			
		}
		
		/// <summary> </summary>
		private const long serialVersionUID = 1L;
		private bool fenceIdSet;
		private System.String fenceId;
		private System.String foId;
		private System.String name;
		private int locType = 0;
		private System.String postCode;
		private double radius;
		private double locationX = 0;
		private double locationY = 0;
		
		
		public GeoFenceData():base()
		{
			fenceIdSet = false;
		}
		
		public GeoFenceData(System.String fenceId):base()
		{
			this.fenceId = fenceId;
			fenceIdSet = true;
		}
	}
}