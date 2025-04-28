//*************************************************************************
//
// $Archive: /gms/product/fol/versions/head/src/java/web_module/src/com/teleca/fleetonline/web/bean/TimewindowDisplayHelper.java $
// $Revision: 1.1 $
// $Date: 2008/04/10 14:28:23 $
// $Author: salih.canoz $
//
//*************************************************************************

//
// * @(#)FILENAME
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
namespace com.teleca.fleetonline.web.bean
{

    //using TimewindowsForm = com.teleca.fleetonline.web.form.TimewindowsForm;

    ///
    // * Helper deals with outputting the timewindows in the correct format
    // * <PRE>
    // * // format: name, start time, finish time, weekdays, weekends, foId
    // * positioningData[0]=new Array("Roger", 0, 12, true, false,"11");
    // * positioningData[1]=new Array("Jim", 4, 15, true, false,"12");
    // * positioningData[2]=new Array("Mum", 12, 24, false, true,"13");
    // * </PRE>
    // * 
    // * @author $Author: salih.canoz $
    // * @version $Revision: 1.1 $, $Date: 2008/04/10 14:28:23 $
    // 
    [System.Serializable]
    public class TimewindowDisplayHelper
    {

        //internal TimewindowsForm windows = null;
        private string message = "";

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        private int operatorId = 9999;

        public int OperatorId
        {
            get { return operatorId; }
            set { operatorId = value; }
        }

        public TimewindowDisplayHelper()
        {
        }

        //    *
        //	 * Construct a helper to output time windows
        //	 * @param windows the data to use for outputting
        //	 
        //public TimewindowDisplayHelper(TimewindowsForm windows)
        //{
        //    this.windows = windows;
        //}
        public virtual void setMessage(string message)
        {
            this.Message = message;
        }
        public virtual string getMessage()
        {
            return this.Message;
        }

        //    *
        //	 * @return Returns the operatorId.
        //	 
        public virtual int getOperatorId()
        {
            return OperatorId;
        }

        //    *
        //	 * @param operatorId The operatorId to set.
        //	 
        public virtual void setOperatorId(int operatorId)
        {
            this.OperatorId = operatorId;
        }
    }
}