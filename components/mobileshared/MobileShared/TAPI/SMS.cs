using System;
using System.Text;

using OpenNETCF.Phone.Sms;

namespace MobileShared.TAPI
{
    public static class SMS
    {
        public static bool SendSMS(string Text, string RecipientNumber)
        {
            bool success = false;

            try
            { 
                Sms NewSMS = new Sms();

                NewSMS.SendMessage(new OpenNETCF.Phone.Sms.SmsAddress(RecipientNumber, OpenNETCF.Phone.AddressType.International), Text);

                success = true;
            }
            catch(Exception ex)
            {
                string s = ex.Message;
                System.Windows.Forms.MessageBox.Show(":" + ex.Message + " RecipientNumber = " + RecipientNumber);
            }

            return success;
        }
    }
}
