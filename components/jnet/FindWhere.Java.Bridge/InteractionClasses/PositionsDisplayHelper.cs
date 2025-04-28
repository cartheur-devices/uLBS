using System;
using com.teleca.fleetonline.utils;
using System.Collections;
using com.teleca.fleetonline.repository;
using com.teydo.fleetonline.utils;
using JNetBridge.Classes;

namespace com.teleca.fleetonline.web.bean
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL/src/java/web_module/src/com/teleca/fleetonline/web/bean/PositionsDisplayHelper.java,v $
    // $Revision: 1.34 $
    // $Date: 2008/01/14 15:25:05 $
    //
    // Copyright : Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************


    

    ///
    // * Deals with outputting the positions table
    // * 
    // 
    [System.Serializable]
    public class PositionsDisplayHelper
    {

        //    *
        //	 * 
        //	 
        private const long serialVersionUID = 1L;       

        private static FOLProperties folProps = FOLProperties.getInstance();

        //    *
        //	 * Main positions content
        //	 
        private ArrayList allPos = null;
        public ArrayList AllPositions { get { return allPos; } set { allPos = value; } }

        //    *
        //	 * fleet information. Used to output the correct alias for FMs
        //	 
       
        private int distanceUnits;
        private string speedUnitStr;

        public PositionsDisplayHelper()
        {
        }        

        //    *
        //	 * Set the helper
        //	 * @param all main position data
        //	 * @param fleet fleet data
        //	 
        public PositionsDisplayHelper(ArrayList all, GroupsAndMembersData fleet, int distanceUnits)
        {
            allPos = all;
            //this.fleet = fleet;
            this.distanceUnits = distanceUnits;
            if (distanceUnits == GlobalConstants.DISTANCE_KM)
            {
                speedUnitStr = " " + folProps.getProperty("label.speed.km");
            }
            else
            {
                speedUnitStr = " " + folProps.getProperty("label.speed.mile");
            }
            if (speedUnitStr == null)
            {
                Console.WriteLine("Missing/empty property label.speed.mile or label.speed.km");
                speedUnitStr = "";
            }
        }
    }
}