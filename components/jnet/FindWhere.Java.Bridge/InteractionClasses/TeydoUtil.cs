using System;
using System.Collections;
using com.teleca.fleetonline.utils;
using com.teleca.fleetonline.mapping;

namespace com.teydo.fleetonline.utils
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL/src/java/ejb_module/src/com/teydo/fleetonline/utils/TeydoUtil.java,v $
    // $Revision: 1.18 $
    // $Date: 2007/06/06 08:27:12 $
    //
    // Copyright(c) Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //************************************************************************* 



    //using GISCoordinate = com.teleca.fleetonline.mapping.GISCoordinate;
    //using GlobalConstants = com.teleca.fleetonline.utils.GlobalConstants;
    //using LatLong = com.teleca.fleetonline.mapping.LatLong;
    //using Logging = com.teleca.fleetonline.utils.Logging;
    //using FOLProperties = com.teleca.fleetonline.utils.FOLProperties;

    [System.Serializable]
    public class TeydoUtil
    {

        //private static Logging logger = Logging.getInstance();

        private static FOLProperties folProps = FOLProperties.getInstance();

        private static int applicationId = -1;
        private static int commCenterListSizeMax = -1;

        private static string[] timeZoneIDs;

        //private static SimpleDateFormat timeFormatFull;
        //private static SimpleDateFormat timeFormatLbs;
        //private static SimpleDateFormat timeFormatMsg;
        //private static SimpleDateFormat timeFormatTime;

        private static DateTime  timeFormatFull;
        private static DateTime timeFormatLbs;
        private static DateTime timeFormatMsg;
        private static DateTime timeFormatTime;


        //static TeydoUtil()
        //{
        //    string timeFormat;

        //    // Full time stamp
        //    timeFormat = folProps.getProperty("value.timeformat.full");
        //    if (timeFormat == null)
        //    {
        //        timeFormat = "dd/MM/yy hh:mm:ss";
        //    }
        //    timeFormatFull = new SimpleDateFormat(timeFormat);

        //    // Timestamp for LBS
        //    timeFormat = folProps.getProperty("value.timeformat.lbs");
        //    if (timeFormat == null)
        //    {
        //        timeFormat = "dd/MM";
        //    }
        //    timeFormatLbs = new SimpleDateFormat(timeFormat);

        //    // Timestamp for messages
        //    timeFormat = folProps.getProperty("value.timeformat.msg");
        //    if (timeFormat == null)
        //    {
        //        timeFormat = "dd/MM HH:mm";
        //    }
        //    timeFormatMsg = new SimpleDateFormat(timeFormat);

        //    // Timestamp for time
        //    timeFormat = folProps.getProperty("value.timeformat.time");
        //    if (timeFormat == null)
        //    {
        //        timeFormat = "HH:mm";
        //    }
        //    timeFormatTime = new SimpleDateFormat(timeFormat);
        //}

        //public static int getApplicationId()
        //{
        //    if (applicationId == -1)
        //    {
        //        // We need to retrieve this value from the DB only ONCE!
        //        string val = folProps.getProperty("value.application.id");
        //        try
        //        {
        //            applicationId = Convert.ToInt32(val);
        //        }
        //        catch (Exception e)
        //        {
        //            logger.logMajorError("TeydoUtil", "getApplicationId()", "Exception parsing property 'value.application.id'=" + val + "  will assume this is a regular FOL instance", Logging.Component.UTILITIES);
        //            applicationId = GlobalConstants.APPLICATION_ID_FOL;
        //        }
        //    }
        //    return applicationId;
        //}

        //public static int getCommCenterListSizeMax()
        //{
        //    if (commCenterListSizeMax == -1)
        //    {
        //        // We need to retrieve this value from the DB only ONCE!
        //        string val = folProps.getProperty("value.commcenter.listsize.max");
        //        try
        //        {
        //            commCenterListSizeMax = Convert.ToInt32(val);
        //        }
        //        catch (Exception e)
        //        {
        //            logger.logInfo("TeydoUtil getCommCenterListSizeMax() Exception parsing property 'value.commcenter.listsize.max'=" + val + " .Will use the deafult value.", Logging.Component.UTILITIES);
        //            commCenterListSizeMax = GlobalConstants.COMM_CENTER_LIST_SIZE_MAX;
        //        }
        //    }
        //    return commCenterListSizeMax;
        //}

        //    *
        //	 * Get all available time zone identifiers (sorted on alphabet).
        //	 *
        //	 * @return String[] The timezone identifiers
        //	 
        //public static string[] getFolTimeZoneIDs()
        //{
        //    if (timeZoneIDs == null)
        //    {
        //        string[] tzIDs = TimeZone.getAvailableIDs();
        //        ArrayList filteredTzIDs = new ArrayList();
        //        string @value = folProps.getProperty("variable.timezone.display");
        //        string[] partsOfValue = splitArgument(@value, ",");
        //        if (partsOfValue != null)
        //        {
        //            for (int i = 0; i < tzIDs.length; i++)
        //            {
        //                string currentID = tzIDs[i];

        //                for (int j = 0; j < partsOfValue.length; j++)
        //                {
        //                    if (currentID.StartsWith(partsOfValue[j]))
        //                    {
        //                        filteredTzIDs.Add(currentID);
        //                        break;
        //                    }
        //                }
        //            }

        //            tzIDs = new string[filteredTzIDs.Count];
        //            filteredTzIDs.ToArray(tzIDs);
        //        }
        //        else
        //        {
        //            logger.logMajorError("TeydoUtil.java", "getFolTimeZoneIDs", "variable.timezone.dislay not present or null", Logging.Component.UTILITIES);
        //        }
        //        System.Array.Sort(tzIDs);
        //        timeZoneIDs = tzIDs;
        //    }
        //    return (timeZoneIDs);
        //}

        // Convert a string separated by another string (splitStr) into a String[]
        //public static string[] splitArgument(string @value, string splitStr)
        //{
        //    if (@value == null || @value.Length == 0)
        //    {
        //        return (null);
        //    }
        //    StringTokenizer tokenizer = new StringTokenizer(@value, splitStr);
        //    string[] resultVal = new string[tokenizer.countTokens()];
        //    int idx = 0;
        //    while (tokenizer.hasMoreElements())
        //    {
        //        resultVal[idx] = tokenizer.nextToken();
        //        idx++;
        //    }
        //    return (resultVal);
        //}

        //public static string formatDateForFullDisplay(DateTime date)
        //{
        //    return (timeFormatFull.format(date));
        //}

        //public static string formatDateForLbsDisplay(DateTime date)
        //{
        //    return (timeFormatLbs.format(date));
        //}

        //public static string formatDateForMessageDisplay(DateTime date)
        //{
        //    if (date == null)
        //        return "";
        //    return (timeFormatMsg.format(date));
        //}

        //public static string formatTimeForLbsDisplay(DateTime date)
        //{
        //    return timeFormatTime.format(date);
        //}

        //    *
        //	 * returns an array of 4 latitude/longitude pairs that represent the corners of a square around a center (latitude, longitude) with a north, east, south and west radius
        //	 * @param latitude in decimal degrees (center coordinate)
        //	 * @param longitude in decimal degrees (center coordinate)
        //	 * @param radius in meters
        //	 * @return LatLong[4] 4 LatLong object holding NE, SE, SW, NW corner coordinate object
        //	 
        //public static LatLong[] createSquareCoordinatesByCenterAndRadius(double latitude, double longitude, double radius)
        //{

        //    LatLong[] result = new LatLong[4];
        //    GISCoordinate g = null;
        //    int distanceIn = GISCoordinate.METERS;
        //    int sphereDef = GISCoordinate.SPHERE;

        //    try
        //    {
        //        g = new GISCoordinate(latitude, longitude, false);
        //        // north-east corner coordinate
        //        g.move(radius, distanceIn, GISCoordinate.NORTH, sphereDef);
        //        g.move(radius, distanceIn, GISCoordinate.EAST, sphereDef);
        //        result[0] = new LatLong(g.getLatInDecDeg(), g.getLonInDecDeg());

        //        //			south-east corner coordinate
        //        g = new GISCoordinate(latitude, longitude, false);
        //        g.move(radius, distanceIn, GISCoordinate.SOUTH, sphereDef);
        //        g.move(radius, distanceIn, GISCoordinate.EAST, sphereDef);
        //        result[1] = new LatLong(g.getLatInDecDeg(), g.getLonInDecDeg());

        //        //			south-west corner coordinate
        //        g = new GISCoordinate(latitude, longitude, false);
        //        g.move(radius, distanceIn, GISCoordinate.SOUTH, sphereDef);
        //        g.move(radius, distanceIn, GISCoordinate.WEST, sphereDef);
        //        result[2] = new LatLong(g.getLatInDecDeg(), g.getLonInDecDeg());

        //        //			north-west corner coordinate
        //        g = new GISCoordinate(latitude, longitude, false);
        //        g.move(radius, distanceIn, GISCoordinate.NORTH, sphereDef);
        //        g.move(radius, distanceIn, GISCoordinate.WEST, sphereDef);
        //        result[3] = new LatLong(g.getLatInDecDeg(), g.getLonInDecDeg());

        //    }
        //    catch (Exception e)
        //    {
        //        logger.logMajorError("TeydoUtil.java", "createSquareCoordinatesByCenterAndRadius", "Error generating LatLongs from GISCoordinate; exception: " + e.getMessage(), Logging.Component.UTILITIES);
        //        return null;
        //    }
        //    return result;
        //}

        //    
        //	 * Convert format of latitude/longitude from D.MMmm -> D.d
        //	 
        //JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public static double convertDMmToDd(double val) throws Exception
        public static double convertDMmToDd(double val)
        {
            double result;
            double tmpval;
            long whole;
            double rest;
            bool isNegative;

            //JAVA TO VB & C# CONVERTER TODO TASK: Octal literals cannot be represented in C#:
            isNegative = !(val >= 0.00000000000d);
            tmpval = Math.Abs(val);
            whole = (long)Math.Floor(tmpval);
            rest = tmpval - whole;
            result = whole + ((rest / 60.0) * 100.0);
            if (isNegative)
            {
                result = -result;
            }
            return result;
        }

    }
}