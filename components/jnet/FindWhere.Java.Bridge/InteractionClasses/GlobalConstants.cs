namespace com.teleca.fleetonline.utils
{

    ///
    // * @author FlachV
    // *
    // * To change this generated comment edit the template variable "typecomment":
    // * Window>Preferences>Java>Templates.
    // * To enable and disable the creation of type comments go to
    // * Window>Preferences>Java>Code Generation.
    // 
    [System.Serializable]
    public class GlobalConstants
    {

        // NB! Be careful when changing these numbers; there can
        // be dependencies from property files, SQL procedures etc.        

        public const int OPERATOR_UNDEFINED = 9999;
        public const int PROVIDER_UNDEFINED = 0;
        public const int MEMBERTYPE_UNREGISTERED = 0;
        public const int LBS_UNDEFINED = 0; //Undefined state, should never happen.

        // User types identifiers
        public const int USER_TYPE_ID_TTVAM = 3; // Trimtrac with a VAM
        public const int USER_TYPE_ID_OCELLUS = 4; // Ocellus
        public const int USER_TYPE_ID_TT_1_5 = 10; // Trimtrac 1.5
        public const int USER_TYPE_ID_TT_1_5VAM = 11; // Trimtrac 1.5 with a VAM     

        // SM Status
        public const int SM_UNDEFINED = 0; //Undefined state, should never happen.

        // SM Direction      
        public const int SM_DIRECTION_MT = 1;

        // User active
        public const int USER_ACTIVE_UNDEFINED = 2;

        //GIS related Constants
        //Units
        public const int DISTANCE_KM = 0;
        public const int DISTANCE_MILES = 1;

        // Deliver(ed) notification by email or sms
        public const int NOTIFICATION_MSG_CARRIER_EMAIL = 0;
        public const int NOTIFICATION_MSG_CARRIER_SMS = 1;

        // Notification event numbers       
        public const int NOTIFICATION_EVENT_INSIDE = 100;
        public const int NOTIFICATION_EVENT_OUTSIDE = 101;
        public const int NOTIFICATION_EVENT_CREDIT_LOW = 102;
        public const int NOTIFICATION_EVENT_TEST_MSG = 103;
        public const int NOTIFICATION_EVENT_CREDIT_ZERO = 104;
        public const int NOTIFICATION_EVENT_SMS_ACTION_POSITION = 106;
        public const int NOTIFICATION_EVENT_TT_BATTERY_LEVEL = 200;
        public const int NOTIFICATION_EVENT_TT_MOVEMENT = 204;           

        //tt 1.5 notifications       
        public const int NOTIFICATION_EVENT_TT15_SPEED_VIOLATION = 501;
        public const int NOTIFICATION_EVENT_TT15_GEOFENCE_VIOLATION_IN = 505;
        public const int NOTIFICATION_EVENT_TT15_GEOFENCE_VIOLATION_OUT = 506;

        // Enfora
        public const int NOTIFICATION_EVENT_ENFORA_INPUT1 = 601;
        public const int NOTIFICATION_EVENT_ENFORA_INPUT2 = 602;
        public const int NOTIFICATION_EVENT_ENFORA_INPUT3 = 603;
        public const int NOTIFICATION_EVENT_ENFORA_PERIODIC_REPORT = 604;

        //wi
        public const int NOTIFICATION_EVENT_WI_PANIC_BUTTON = 800;

        // config status (for example, for profiles or device-based geofences)
        public const int CONFIG_STATUS_UNDEFINED = -1;

        // direction constants
        public const string NODIRECTION = "-";
        public const string NORTH = "N";
        public const string NORTHEAST = "NE";
        public const string EAST = "E";
        public const string SOUTHEAST = "SE";
        public const string SOUTH = "S";
        public const string SOUTHWEST = "SW";
        public const string WEST = "W";
        public const string NORTHWEST = "NW";
    }
}