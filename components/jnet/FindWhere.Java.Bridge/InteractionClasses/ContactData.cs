using com.teleca.fleetonline.web.bean;
namespace com.teleca.fleetonline.repository
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL/src/java/ejb_module/src/com/teleca/fleetonline/repository/ContactData.java,v $
    // $Revision: 1.3 $
    // $Date: 2007/08/01 12:33:46 $
    //
    // Copyright(c) Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************
    [System.Serializable]
    public class ContactData : RepositoryData
    {

        //    *
        //	 * 
        //	 
        private const long serialVersionUID = 1L;

        public long SerialVersionUID
        {
            get { return serialVersionUID; }
        }

        private int recId = -1; // record id unique

        public int RecId
        {
            get { return recId; }
            set { recId = value; }
        }
        private string foId; // foId
        /// <summary>
        /// 
        /// </summary>
        public string FoId
        {
            get { return foId; }
            set { foId = value; }
        }
        private int indexNr; // follow number in contact list (0 - x)

        public int IndexNr
        {
            get { return indexNr; }
            set { indexNr = value; }
        }
        private string name = ""; // name of the contact
        private string phone = ""; // phone number of the contact

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private string email = ""; // email number of the contact
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private int status; // status indicator (requested / confirmed)

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private int provider = 0; //mobile-email operator

        public int Provider
        {
            get { return provider; }
            set { provider = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public ContactData()
        {
        }

        public ContactData(string foId)
        {
            this.foId = foId;
        }

        public ContactData(int recId, string foId)
        {
            this.recId = recId;
            this.foId = foId;
        }

        //    *
        //	 * @return Returns the email.
        //	 
        public virtual string getEmail()
        {
            return Email;
        }

        //    *
        //	 * @param email The email to set.
        //	 
        public virtual void setEmail(string email)
        {
            this.Email = email;
        }

        //    *
        //	 * @return Returns the fmId.
        //	 
        public virtual string getFoId()
        {
            return FoId;
        }

        //    *
        //	 * @param fmId The fmId to set.
        //	 
        public virtual void setFoId(string foId)
        {
            this.FoId = foId;
        }

        //    *
        //	 * @return Returns the indexNr.
        //	 
        public virtual int getIndexNr()
        {
            return IndexNr;
        }

        //    *
        //	 * @param indexNr The indexNr to set.
        //	 
        public virtual void setIndexNr(int indexNr)
        {
            this.IndexNr = indexNr;
        }

        //    *
        //	 * @return Returns the name.
        //	 
        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }

        //    *
        //	 * @param name The name to set.
        //	 

        //    *
        //	 * @return Returns the phone.
        //	 
        public virtual string getPhone()
        {
            return Phone;
        }

        //    *
        //	 * @param phone The phone to set.
        //	 
        public virtual void setPhone(string phone)
        {
            this.Phone = phone;
        }

        //    *
        //	 * @return Returns the recId.
        //	 
        public virtual int getRecId()
        {
            return RecId;
        }

        //    *
        //	 * @param recId The recId to set.
        //	 
        public virtual void setRecId(int recId)
        {
            this.RecId = recId;
        }

        //    *
        //	 * @return Returns the status.
        //	 
        public virtual int getStatus()
        {
            return Status;
        }

        //    *
        //	 * @param status The status to set.
        //	 
        public virtual void setStatus(int status)
        {
            this.Status = status;
        }

        //    *
        //	 * @return Returns the provider.
        //	 
        public virtual int getProvider()
        {
            return Provider;
        }

        //    *
        //	 * @param provider The provider to set.
        //	 
        public virtual void setProvider(int provider)
        {
            this.Provider = provider;
        }

    }
}