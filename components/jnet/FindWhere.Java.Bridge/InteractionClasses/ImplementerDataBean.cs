//*************************************************************************

//$Archive: /sdt/product/fol/versions/head/src/java/ejb_module/src/com/teleca/fleetonline/charging/ImplementerDataBean.java $
//$Revision: 1.3 $
//$Date: 2008/11/25 11:09:08 $
//$Author: salih.canoz $

//*************************************************************************

//
// * @(#)ImplementerDataBean	1.0e 15/11/2002
// *
// * Copyright (c) 2002 Teleca AU.
// * Bartholomew's Gate, 11-13 Charterhouse Buildings, EC1M 7AP
// * All rights reserved.
// *
// * This software is the confidential and proprietary information of Teleca AU.
// * ("Confidential Information").  You shall not
// * disclose such Confidential Information and shall use it only in
// * accordance with the terms of the license agreement you entered into
// * with Teleca AU.
// 

namespace com.teleca.fleetonline.charging
{


    ///
    // * Holds Data for Implementer set by XMLConfigHandler
    // *
    // * @author $Author: salih.canoz $
    // * @version $Revision: 1.3 $, $Date: 2008/11/25 11:09:08 $
    // 
    public class ImplementerDataBean
    {

        //properties of bean
        /// BILLING_METHOD 0 = PSMS 
        private int BILLING_METHOD;

        /// PSM_TARIF the cost of a Premium SM 
        private float PSM_TARIF;

        /// PSM_SHORTCODE maps to a tarif defined by the provider (EP SCs for know) 
        private string PSM_SHORTCODE;

        /// SMS_TARIF the cost of a SM 
        private float SM_TARIF;

        /// LBS_TARIF the cost of a LBS request
        private float LBS_TARIF;

        /// the cost of a notification by email 
        private float NOTIFICATION_EMAIL_TARIF;

        /// the cost of a notification by sms 
        private float NOTIFICATION_SMS_TARIF;

        /// MAP_TARIF the cost of a Map 
        private float MAP_TARIF;

        //    * MIN_CREDIT_VALUE defines the minimum value the balance may reach,
        //	 *  before chargable requests are rejected.
        //	 
        private float MIN_CREDIT_VALUE;

        /// CREDIT_THRESHHOLD_VALUE when this treshhold is reached a billing event is triggered 
        private float CREDIT_THRESHHOLD_VALUE;

        /// MAX_REQUEST_VALUE the maxium size of request allowed 
        private float MAX_REQUEST_VALUE;

        /// ADMIN_FEE the Administration Fee charged during Registration 
        private float ADMIN_FEE;

        /// SETUP_FEE the actual cost of administration - needed to set the user's balance when added to the system 
        private float SETUP_FEE;

        /// CURRENCY 
        private int CURRENCY;

        /// OCELLUSLOC_TARIF the cost of a OCELLUS location report 
        private float OCELLUSLOC_RESP_TARIF_SMS;
        private float OCELLUSLOC_RESP_TARIF_GPRS;
        private float OCELLUSLOC_RESP_TARIF_SMS_ROAMING;
        private float OCELLUSLOC_RESP_TARIF_GPRS_ROAMING;
        /// OCELLUSPOSREQ_TARIF the cost of an Ocellus location request 
        private float OCELLUSPOSREQ_TARIF_SMS;
        private float OCELLUSPOSREQ_TARIF_GPRS;
        /// OCELLUSCONFIGREQ_TARIF the cost of an OCELLUS location report 
        private float OCELLUSCONFIGREQ_TARIF_SMS;
        private float OCELLUSCONFIGREQ_TARIF_GPRS;
        /// OCELLUSCONFIGRESP_TARIF the cost of an OCELLUS location report 
        private float OCELLUSCONFIGRESP_TARIF_SMS;
        private float OCELLUSCONFIGRESP_TARIF_GPRS;
        /// OCELLUSGeofenceReq_TARIF the cost of an OCELLUS set geofence 
        private float OCELLUSGeofenceReq_TARIF_SMS;
        private float OCELLUSGeofenceReq_TARIF_GPRS;


        /// TRIMTRAC_TARIF the cost of a TRIMTRAC location report 
        private float TTLOC_RESP_TARIF_SMS;
        private float TTLOC_RESP_TARIF_GPRS;
        private float TTLOC_RESP_TARIF_SMS_ROAMING;
        private float TTLOC_RESP_TARIF_GPRS_ROAMING;
        /// TTPOSREQ_TARIF the cost of a TRIMTRAC position request 
        private float TTPOSREQ_TARIF_SMS;
        private float TTPOSREQ_TARIF_GPRS;
        /// TRIMTRAC_TARIF the cost of a TRIMTRAC status report 
        private float TTSTAT_TARIF_SMS;
        private float TTSTAT_TARIF_GPRS;
        /// TRIMTRAC_TARIF the cost of a TRIMTRAC status report 

        private float TTCONFIG_RESP_TARIF_SMS;
        private float TTCONFIG_REQ_TARIF_SMS;
        private float TTCONFIG_RESP_TARIF_GPRS;
        private float TTCONFIG_REQ_TARIF_GPRS;

        private float WI_LOC_REQ_TARIF_SMS;
        private float WI_LOC_RESP_TARIF_SMS;
        private float WI_LOC_RESP_TARIF_GPRS;
        private float WI_LOC_RESP_TARIF_SMS_ROAMING;
        private float WI_LOC_RESP_TARIF_GPRS_ROAMING;
        private float WICONFIG_RESP_TARIF_GPRS;
        private float WICONFIG_REQ_TARIF_GPRS;
        private float WICONFIG_RESP_TARIF_SMS;
        private float WICONFIG_REQ_TARIF_SMS;

        private float ENFORA_LOC_REQ_TARIF_SMS;
        private float ENFORA_LOC_RESP_TARIF_SMS;
        private float ENFORA_LOC_RESP_TARIF_GPRS;
        private float ENFORA_LOC_RESP_TARIF_SMS_ROAMING;
        private float ENFORA_LOC_RESP_TARIF_GPRS_ROAMING;
        private float ENFORACONFIG_RESP_TARIF_GPRS;
        private float ENFORACONFIG_REQ_TARIF_GPRS;
        private float ENFORACONFIG_RESP_TARIF_SMS;
        private float ENFORACONFIG_REQ_TARIF_SMS;

        private float TM_LOC_REQ_TARIF_SMS;
        private float TM_LOC_RESP_TARIF_SMS;
        private float TM_LOC_RESP_TARIF_GPRS;
        private float TM_LOC_RESP_TARIF_SMS_ROAMING;
        private float TM_LOC_RESP_TARIF_GPRS_ROAMING;
        private float TMCONFIG_RESP_TARIF_GPRS;
        private float TMCONFIG_REQ_TARIF_GPRS;
        private float TMCONFIG_RESP_TARIF_SMS;
        private float TMCONFIG_REQ_TARIF_SMS;        

        private float REVERSE_GEOCODING_TARIF;

        private float TEST_EMAIL_TARIF;
        private float TEST_SMS_TARIF;

        public virtual float getADMIN_FEE()
        {
            return ADMIN_FEE;
        }
        public virtual int getBILLING_METHOD()
        {
            return BILLING_METHOD;
        }
        public virtual float getCREDIT_THRESHHOLD_VALUE()
        {
            return CREDIT_THRESHHOLD_VALUE;
        }
        public virtual int getCURRENCY()
        {
            return CURRENCY;
        }

        public virtual float getLBS_TARIF()
        {
            return LBS_TARIF;
        }
        public virtual float getMAP_TARIF()
        {
            return MAP_TARIF;
        }
        public virtual float getMAX_REQUEST_VALUE()
        {
            return MAX_REQUEST_VALUE;
        }

        public virtual float getMIN_CREDIT_VALUE()
        {
            return MIN_CREDIT_VALUE;
        }
        public virtual float getNOTIFICATION_EMAIL_TARIF()
        {
            return NOTIFICATION_EMAIL_TARIF;
        }
        public virtual float getNOTIFICATION_SMS_TARIF()
        {
            return NOTIFICATION_SMS_TARIF;
        }
        public virtual float getOCELLUSCONFIGREQ_TARIF_GPRS()
        {
            return OCELLUSCONFIGREQ_TARIF_GPRS;
        }
        public virtual float getOCELLUSCONFIGREQ_TARIF_SMS()
        {
            return OCELLUSCONFIGREQ_TARIF_SMS;
        }
        public virtual float getOCELLUSCONFIGRESP_TARIF_GPRS()
        {
            return OCELLUSCONFIGRESP_TARIF_GPRS;
        }
        public virtual float getOCELLUSCONFIGRESP_TARIF_SMS()
        {
            return OCELLUSCONFIGRESP_TARIF_SMS;
        }
        public virtual float getOCELLUSGeofenceReq_TARIF_GPRS()
        {
            return OCELLUSGeofenceReq_TARIF_GPRS;
        }
        public virtual float getOCELLUSGeofenceReq_TARIF_SMS()
        {
            return OCELLUSGeofenceReq_TARIF_SMS;
        }
        public virtual float getOCELLUSLOC_RESP_TARIF_GPRS()
        {
            return OCELLUSLOC_RESP_TARIF_GPRS;
        }
        public virtual float getOCELLUSLOC_RESP_TARIF_GPRS_ROAMING()
        {
            return OCELLUSLOC_RESP_TARIF_GPRS_ROAMING;
        }
        public virtual float getOCELLUSLOC_RESP_TARIF_SMS()
        {
            return OCELLUSLOC_RESP_TARIF_SMS;
        }
        public virtual float getOCELLUSLOC_RESP_TARIF_SMS_ROAMING()
        {
            return OCELLUSLOC_RESP_TARIF_SMS_ROAMING;
        }
        public virtual float getOCELLUSPOSREQ_TARIF_GPRS()
        {
            return OCELLUSPOSREQ_TARIF_GPRS;
        }
        public virtual float getOCELLUSPOSREQ_TARIF_SMS()
        {
            return OCELLUSPOSREQ_TARIF_SMS;
        }
        public virtual string getPSM_SHORTCODE()
        {
            return PSM_SHORTCODE;
        }
        public virtual float getPSM_TARIF()
        {
            return PSM_TARIF;
        }
        public virtual float getSETUP_FEE()
        {
            return SETUP_FEE;
        }
        public virtual float getSM_TARIF()
        {
            return SM_TARIF;
        }
        public virtual float getTTCONFIG_REQ_TARIF_GPRS()
        {
            return TTCONFIG_REQ_TARIF_GPRS;
        }
        public virtual float getTTCONFIG_REQ_TARIF_SMS()
        {
            return TTCONFIG_REQ_TARIF_SMS;
        }
        public virtual float getTTCONFIG_RESP_TARIF_GPRS()
        {
            return TTCONFIG_RESP_TARIF_GPRS;
        }
        public virtual float getTTCONFIG_RESP_TARIF_SMS()
        {
            return TTCONFIG_RESP_TARIF_SMS;
        }
        public virtual float getTTLOC_RESP_TARIF_GPRS()
        {
            return TTLOC_RESP_TARIF_GPRS;
        }
        public virtual float getTTLOC_RESP_TARIF_GPRS_ROAMING()
        {
            return TTLOC_RESP_TARIF_GPRS_ROAMING;
        }
        public virtual float getTTLOC_RESP_TARIF_SMS()
        {
            return TTLOC_RESP_TARIF_SMS;
        }
        public virtual float getTTLOC_RESP_TARIF_SMS_ROAMING()
        {
            return TTLOC_RESP_TARIF_SMS_ROAMING;
        }
        public virtual float getTTPOSREQ_TARIF_GPRS()
        {
            return TTPOSREQ_TARIF_GPRS;
        }
        public virtual float getTTPOSREQ_TARIF_SMS()
        {
            return TTPOSREQ_TARIF_SMS;
        }
        public virtual float getTTSTAT_TARIF_GPRS()
        {
            return TTSTAT_TARIF_GPRS;
        }
        public virtual float getTTSTAT_TARIF_SMS()
        {
            return TTSTAT_TARIF_SMS;
        }
        public virtual void setADMIN_FEE(float admin_fee)
        {
            ADMIN_FEE = admin_fee;
        }
        public virtual void setBILLING_METHOD(int billing_method)
        {
            BILLING_METHOD = billing_method;
        }
        public virtual void setCREDIT_THRESHHOLD_VALUE(float credit_threshhold_value)
        {
            CREDIT_THRESHHOLD_VALUE = credit_threshhold_value;
        }
        public virtual void setCURRENCY(int currency)
        {
            CURRENCY = currency;
        }

        public virtual void setLBS_TARIF(float lbs_tarif)
        {
            LBS_TARIF = lbs_tarif;
        }
        public virtual void setMAP_TARIF(float map_tarif)
        {
            MAP_TARIF = map_tarif;
        }
        public virtual void setMAX_REQUEST_VALUE(float max_request_value)
        {
            MAX_REQUEST_VALUE = max_request_value;
        }
        public virtual void setMIN_CREDIT_VALUE(float min_credit_value)
        {
            MIN_CREDIT_VALUE = min_credit_value;
        }
        public virtual void setNOTIFICATION_EMAIL_TARIF(float notification_email_tarif)
        {
            NOTIFICATION_EMAIL_TARIF = notification_email_tarif;
        }
        public virtual void setNOTIFICATION_SMS_TARIF(float notification_sms_tarif)
        {
            NOTIFICATION_SMS_TARIF = notification_sms_tarif;
        }
        public virtual void setOCELLUSCONFIGREQ_TARIF_GPRS(float ocellusconfigreq_tarif_gprs)
        {
            OCELLUSCONFIGREQ_TARIF_GPRS = ocellusconfigreq_tarif_gprs;
        }
        public virtual void setOCELLUSCONFIGREQ_TARIF_SMS(float ocellusconfigreq_tarif_sms)
        {
            OCELLUSCONFIGREQ_TARIF_SMS = ocellusconfigreq_tarif_sms;
        }
        public virtual void setOCELLUSCONFIGRESP_TARIF_GPRS(float ocellusconfigresp_tarif_gprs)
        {
            OCELLUSCONFIGRESP_TARIF_GPRS = ocellusconfigresp_tarif_gprs;
        }
        public virtual void setOCELLUSCONFIGRESP_TARIF_SMS(float ocellusconfigresp_tarif_sms)
        {
            OCELLUSCONFIGRESP_TARIF_SMS = ocellusconfigresp_tarif_sms;
        }
        public virtual void setOCELLUSGeofenceReq_TARIF_GPRS(float geofenceReq_TARIF_GPRS)
        {
            OCELLUSGeofenceReq_TARIF_GPRS = geofenceReq_TARIF_GPRS;
        }
        public virtual void setOCELLUSGeofenceReq_TARIF_SMS(float geofenceReq_TARIF_SMS)
        {
            OCELLUSGeofenceReq_TARIF_SMS = geofenceReq_TARIF_SMS;
        }
        public virtual void setOCELLUSLOC_RESP_TARIF_GPRS(float ocellusloc_resp_tarif_gprs)
        {
            OCELLUSLOC_RESP_TARIF_GPRS = ocellusloc_resp_tarif_gprs;
        }
        public virtual void setOCELLUSLOC_RESP_TARIF_GPRS_ROAMING(float ocellusloc_resp_tarif_gprs_roaming)
        {
            OCELLUSLOC_RESP_TARIF_GPRS_ROAMING = ocellusloc_resp_tarif_gprs_roaming;
        }
        public virtual void setOCELLUSLOC_RESP_TARIF_SMS(float ocellusloc_resp_tarif_sms)
        {
            OCELLUSLOC_RESP_TARIF_SMS = ocellusloc_resp_tarif_sms;
        }
        public virtual void setOCELLUSLOC_RESP_TARIF_SMS_ROAMING(float ocellusloc_resp_tarif_sms_roaming)
        {
            OCELLUSLOC_RESP_TARIF_SMS_ROAMING = ocellusloc_resp_tarif_sms_roaming;
        }
        public virtual void setOCELLUSPOSREQ_TARIF_GPRS(float ocellusposreq_tarif_gprs)
        {
            OCELLUSPOSREQ_TARIF_GPRS = ocellusposreq_tarif_gprs;
        }
        public virtual void setOCELLUSPOSREQ_TARIF_SMS(float ocellusposreq_tarif_sms)
        {
            OCELLUSPOSREQ_TARIF_SMS = ocellusposreq_tarif_sms;
        }
        public virtual void setPSM_SHORTCODE(string psm_shortcode)
        {
            PSM_SHORTCODE = psm_shortcode;
        }
        public virtual void setPSM_TARIF(float psm_tarif)
        {
            PSM_TARIF = psm_tarif;
        }
        public virtual void setSETUP_FEE(float setup_fee)
        {
            SETUP_FEE = setup_fee;
        }
        public virtual void setSM_TARIF(float sm_tarif)
        {
            SM_TARIF = sm_tarif;
        }
        public virtual void setTTCONFIG_REQ_TARIF_GPRS(float ttconfig_req_tarif_gprs)
        {
            TTCONFIG_REQ_TARIF_GPRS = ttconfig_req_tarif_gprs;
        }
        public virtual void setTTCONFIG_REQ_TARIF_SMS(float ttconfig_req_tarif_sms)
        {
            TTCONFIG_REQ_TARIF_SMS = ttconfig_req_tarif_sms;
        }
        public virtual void setTTCONFIG_RESP_TARIF_GPRS(float ttconfig_resp_tarif_gprs)
        {
            TTCONFIG_RESP_TARIF_GPRS = ttconfig_resp_tarif_gprs;
        }
        public virtual void setTTCONFIG_RESP_TARIF_SMS(float ttconfig_resp_tarif_sms)
        {
            TTCONFIG_RESP_TARIF_SMS = ttconfig_resp_tarif_sms;
        }
        public virtual void setTTLOC_RESP_TARIF_GPRS(float ttloc_resp_tarif_gprs)
        {
            TTLOC_RESP_TARIF_GPRS = ttloc_resp_tarif_gprs;
        }
        public virtual void setTTLOC_RESP_TARIF_GPRS_ROAMING(float ttloc_resp_tarif_gprs_roaming)
        {
            TTLOC_RESP_TARIF_GPRS_ROAMING = ttloc_resp_tarif_gprs_roaming;
        }
        public virtual void setTTLOC_RESP_TARIF_SMS(float ttloc_resp_tarif_sms)
        {
            TTLOC_RESP_TARIF_SMS = ttloc_resp_tarif_sms;
        }
        public virtual void setTTLOC_RESP_TARIF_SMS_ROAMING(float ttloc_resp_tarif_sms_roaming)
        {
            TTLOC_RESP_TARIF_SMS_ROAMING = ttloc_resp_tarif_sms_roaming;
        }
        public virtual void setTTPOSREQ_TARIF_GPRS(float ttposreq_tarif_gprs)
        {
            TTPOSREQ_TARIF_GPRS = ttposreq_tarif_gprs;
        }
        public virtual void setTTPOSREQ_TARIF_SMS(float ttposreq_tarif_sms)
        {
            TTPOSREQ_TARIF_SMS = ttposreq_tarif_sms;
        }
        public virtual void setTTSTAT_TARIF_GPRS(float ttstat_tarif_gprs)
        {
            TTSTAT_TARIF_GPRS = ttstat_tarif_gprs;
        }
        public virtual void setTTSTAT_TARIF_SMS(float ttstat_tarif_sms)
        {
            TTSTAT_TARIF_SMS = ttstat_tarif_sms;
        }
        public virtual float getREVERSE_GEOCODING_TARIF()
        {
            return REVERSE_GEOCODING_TARIF;
        }
        public virtual void setREVERSE_GEOCODING_TARIF(float reverse_geocoding_tarif)
        {
            REVERSE_GEOCODING_TARIF = reverse_geocoding_tarif;
        }

        public virtual float getTEST_EMAIL_TARIF()
        {
            return TEST_EMAIL_TARIF;
        }
        public virtual void setTEST_EMAIL_TARIF(float test_email)
        {
            TEST_EMAIL_TARIF = test_email;
        }

        public virtual float getTEST_SMS_TARIF()
        {
            return TEST_SMS_TARIF;
        }
        public virtual void setTEST_SMS_TARIF(float test)
        {
            TEST_SMS_TARIF = test;
        }
        public virtual float getWI_LOC_RESP_TARIF_GPRS()
        {
            return WI_LOC_RESP_TARIF_GPRS;
        }
        public virtual void setWI_LOC_RESP_TARIF_GPRS(float wi_loc_resp_tarif_gprs)
        {
            WI_LOC_RESP_TARIF_GPRS = wi_loc_resp_tarif_gprs;
        }
        public virtual float getWI_LOC_RESP_TARIF_GPRS_ROAMING()
        {
            return WI_LOC_RESP_TARIF_GPRS_ROAMING;
        }
        public virtual void setWI_LOC_RESP_TARIF_GPRS_ROAMING(float wi_loc_resp_tarif_gprs_roaming)
        {
            WI_LOC_RESP_TARIF_GPRS_ROAMING = wi_loc_resp_tarif_gprs_roaming;
        }
        public virtual float getWI_LOC_RESP_TARIF_SMS()
        {
            return WI_LOC_RESP_TARIF_SMS;
        }
        public virtual void setWI_LOC_RESP_TARIF_SMS(float wi_loc_resp_tarif_sms)
        {
            WI_LOC_RESP_TARIF_SMS = wi_loc_resp_tarif_sms;
        }
        public virtual float getWI_LOC_RESP_TARIF_SMS_ROAMING()
        {
            return WI_LOC_RESP_TARIF_SMS_ROAMING;
        }
        public virtual void setWI_LOC_RESP_TARIF_SMS_ROAMING(float wi_loc_resp_tarif_sms_roaming)
        {
            WI_LOC_RESP_TARIF_SMS_ROAMING = wi_loc_resp_tarif_sms_roaming;
        }
        public virtual float getWICONFIG_REQ_TARIF_GPRS()
        {
            return WICONFIG_REQ_TARIF_GPRS;
        }
        public virtual void setWICONFIG_REQ_TARIF_GPRS(float wiconfig_req_tarif_gprs)
        {
            WICONFIG_REQ_TARIF_GPRS = wiconfig_req_tarif_gprs;
        }
        public virtual float getWICONFIG_RESP_TARIF_GPRS()
        {
            return WICONFIG_RESP_TARIF_GPRS;
        }
        public virtual void setWICONFIG_RESP_TARIF_GPRS(float wiconfig_resp_tarif_gprs)
        {
            WICONFIG_RESP_TARIF_GPRS = wiconfig_resp_tarif_gprs;
        }
        public virtual float getWICONFIG_RESP_TARIF_SMS()
        {
            return WICONFIG_RESP_TARIF_SMS;
        }
        public virtual void setWICONFIG_RESP_TARIF_SMS(float wiconfig_resp_tarif_sms)
        {
            WICONFIG_RESP_TARIF_SMS = wiconfig_resp_tarif_sms;
        }
        public virtual float getWICONFIG_REQ_TARIF_SMS()
        {
            return WICONFIG_REQ_TARIF_SMS;
        }
        public virtual void setWICONFIG_REQ_TARIF_SMS(float wiconfig_req_tarif_sms)
        {
            WICONFIG_REQ_TARIF_SMS = wiconfig_req_tarif_sms;
        }
        public virtual float getWI_LOC_REQ_TARIF_SMS()
        {
            return WI_LOC_REQ_TARIF_SMS;
        }
        public virtual void setWI_LOC_REQ_TARIF_SMS(float wi_loc_req_tarif_sms)
        {
            WI_LOC_REQ_TARIF_SMS = wi_loc_req_tarif_sms;
        }
        public virtual float getENFORA_LOC_REQ_TARIF_SMS()
        {
            return ENFORA_LOC_REQ_TARIF_SMS;
        }
        public virtual void setENFORA_LOC_REQ_TARIF_SMS(float enfora_loc_req_tarif_sms)
        {
            ENFORA_LOC_REQ_TARIF_SMS = enfora_loc_req_tarif_sms;
        }
        public virtual float getENFORA_LOC_RESP_TARIF_SMS()
        {
            return ENFORA_LOC_RESP_TARIF_SMS;
        }
        public virtual void setENFORA_LOC_RESP_TARIF_SMS(float enfora_loc_resp_tarif_sms)
        {
            ENFORA_LOC_RESP_TARIF_SMS = enfora_loc_resp_tarif_sms;
        }
        public virtual float getENFORA_LOC_RESP_TARIF_GPRS()
        {
            return ENFORA_LOC_RESP_TARIF_GPRS;
        }
        public virtual void setENFORA_LOC_RESP_TARIF_GPRS(float enfora_loc_resp_tarif_gprs)
        {
            ENFORA_LOC_RESP_TARIF_GPRS = enfora_loc_resp_tarif_gprs;
        }
        public virtual float getENFORA_LOC_RESP_TARIF_SMS_ROAMING()
        {
            return ENFORA_LOC_RESP_TARIF_SMS_ROAMING;
        }
        public virtual void setENFORA_LOC_RESP_TARIF_SMS_ROAMING(float enfora_loc_resp_tarif_sms_roaming)
        {
            ENFORA_LOC_RESP_TARIF_SMS_ROAMING = enfora_loc_resp_tarif_sms_roaming;
        }
        public virtual float getENFORA_LOC_RESP_TARIF_GPRS_ROAMING()
        {
            return ENFORA_LOC_RESP_TARIF_GPRS_ROAMING;
        }
        public virtual void setENFORA_LOC_RESP_TARIF_GPRS_ROAMING(float enfora_loc_resp_tarif_gprs_roaming)
        {
            ENFORA_LOC_RESP_TARIF_GPRS_ROAMING = enfora_loc_resp_tarif_gprs_roaming;
        }
        public virtual float getENFORACONFIG_RESP_TARIF_GPRS()
        {
            return ENFORACONFIG_RESP_TARIF_GPRS;
        }
        public virtual void setENFORACONFIG_RESP_TARIF_GPRS(float enforaconfig_resp_tarif_gprs)
        {
            ENFORACONFIG_RESP_TARIF_GPRS = enforaconfig_resp_tarif_gprs;
        }
        public virtual float getENFORACONFIG_REQ_TARIF_GPRS()
        {
            return ENFORACONFIG_REQ_TARIF_GPRS;
        }
        public virtual void setENFORACONFIG_REQ_TARIF_GPRS(float enforaconfig_req_tarif_gprs)
        {
            ENFORACONFIG_REQ_TARIF_GPRS = enforaconfig_req_tarif_gprs;
        }
        public virtual float getENFORACONFIG_RESP_TARIF_SMS()
        {
            return ENFORACONFIG_RESP_TARIF_SMS;
        }
        public virtual void setENFORACONFIG_RESP_TARIF_SMS(float enforaconfig_resp_tarif_sms)
        {
            ENFORACONFIG_RESP_TARIF_SMS = enforaconfig_resp_tarif_sms;
        }
        public virtual float getENFORACONFIG_REQ_TARIF_SMS()
        {
            return ENFORACONFIG_REQ_TARIF_SMS;
        }
        public virtual void setENFORACONFIG_REQ_TARIF_SMS(float enforaconfig_req_tarif_sms)
        {
            ENFORACONFIG_REQ_TARIF_SMS = enforaconfig_req_tarif_sms;
        }
        //    *
        //	 * @return the tM_LOC_REQ_TARIF_SMS
        //	 
        public virtual float getTM_LOC_REQ_TARIF_SMS()
        {
            return TM_LOC_REQ_TARIF_SMS;
        }
        //    *
        //	 * @param tm_loc_req_tarif_sms the tM_LOC_REQ_TARIF_SMS to set
        //	 
        public virtual void setTM_LOC_REQ_TARIF_SMS(float tm_loc_req_tarif_sms)
        {
            TM_LOC_REQ_TARIF_SMS = tm_loc_req_tarif_sms;
        }
        //    *
        //	 * @return the tM_LOC_RESP_TARIF_SMS
        //	 
        public virtual float getTM_LOC_RESP_TARIF_SMS()
        {
            return TM_LOC_RESP_TARIF_SMS;
        }
        //    *
        //	 * @param tm_loc_resp_tarif_sms the tM_LOC_RESP_TARIF_SMS to set
        //	 
        public virtual void setTM_LOC_RESP_TARIF_SMS(float tm_loc_resp_tarif_sms)
        {
            TM_LOC_RESP_TARIF_SMS = tm_loc_resp_tarif_sms;
        }
        //    *
        //	 * @return the tM_LOC_RESP_TARIF_GPRS
        //	 
        public virtual float getTM_LOC_RESP_TARIF_GPRS()
        {
            return TM_LOC_RESP_TARIF_GPRS;
        }
        //    *
        //	 * @param tm_loc_resp_tarif_gprs the tM_LOC_RESP_TARIF_GPRS to set
        //	 
        public virtual void setTM_LOC_RESP_TARIF_GPRS(float tm_loc_resp_tarif_gprs)
        {
            TM_LOC_RESP_TARIF_GPRS = tm_loc_resp_tarif_gprs;
        }
        //    *
        //	 * @return the tM_LOC_RESP_TARIF_SMS_ROAMING
        //	 
        public virtual float getTM_LOC_RESP_TARIF_SMS_ROAMING()
        {
            return TM_LOC_RESP_TARIF_SMS_ROAMING;
        }
        //    *
        //	 * @param tm_loc_resp_tarif_sms_roaming the tM_LOC_RESP_TARIF_SMS_ROAMING to set
        //	 
        public virtual void setTM_LOC_RESP_TARIF_SMS_ROAMING(float tm_loc_resp_tarif_sms_roaming)
        {
            TM_LOC_RESP_TARIF_SMS_ROAMING = tm_loc_resp_tarif_sms_roaming;
        }
        //    *
        //	 * @return the tM_LOC_RESP_TARIF_GPRS_ROAMING
        //	 
        public virtual float getTM_LOC_RESP_TARIF_GPRS_ROAMING()
        {
            return TM_LOC_RESP_TARIF_GPRS_ROAMING;
        }
        //    *
        //	 * @param tm_loc_resp_tarif_gprs_roaming the tM_LOC_RESP_TARIF_GPRS_ROAMING to set
        //	 
        public virtual void setTM_LOC_RESP_TARIF_GPRS_ROAMING(float tm_loc_resp_tarif_gprs_roaming)
        {
            TM_LOC_RESP_TARIF_GPRS_ROAMING = tm_loc_resp_tarif_gprs_roaming;
        }
        //    *
        //	 * @return the tMCONFIG_RESP_TARIF_GPRS
        //	 
        public virtual float getTMCONFIG_RESP_TARIF_GPRS()
        {
            return TMCONFIG_RESP_TARIF_GPRS;
        }
        //    *
        //	 * @param tmconfig_resp_tarif_gprs the tMCONFIG_RESP_TARIF_GPRS to set
        //	 
        public virtual void setTMCONFIG_RESP_TARIF_GPRS(float tmconfig_resp_tarif_gprs)
        {
            TMCONFIG_RESP_TARIF_GPRS = tmconfig_resp_tarif_gprs;
        }
        //    *
        //	 * @return the tMCONFIG_REQ_TARIF_GPRS
        //	 
        public virtual float getTMCONFIG_REQ_TARIF_GPRS()
        {
            return TMCONFIG_REQ_TARIF_GPRS;
        }
        //    *
        //	 * @param tmconfig_req_tarif_gprs the tMCONFIG_REQ_TARIF_GPRS to set
        //	 
        public virtual void setTMCONFIG_REQ_TARIF_GPRS(float tmconfig_req_tarif_gprs)
        {
            TMCONFIG_REQ_TARIF_GPRS = tmconfig_req_tarif_gprs;
        }
        //    *
        //	 * @return the tMCONFIG_RESP_TARIF_SMS
        //	 
        public virtual float getTMCONFIG_RESP_TARIF_SMS()
        {
            return TMCONFIG_RESP_TARIF_SMS;
        }
        //    *
        //	 * @param tmconfig_resp_tarif_sms the tMCONFIG_RESP_TARIF_SMS to set
        //	 
        public virtual void setTMCONFIG_RESP_TARIF_SMS(float tmconfig_resp_tarif_sms)
        {
            TMCONFIG_RESP_TARIF_SMS = tmconfig_resp_tarif_sms;
        }
        //    *
        //	 * @return the tMCONFIG_REQ_TARIF_SMS
        //	 
        public virtual float getTMCONFIG_REQ_TARIF_SMS()
        {
            return TMCONFIG_REQ_TARIF_SMS;
        }
        //    *
        //	 * @param tmconfig_req_tarif_sms the tMCONFIG_REQ_TARIF_SMS to set
        //	 
        public virtual void setTMCONFIG_REQ_TARIF_SMS(float tmconfig_req_tarif_sms)
        {
            TMCONFIG_REQ_TARIF_SMS = tmconfig_req_tarif_sms;
        }  
       
    }
}