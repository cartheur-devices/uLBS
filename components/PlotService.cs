using System;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Collections;
using com.teleca.fleetonline.mapping;
using JNetBridge.ReplyClasses;
using com.teleca.fleetonline.repository;
using JNetBridge;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Web;
using log4net;


namespace FindWhere.Locations
{
    /// <summary>
    /// Summary description for DataService
    /// </summary>
    /// 
    [WebService(Namespace = "http://www.findwhere.com/Panel5/PlotService.asmx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]

    public class PlotService : System.Web.Services.WebService
    {
        private JNetBridge.Classes.JnetBridgeLoginUnit javaID = null;
        protected static readonly ILog log = LogManager.GetLogger(typeof(PlotService));

        public PlotService()
        {
            javaID = (JNetBridge.Classes.JnetBridgeLoginUnit)Session["netCookieJavaSessionID"];
        }

        [WebMethod(true, Description = "Get Plot Info for Selected Last Known Position.")]
        public ArrayList GetLastKnownLocation(string requestId)
        {
            validateSession();

            JNetBridge.ReplyClasses.LastPositionJavaCallReply reply = JNetBridge.LastPositionJavaCall.RequestPosition(javaID, Int32.Parse(requestId));
            ArrayList list = new ArrayList();

            if (reply.MapDisplayHelper.AllPos != null)
            {
                FillLocationArray(reply, list);
            }
            return list;
        }

        [WebMethod(true, Description = "Get History Info for Selected Member.")]
        public ArrayList GetLocationHistory(string memberId, string alias, int numPositions, string fromDate, string toDate)
        {
            validateSession();

            if (fromDate.Trim() != "")
            {
                DateTime dtFrom = DateTime.Parse(fromDate, new CultureInfo(Session["UserCulture"].ToString(), false));
                fromDate = dtFrom.ToString("dd-MM-yyyy");
            }
            if (toDate.Trim() != "")
            {
                DateTime dtTo = DateTime.Parse(toDate, new CultureInfo(Session["UserCulture"].ToString(), false));
                toDate = dtTo.ToString("dd-MM-yyyy");
            }

            JNetBridge.ReplyClasses.LastPositionJavaCallReply reply = JNetBridge.LastPositionJavaCall.GetPositions(javaID, memberId, numPositions, fromDate, toDate);
            ArrayList list = new ArrayList();
            if (reply.MapDisplayHelper.AllPos != null)
            {
                FillLocationArray(reply, list);
            }
            //replace the number with alias
            foreach (object[] o in list)
            {
                o[0] = alias;
            }

            return list;
        }

        [WebMethod(true, Description = "Get Last Known Location For Selected Members.")]
        public ArrayList GetLastKnownLocationForSelectedMembers(string[] memberList)
        {
            validateSession();

            JNetBridge.ReplyClasses.LastPositionJavaCallReply reply = JNetBridge.LastPositionJavaCall.GetLastKnownPositions(javaID, JNetBridge.LastPositionJavaCall.postionType.historical, memberList, false);
            ArrayList list = new ArrayList();

            if (reply.MapDisplayHelper != null)
            {
                if (reply.MapDisplayHelper.AllPos != null)
                {
                    FillLocationArray(reply, list);
                }
            }
            return list;
        }

        [WebMethod(true, Description = "Get Location is a location request. A sms has been send to the unit for Selected Members.")]
        public ArrayList GetLocationForSelectedMembers(string[] memberList, Boolean GSM)
        {
            validateSession();

            JNetBridge.ReplyClasses.LastPositionJavaCallReply reply = JNetBridge.LastPositionJavaCall.GetLastKnownPositions(javaID, JNetBridge.LastPositionJavaCall.postionType.position, memberList, GSM);
            ArrayList list = new ArrayList();

            if (reply.MapDisplayHelper != null && reply.MapDisplayHelper.AllPos != null)
            {
                FillLocationArray(reply, list);
            }
            return list;
        }

        private static void FillLocationArray(JNetBridge.ReplyClasses.LastPositionJavaCallReply reply, ArrayList list)
        {

            int recordCount = reply.MapDisplayHelper.AllPos.Length;
            for (int i = 0; i < recordCount; i++)
            {
                object[] objTest = new object[13];
                objTest[0] = HttpUtility.UrlDecode(reply.MapDisplayHelper.AllPos[i].Label);
                objTest[1] = reply.MapDisplayHelper.AllPos[i].Y;
                objTest[2] = reply.MapDisplayHelper.AllPos[i].X;
                objTest[3] = reply.MapDisplayHelper.AllPos[i].StrRequestTime.ToString();
                objTest[4] = HttpUtility.UrlDecode(reply.MapDisplayHelper.AllPos[i].Town);
                objTest[5] = reply.MapDisplayHelper.AllPos[i].PostCode;
                objTest[6] = reply.MapDisplayHelper.AllPos[i].Direction;
                objTest[7] = reply.MapDisplayHelper.AllPos[i].Radius;
                objTest[8] = reply.MapDisplayHelper.AllPos[i].Accuracy;
                objTest[9] = reply.MapDisplayHelper.AllPos[i].Speed;
                objTest[10] = reply.MapDisplayHelper.AllPos[i].IconId;
                objTest[11] = reply.MapDisplayHelper.AllPos[i].AlertInfo;
                objTest[12] = FindWhere.Utils.Utils.GetAlertImage(reply.MapDisplayHelper.AllPos[i].AlertInfo);

                list.Add(objTest);
            }
        }


        [WebMethod(true, Description = "Get GeoFence Data")]
        public ArrayList GetGeoFenceItem(int geoFenceID)
        {
            validateSession();

            JNetBridge.ReplyClasses.GeofenceActionJavaCallReply reply = JNetBridge.GeofenceActionJavaCall.GetGeofenceAction(javaID, "GETLIST");
            // int zoomlevel = reply.Fleetonline_zoom_level.Content;
            com.teleca.fleetonline.repository.GeoFenceData gfdSelected = null;

            double radius = 0;

            ArrayList list = new ArrayList();
            foreach (com.teleca.fleetonline.repository.GeoFenceData gfd in reply.GeoFenceListDisplayHelper.DataList)
            {
                if (geoFenceID == int.Parse(gfd.FenceId))
                {
                    gfdSelected = gfd;
                    radius = gfd.Radius;
                }
            }

            // get the mapdata for the selected geofence 
            if (gfdSelected != null)
            {
                reply = JNetBridge.GeofenceActionJavaCall.getMap(javaID, gfdSelected.PostCode, gfdSelected.Radius, gfdSelected.LocType, gfdSelected.LocationX, gfdSelected.LocationY, 0);

                int recordCount = reply.GeoFenceLatLonglist.Count;
                for (int i = 0; i < recordCount; i++)
                {
                    object[] objTest = new object[3];
                    objTest[0] = ((LatLong)reply.GeoFenceLatLonglist[i]).getLatitude();
                    objTest[1] = ((LatLong)reply.GeoFenceLatLonglist[i]).getLongitude();
                    objTest[2] = radius;
                    list.Add(objTest);
                }
            }
            return list;
        }

        [WebMethod(true, Description = "Store New GeoFence.")]
        public Boolean StoreNewGeofence(string name, double radius, double GeoCentLat, double GeoCentLng)
        {
            validateSession();

            Boolean ttAdmin = (Boolean)Session["UsrTTAdmin"];
            if (!ttAdmin)
            {
                throw new UnauthorizedAccessException("NOT A TTADMIN");
            }

            Boolean nameExists = false;

            GeofenceActionJavaCallReply GeofenceActionJavaCallReply = JNetBridge.GeofenceActionJavaCall.GetGeofenceAction(javaID, "GETLIST");

            if (GeofenceActionJavaCallReply.GeoFenceListDisplayHelper.DataList != null)
            {
                foreach (GeoFenceData gfd in GeofenceActionJavaCallReply.GeoFenceListDisplayHelper.DataList)
                {
                    //ddlGeofences.Items.Add(new ListItem(gfd.Name, gfd.FenceId));
                    if (gfd.Name == name)
                        nameExists = true;
                }
            }

            if (nameExists)
                throw new Exception("Geofence name already exists");

            //radius = 0.25;	// tijdelijk i.v.m. de problematiek van de fencegroote
            JNetBridge.ReplyClasses.GeofenceActionJavaCallReply reply = JNetBridge.GeofenceActionJavaCall.GetGeofenceAction(javaID, JNetBridge.GeofenceActionJavaCall.GeofenceAction.STORE, -1, name, "", radius, 1, GeoCentLng, GeoCentLat);

            if (reply.AnswerHelper.get() == "success")
                return true;
            else
                return false;
        }

        [WebMethod(true, Description = "Store existing GeoFence.")]
        public Boolean EditGeofence(int fenceid, string name, double radius, double GeoCentLat, double GeoCentLng)
        {
            validateSession();

            Boolean ttAdmin = (Boolean)Session["UsrTTAdmin"];
            if (!ttAdmin)
            {
                throw new UnauthorizedAccessException("NOT A TTADMIN");
            }

            // radius = 0.25;	// tijdelijk i.v.m. de problematiek van de fencegroote
            JNetBridge.ReplyClasses.GeofenceActionJavaCallReply reply = JNetBridge.GeofenceActionJavaCall.GetGeofenceAction(javaID, JNetBridge.GeofenceActionJavaCall.GeofenceAction.STORE, fenceid, name, "", radius, 1, GeoCentLng, GeoCentLat);

            if (reply.AnswerHelper.get() == "success")
                return true;
            else
                return false;
        }

        [WebMethod(true, Description = "Delete existing GeoFence.")]
        public Boolean DeleteGeofence(string fenceid)
        {
            validateSession();

            Boolean ttAdmin = (Boolean)Session["UsrTTAdmin"];
            if (!ttAdmin)
            {
                throw new UnauthorizedAccessException("NOT A TTADMIN");
            }

            //check for an existing relation
            Boolean allowDelete = true;
            GeofenceMembListJavaCallReply gmr = GeofenceMembListJavaCall.GetGeofenceMembList(javaID);
            if (gmr.GeoFenceMembListDisplayHelper.DataList != null)
            {
                foreach (GeoFenceMembData gfmd in gmr.GeoFenceMembListDisplayHelper.DataList)
                {
                    if (gfmd.FenceId == fenceid)
                        allowDelete = false;
                }
            }

            if (allowDelete)
            {
                GeofenceActionJavaCall.GetGeofenceAction(javaID, GeofenceActionJavaCall.GeofenceAction.DEL, fenceid);
            }
            else
            {
                //throw new Exception("Fence still in use");
                throw new Exception(GetResource("plotservice_FenceStillInUse"));
            }

            return true;
        }

        [WebMethod(true, Description = "Get geofences")]
        public ArrayList GetGeofences()
        {
            validateSession();

            GeofenceActionJavaCallReply GeofenceActionJavaCallReply = JNetBridge.GeofenceActionJavaCall.GetGeofenceAction(javaID, "GETLIST");

            if (GeofenceActionJavaCallReply.GeoFenceListDisplayHelper.DataList == null)
                return new ArrayList();

            ArrayList list = new ArrayList();

            if (GeofenceActionJavaCallReply.GeoFenceListDisplayHelper.DataList != null)
            {
                foreach (GeoFenceData gfd in GeofenceActionJavaCallReply.GeoFenceListDisplayHelper.DataList)
                {
                    object[] objTest = new object[2];
                    objTest[0] = gfd.Name;
                    objTest[1] = gfd.FenceId;
                    list.Add(objTest);
                }
            }
            return list;
        }

        [WebMethod(true, Description = "Get help text by topic")]
        public string GetHelpText(string topic)
        {
            validateSession();

            return GetHelpResource(topic);
        }

        [WebMethod(true, Description = "Get Reverse geocode")]
        public object[] GetReverseGeoceode(string addresslabel, string postcodelabel, int LbsID)
        {
            validateSession();
            JNetBridge.ReplyClasses.GetReverseGeoceodeJavaCallReply reply = JNetBridge.GetReverseeGeocodeJavaCall.getAddress(javaID, LbsID);

            object[] objReturn = new object[5];
            objReturn[0] = HttpUtility.UrlDecode(reply.TownName);
            objReturn[1] = HttpUtility.UrlDecode(reply.Postcode);

            if (reply.TownName == null || reply.Postcode == null)
            {
                objReturn[0] = HttpUtility.UrlDecode(reply.Fleetonline_error_content.Content);
                objReturn[1] = "-";
            }

            objReturn[2] = HttpUtility.UrlDecode(reply.Fleetonline_error_content.Content);
            objReturn[3] = addresslabel;
            objReturn[4] = postcodelabel;

            return objReturn;
        }

        [WebMethod(true, Description = "Get Translated Resource")]
        public string GetResource(string resourceKey)
        {
            return FindWhere.Utils.Utils.GetResource(Session, resourceKey, Session["ThemeToUse"].ToString());

        }

        [WebMethod(true, Description = "Get Translated Help Resource")]
        public string GetHelpResource(string resourceKey)
        {
            if (resourceKey.IndexOf("Help_Content_MyAccount_") > -1 && Session["MyAccountOpenTab"] != null)
                resourceKey = Session["MyAccountOpenTab"].ToString();

            string retValue = FindWhere.Utils.Utils.GetResource(Session, resourceKey, "Help");
            if (!string.IsNullOrEmpty(retValue))
                return retValue;
            else
                return "No help found for key: " + resourceKey;
        }

        [WebMethod(true, Description = "Log a javascript error")]
        public void LogJavascriptError(string message, string url, string line)
        {
            string error = string.Concat("Javascript error: ", Environment.NewLine, "Message:", message);
            error = string.Concat(error, " url: ", url, " line: ", line);
            error += FindWhere.Utils.Utils.GetSessionParams(Session);
            log.Error(error);
        }

        private void validateSession()
        {
            if (Session == null)
                throw new UnauthorizedAccessException("NO_SESSION");

            if (!FindWhere.Utils.Utils.ValidateTimeoutFromWebservice(Session))
                throw new UnauthorizedAccessException("TIMEOUT_SESSION");
        }
    }
}
