using com.teleca.fleetonline.web.bean;
using System;
namespace com.teleca.fleetonline.repository
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL5/ejb_module/src/com/teleca/fleetonline/repository/UserAccountData.java,v $
    // $Revision: 1.1 $
    // $Date: 2008/04/10 14:27:38 $
    //
    // Copyright (c) Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //************************************************************************* 


    // *
    // * This class holds User Account Data (balance, currency, account type and whether the user is blocked or not)
    // 
    [System.Serializable]
    public class UserAccountData : RepositoryData
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
        //private boolean uidSet = false;
        private bool blocked = false;

        public bool Blocked
        {
            get { return blocked; }
            set { blocked = value; }
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
        private int accountType = 0;

        public int AccountType
        {
            get { return accountType; }
            set { accountType = value; }
        }
        private DateTime creditLowNotifSent; // time when 'credit is low' notification has been sent

        public DateTime CreditLowNotifSent
        {
            get { return creditLowNotifSent; }
            set { creditLowNotifSent = value; }
        }
        private DateTime creditZeroNotifSent; // time when 'credit is zero' notification has been sent

        public DateTime CreditZeroNotifSent
        {
            get { return creditZeroNotifSent; }
            set { creditZeroNotifSent = value; }
        }
        private string fmId;

        public string FmId
        {
            get { return fmId; }
            set { fmId = value; }
        }
        private DateTime paymentDate; //time when the last payment done

        public DateTime PaymentDate
        {
            get { return paymentDate; }
            set { paymentDate = value; }
        }

        //    *
        //	 * Constructor
        //	 
        public UserAccountData()
        {
            //uidSet = false;
        }

        //    *
        //	 * Constructor that sets User ID
        //	 * @param foId ID of the User
        //	 
        public UserAccountData(string foId, string fmId)
        {
            this.foId = foId;
            this.fmId = fmId;
            //uidSet = true;
        }

        //    *
        //	 * Returns the balance.
        //	 * @return float
        //	 
        public virtual float getBalance()
        {
            return Balance;
        }

        //    *
        //	 * Returns the blocked.
        //	 * @return boolean
        //	 
        public virtual bool isBlocked()
        {
            return Blocked;
        }

        //    *
        //	 * Returns the currency.
        //	 * @return int
        //	 
        public virtual int getCurrency()
        {
            return Currency;
        }

        //    *
        //	 * Returns the foId.
        //	 * @return String
        //	 
        public virtual string getFoId()
        {
            return FoId;
        }

        //    *
        //	 * Sets the balance.
        //	 * @param balance The balance to set
        //	 
        public virtual void setBalance(float balance)
        {
            this.Balance = balance;
        }

        //    *
        //	 * Sets the blocked.
        //	 * @param blocked The blocked to set
        //	 
        public virtual void setBlocked(bool blocked)
        {
            this.Blocked = blocked;
        }

        //    *
        //	 * Sets the currency.
        //	 * @param currency The currency to set
        //	 
        public virtual void setCurrency(int currency)
        {
            this.Currency = currency;
        }

        //    *
        //	 * Returns the accountType.
        //	 * @return int
        //	 
        public virtual int getAccountType()
        {
            return AccountType;
        }

        //    *
        //	 * Sets the accountType.
        //	 * @param accountType The accountType to set
        //	 
        public virtual void setAccountType(int accountType)
        {
            this.AccountType = accountType;
        }

        //    *
        //	 * @return Returns the creditLowNotifSent.
        //	 
        public virtual DateTime getCreditLowNotifSent()
        {
            return CreditLowNotifSent;
        }

        //    *
        //	 * @param creditLowNotifSent The creditLowNotifSent to set.
        //	 
        public virtual void setCreditLowNotifSent(DateTime creditLowNotifSent)
        {
            this.CreditLowNotifSent = creditLowNotifSent;
        }

        //    *
        //	 * @return Returns the creditZeroNotifSent.
        //	 
        public virtual DateTime getCreditZeroNotifSent()
        {
            return CreditZeroNotifSent;
        }

        //    *
        //	 * @param creditZeroNotifSent The creditZeroNotifSent to set.
        //	 
        public virtual void setCreditZeroNotifSent(DateTime creditZeroNotifSent)
        {
            this.CreditZeroNotifSent = creditZeroNotifSent;
        }

        public virtual string getFmId()
        {
            return FmId;
        }

        public virtual DateTime getPaymentDate()
        {
            return PaymentDate;
        }

        public virtual void setPaymentDate(DateTime paymentDate)
        {
            this.PaymentDate = paymentDate;
        }


    }
}