using System.Web;
namespace com.teleca.fleetonline.web.bean
{

    [System.Serializable]
    public class BalanceHelper
    {

        private string dateTime;

        public string DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }
        private string quantity;

        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        private string charge;

        public string Charge
        {
            get { return charge; }
            set { charge = HttpUtility.UrlDecode(value); }
        }
        private string cost;

        public string Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        private string total;

        public string Total
        {
            get { return total; }
            set { total = value; }
        }

    }
}