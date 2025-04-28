using com.teleca.fleetonline.web.bean;
namespace com.teleca.fleetonline.repository
{

    public class MemberBalanceData : RepositoryData
    {
        private const long serialVersionUID = 1L;

        public long SerialVersionUID
        {
            get { return serialVersionUID; }
        } 


        private string foId;

        public string FoId
        {
            get { return foId; }
            set { foId = value; }
        }
        private string fmId;

        public string FmId
        {
            get { return fmId; }
            set { fmId = value; }
        }
        private float balance = 0;

        public float Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        private int currency = 0;

        public int Currency
        {
            get { return currency; }
            set { currency = value; }
        }
        private string alias;

        public string Alias
        {
            get { return alias; }
            set { alias = value; }
        }


        public MemberBalanceData(string foId, string fmId, float balance, int currency, string alias)
            : base()
        {
            this.foId = foId;
            this.fmId = fmId;
            this.balance = balance;
            this.currency = currency;
            this.alias = alias;
        }

        public MemberBalanceData()
        {
        }

        //    *
        //	 * @return the alias
        //	 
        public virtual string getAlias()
        {
            return Alias;
        }

        //    *
        //	 * @param alias the alias to set
        //	 
        public virtual void setAlias(string alias)
        {
            this.Alias = alias;
        }

        //    *
        //	 * @return the balance
        //	 
        public virtual float getBalance()
        {
            return Balance;
        }

        //    *
        //	 * @param balance the balance to set
        //	 
        public virtual void setBalance(float balance)
        {
            this.Balance = balance;
        }

        //    *
        //	 * @return the currency
        //	 
        public virtual int getCurrency()
        {
            return Currency;
        }

        //    *
        //	 * @param currency the currency to set
        //	 
        public virtual void setCurrency(int currency)
        {
            this.Currency = currency;
        }

        //    *
        //	 * @return the fmId
        //	 
        public virtual string getFmId()
        {
            return FmId;
        }

        //    *
        //	 * @param fmId the fmId to set
        //	 
        public virtual void setFmId(string fmId)
        {
            this.FmId = fmId;
        }

        //    *
        //	 * @return the foId
        //	 
        public virtual string getFoId()
        {
            return FoId;
        }

        //    *
        //	 * @param foId the foId to set
        //	 
        public virtual void setFoId(string foId)
        {
            this.FoId = foId;
        }


    }
}