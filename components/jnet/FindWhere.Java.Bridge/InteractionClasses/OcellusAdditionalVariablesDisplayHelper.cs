using System;
using com.teleca.fleetonline.utils;
using System.Collections;
using com.teydo.fleetonline.utils;
using com.teleca.fleetonline.repository;

namespace com.teleca.fleetonline.web.bean
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL5/web_module/src/com/teleca/fleetonline/web/bean/OcellusAdditionalVariablesDisplayHelper.java,v $
    // $Revision: 1.1 $
    // $Date: 2008/04/10 14:28:24 $
    //
    // Copyright : Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //************************************************************************
    ///
    // * Deals with outputting the Ocellus additional variables table
    // * 
    // 
    [System.Serializable]
    public class OcellusAdditionalVariablesDisplayHelper
    {

        //    *
        //	 * 
        //	 
        private const long serialVersionUID = 1L;

        public long SerialVersionUID
        {
            get { return serialVersionUID; }
        } 

        private static FOLProperties folProps = FOLProperties.getInstance();

        //    *
        //	 * Main additional variables content
        //	 
        private ArrayList allAddVars = null;

        public ArrayList AllAddVars
        {
            get { return allAddVars; }
            set { allAddVars = value; }
        }

        private GroupsAndMembersData fleet = null;

        public GroupsAndMembersData Fleet
        {
            get { return fleet; }
            set { fleet = value; }
        }

        private int distUnitType;

        public int DistUnitType
        {
            get { return distUnitType; }
            set { distUnitType = value; }
        }
        private string speedUnitStr;

        public string SpeedUnitStr
        {
            get { return speedUnitStr; }
            set { speedUnitStr = value; }
        }
        private string altitudeUnitStr;

        public string AltitudeUnitStr
        {
            get { return altitudeUnitStr; }
            set { altitudeUnitStr = value; }
        }

        public OcellusAdditionalVariablesDisplayHelper()
        {
        }

        //    *
        //	 * Set the helper
        //	 * @param all main additional variables data
        //	 
        public OcellusAdditionalVariablesDisplayHelper(ArrayList  all, GroupsAndMembersData fleet, int distUnitType)
        {
            allAddVars = all;
            this.fleet = fleet;
            this.distUnitType = distUnitType;
            if (distUnitType == GlobalConstants.DISTANCE_KM)
            {
                speedUnitStr = " " + folProps.getProperty("label.speed.km");
                altitudeUnitStr = " " + folProps.getProperty("label.general.distanceunit.meter");
            }
            else
            {
                speedUnitStr = " " + folProps.getProperty("label.speed.mile");
                altitudeUnitStr = " " + folProps.getProperty("label.general.distanceunit.feet");
            }
            if (speedUnitStr == null)
            {
                Console.WriteLine("Missing/empty property label.speed.mile or label.speed.km");
                speedUnitStr = "";
            }
            if (altitudeUnitStr == null)
            {
                Console.WriteLine("Missing/empty property label.general.distanceunit.feet or label.general.distanceunit.meter");
                altitudeUnitStr = "";
            }
        }
    }
}