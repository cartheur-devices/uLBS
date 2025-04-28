using System;
using System.Collections.Generic;
using System.Text;
using JNetBridge.InteractionClasses;
using com.teleca.fleetonline.web.bean;
using System.Collections;
using com.teleca.fleetonline.mapping;

namespace JNetBridge.ReplyClasses
{
    public class GeofenceActionJavaCallReply : JavaCallReply
    {
        private GeoFenceListDisplayHelper fleetonline_geofencelist;
        private ResponseTypeHelper  fleetonline_request_type;
        private AnswerHelper  fleetonline_request_status;
        private MiscContentHelper m_MiscContentHelper;
        private MapDisplayHelper fleetonline_trace_data;
        private ZoomLevelHelper fleetonline_zoom_level;

        public ZoomLevelHelper Fleetonline_zoom_level
        {
            get { return fleetonline_zoom_level; }
            set { fleetonline_zoom_level = value; }
        }

        public com.teleca.fleetonline.web.bean.MapDisplayHelper Fleetonline_trace_data
        {
            get { return fleetonline_trace_data; }
            set { fleetonline_trace_data = value; }
        }

        public ArrayList GeoFenceLatLonglist
        {
            get 
            {
                ArrayList retList = new ArrayList();
                if (this.fleetonline_trace_data.GeofenceCornerCoordinates != null)
                {
                    foreach (LatLong ll in this.fleetonline_trace_data.GeofenceCornerCoordinates)
                    {
                        retList.Add(ll);
                    }
                    return retList;
                }

                else
                {   
                    return retList;
                }
            }
        }

        //private UserData  m_UserData;
        
        public GeofenceActionJavaCallReply()
        {
        }

        public GeofenceActionJavaCallReply(GeoFenceListDisplayHelper gdh, String replyIn)
        {
            fleetonline_geofencelist = gdh;
           this.Reply = replyIn;
        }

        public GeoFenceListDisplayHelper GeoFenceListDisplayHelper { get { return fleetonline_geofencelist; } set { fleetonline_geofencelist = value; } }
        public ResponseTypeHelper ResponseTypeHelper { get { return fleetonline_request_type; } set { fleetonline_request_type = value; } }
        public AnswerHelper AnswerHelper { get { return fleetonline_request_status; } set { fleetonline_request_status = value; } }
        public MiscContentHelper MiscContentHelper { get { return m_MiscContentHelper; } set { m_MiscContentHelper = value; } }


        //public UserData UserData 
        //{
        //    get
        //    {
        //        return m_UserData; 
        //    }
        //    set 
        //    {
        //        m_UserData = value;
                
        //    }
        //}

       


               
    }
}
