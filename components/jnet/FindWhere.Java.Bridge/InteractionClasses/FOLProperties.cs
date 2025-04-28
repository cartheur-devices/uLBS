using System;
using System.Collections;
using System.Diagnostics;

namespace com.teleca.fleetonline.utils
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL/src/java/common_module/src/com/teleca/fleetonline/utils/FOLProperties.java,v $
    // $Revision: 1.6 $
    // $Date: 2008/01/03 13:17:25 $
    //
    // Copyright(c) Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //************************************************************************* 


    //using InitialContext = javax.naming.InitialContext;
    //using NamingException = javax.naming.NamingException;
    //using DataSource = javax.sql.DataSource;


    ///
    // *
    // * Singleton class to read the properties from the database. 
    // * Use the getInstance() method to receive an instance from this class.
    // 
    [System.Serializable]
    public class FOLProperties
    {
        //private Connection conn = null;
        private bool connected = false;
        private string connectionstring = null;

        //private static final String repositoryConfigPath = "/config/repository.properties";
        private string user = null;

        public string User
        {
            get { return user; }
            set { user = value; }
        }
        private string password = null;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string dbDriver = null;

        public string DbDriver
        {
            get { return dbDriver; }
            set { dbDriver = value; }
        }
        private string dbDatasource = null;

        public string DbDatasource
        {
            get { return dbDatasource; }
            set { dbDatasource = value; }
        }
        private string timeZone = null;

        public string TimeZone
        {
            get { return timeZone; }
            set { timeZone = value; }
        }

        internal bool debugLog = true;
        //private static Logging logger = Logging.getInstance();

        // static instance 
        private static FOLProperties folProperties = new FOLProperties();

        // private hashtable to store the properties 
        public Hashtable properties;



        //public FOLProperties()
        //{
        //    ReadProperties();
        //    readFromDatabase();
        //}


        //private void readFromDatabase()
        //{
        //    // read from database 
        //    try
        //    {
        //        Connect();
        //        ArrayList data = getProperties();
        //        int size = data.Count;

        //        // put the values into the hashtable 
        //        properties = new Hashtable(size * 2);
        //        for (int t = 0; t < size; t++)
        //        {
        //            ArrayList record = (ArrayList)data[t];
        //            string key = (string)record[0];
        //            string @value = (string)record[1];
        //            //                
        //            //				if (key != null) {
        //            //					key = key.toLowerCase();
        //            //				} 
        //            //				
        //            if (@value == null)
        //            {
        //                @value = "";
        //            }
        //            properties[key] = @value;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logger.logMajorError("FOLProperties", "readFromDatabase()", "Exception reading Data-Base::" + e, Logging.Component.UTILITIES);
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            Disconnect();
        //        }
        //        catch (Exception e)
        //        {
        //            logger.logMajorError("FOLProperties", "readFromDatabase()", "Exception closing database connection, exc=" + e, Logging.Component.UTILITIES);
        //        }
        //    }
        //}

        // Static method to get the single instance 
        public static FOLProperties getInstance()
        {
            return folProperties;
        }

        //public virtual Hashtable  getPropertiesOfType(string type)
        //{
        //    Hashtable props = new Hashtable();
        //    string key = "";
        //    string @value = "";
        //    //JAVA TO VB & C# CONVERTER TODO TASK: There is no .NET Dictionary equivalent to the Java 'keySet' method:
        //    for (Iterator it = properties.keySet().iterator(); it.hasNext(); )
        //    {
        //        key = (string)it.next();
        //        if (key.StartsWith(type + "."))
        //        {
        //            @value = (string)properties[key];
        //            props.put(key.Substring(type.Length + 1), @value);
        //        }
        //    }
        //    return props;
        //}

        // returns the value for the given property key 
        public virtual string getProperty(string key)
        {
            
            string result = null;
            if (key != null)
            {
                result = (string)properties[key];
                if (result == null)
                {
                    
                    result = (string)properties[key.ToLower()];
                }
            }
            Debug.WriteLine(String.Concat("Folproperties get property_1 key: ", key, " value: ", result));
            return result;
        }

        public virtual string getProperty(string key, string defaultValue)
        {


            if (properties == null || getProperty(key) == null)
            {
                Debug.WriteLine(String.Concat("Folproperties get defaultproperty_2 key: ", key, "value: ", defaultValue));
                return defaultValue;
            }

            Debug.WriteLine(String.Concat("Folproperties get property_2 key: ", key, "value: ", getProperty(key)));
            return getProperty(key);
        }

        ////JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        ////ORIGINAL LINE: public int getPropertyAsInt(String key) throws PropertyParseException
        //public virtual int getPropertyAsInt(string key)
        //{
        //    string @value = getProperty(key);

        //    try
        //    {
        //        return Convert.ToInt32(@value);
        //    }
        //    catch (NumberFormatException nfe)
        //    {
        //        throw new PropertyParseException(key + " not a number");
        //    }
        //}

        public virtual bool getPropertyAsBoolean(string key)
        {
            Debug.WriteLine(String.Concat("Folproperties get property_4 key: ", key));
            string @value = getProperty(key);
            try
            {
                return Boolean.Parse(@value);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //public virtual void Disconnect()
        //{
        //    try
        //    {
        //        conn.Close();
        //    }
        //    catch (SQLException sqle)
        //    {
        //        logger.logMajorError("DBConnector", "Disconnect()", "SQLException=" + sqle.getMessage(), Logging.Component.REPOSITORY_PROVIDER);
        //        //throw new DatabaseException("Problem when closing connection to DB.", sqle);
        //    }

        //    this.connected = false;
        //    if (debugLog)
        //        logger.logInfo("DBConnector", "Disconnect()", "Disonnected from DB:", Logging.Component.REPOSITORY_PROVIDER);
        //}

        //JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void Connect() throws Exception
        //public virtual void Connect()
        //{

        //    // check if already connected
        //    if (this.connected == true)
        //    {
        //        logger.logWarning("DBConnector", "Connect()", "Repository already initialised.", Logging.Component.REPOSITORY_PROVIDER);
        //        //throw new RepositoryException("Repository already initialised.");
        //        return;
        //    }

        //    logger.logInfo("DBConnector", "Connect()", "Try to connect to db", Logging.Component.REPOSITORY_PROVIDER);

        //    try
        //    {
        //        // Try to get a pooled connection
        //        conn = getPooledConnection();
        //        this.connected = true;
        //    }
        //    catch (Exception e)
        //    {
        //        logger.logMinorError("DBConnector", "Connect()", "Error connecting to db using pooled datasource: " + dbDatasource, Logging.Component.REPOSITORY_PROVIDER);

        //        logger.logWarning("DBConnector", "Connect()", "Fallback to non pooled jdbc connection: " + connectionstring, Logging.Component.REPOSITORY_PROVIDER);
        //        try
        //        {
        //            // Try a normal non-pooled connection
        //            conn = getConnection();
        //            this.connected = true;
        //        }
        //        catch (Exception ne)
        //        {
        //            throw new Exception("Unable to get any connection to database", ne);
        //        }
        //    }
        //    if (debugLog)
        //    {
        //        logger.logInfo("DBConnector", "Connect()", "Connected to DB.", Logging.Component.REPOSITORY_PROVIDER);
        //    }
        //}

        //    *
        //	 * gets a pooled database connection from the datasource
        //	 * @return
        //	 * @throws DatabaseException
        //	 
        //JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public Connection getPooledConnection() throws Exception
        //public virtual Connection getPooledConnection()
        //{
        //    try
        //    {
        //        InitialContext context = new InitialContext();
        //        DataSource jdbcURL = (DataSource)context.lookup(dbDatasource);
        //        return jdbcURL.getConnection();
        //    }
        //    catch (NamingException e)
        //    {
        //        throw new Exception("Error looking up DataSource=" + dbDatasource, e);
        //    }
        //    catch (SQLException se)
        //    {
        //        throw new Exception("Error connecting to DataSource=" + dbDatasource, se);
        //    }
        //}

        //    *
        //	 * Get a direct non-pooled connection to the database
        //	 * @param sysDB
        //	 * @return
        //	 * @throws DatabaseException
        //	 
        //public virtual Connection getConnection()
        //{
        //    Connection connection = null;
        //    try
        //    {
        //        // Load the JDBC driver
        //        string driverName = dbDriver;
        //        logger.logInfo("DBConnector.Connect() - db driver=" + driverName, Logging.Component.REPOSITORY_PROVIDER);
        //        Class.forName(driverName);
        //        connection = DriverManager.getConnection(connectionstring, user, password);
        //    }
        //    catch (SQLException sqle)
        //    {
        //        logger.logMajorError("DBConnector", "Connect()", "DB Connection problem. SQLExcpetion=" + sqle.getMessage(), Logging.Component.REPOSITORY_PROVIDER);
        //        //throw new DatabaseException("DB Connection problem.", sqle);
        //    }
        //    catch (ClassNotFoundException cnfe)
        //    {
        //        logger.logMajorError("DBConnector", "Connect()", "Problem with database driver class. ClassNotFoundException=" + cnfe.getMessage(), Logging.Component.REPOSITORY_PROVIDER);
        //        //throw new DatabaseException("Problem with database driver class.=" + dbDriver, cnfe);
        //    }
        //    return connection;
        //}

        //    *
        //	  * Retrieves all properties from the FOL_PROPERTIES table.
        //	  * 
        //	  * @return Vectore All (name,value) pairs from the table
        //	  * @throws DatabaseException
        //	  
        //public virtual ArrayList getProperties()
        //{
        //    if (debugLog)
        //        logger.logInfo("FOLProperties", "getProperties", "", Logging.Component.REPOSITORY_PROVIDER);

        //    ArrayList result = new ArrayList();

        //    string query = "select * from fol_properties";
        //    ResultSet rs = null;
        //    CallableStatement stmt = null;
        //    try
        //    {
        //        stmt = conn.prepareCall(query);
        //        rs = stmt.executeQuery();

        //        while (rs.next())
        //        {
        //            string key = rs.getString(1);
        //            string @value = rs.getString(2);
        //            ArrayList record = new ArrayList();
        //            record.Add(key);
        //            record.Add(@value);
        //            result.Add(record);
        //        }
        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        logger.logMajorError("FOLProperties", "getProperties", "Problem accessing DB. SQLException=" + e.getMessage(), Logging.Component.REPOSITORY_PROVIDER);
        //    }
        //    finally
        //    {
        //        closeSQL(rs, stmt);
        //    }
        //    return null;
        //}

        //        *
        //		 * Closes database objects passed in
        //		 * @param rs
        //		 * @param statement
        //		 
        //private void closeSQL(ResultSet rs, Statement statement)
        //{
        //    if (rs != null)
        //    {
        //        try
        //        {
        //            rs.Close();
        //            rs = null;
        //        }
        //        catch (Exception e)
        //        {
        //            logger.logMinorError("FOLProperties", "closeSQL", "Problem closing resultset. SQLException=" + e.getMessage(), Logging.Component.REPOSITORY_PROVIDER);

        //        }
        //    }
        //    if (statement != null)
        //    {
        //        try
        //        {
        //            statement.Close();
        //            statement = null;
        //        }
        //        catch (Exception e)
        //        {
        //            logger.logMinorError("FOLProperties", "closeSQL", "Problem closing statement. SQLException=" + e.getMessage(), Logging.Component.REPOSITORY_PROVIDER);

        //        }
        //    }
        //}

        //        *
        //		 * This function reads from the repoistory.properties file
        //		 * @throws RepositoryException
        //		 * @throws DatabaseException
        //		 	
        //public virtual void ReadProperties()
        //{
        //    try
        //    {
        //        Properties p = getProperties(GlobalConstants.APPLICATION_PROPERTIES_PATH);
        //        string instance = p.getProperty("instanceid");
        //        string testorlive = p.getProperty("testorlive");

        //        connectionstring = p.getProperty(testorlive + ".SystemDB.ConnectionString");
        //        user = p.getProperty(instance + ".SystemDB.User");
        //        password = p.getProperty(instance + ".SystemDB.Pass");
        //        dbDriver = p.getProperty("SystemDB.Driver");
        //        dbDatasource = p.getProperty(instance + ".SystemDB.Datasource");

        //        timeZone = p.getProperty(instance + ".timezone");
        //        if ((timeZone == null) || (timeZone.Length < 1))
        //        {
        //            //throw new RepositoryPropertiesException("timezone not defined in repository.properties");
        //        }
        //    }
        //    catch (Exception rpe)
        //    {
        //        logger.logMajorError("FOLProperties", "ReadProperties()", "RepositoryPropertiesException=" + rpe.getMessage(), Logging.Component.REPOSITORY_PROVIDER);
        //    }

        //}

        //        *
        //	     * This function loads a property file and return a Properties object
        //	     * @param path Path of property file
        //	     * @throws RepositoryPropertiesException
        //	     * @return Properties object
        //	     	
        //private Properties getProperties(string path)
        //{
        //    Properties p = new Properties();
        //    InputStream @is = null;
        //    try
        //    {
        //        @is = this.getClass().getResourceAsStream(path);
        //        p.load(@is);
        //    }
        //    catch (Exception ioe)
        //    {
        //        logger.logMajorError("FOLProperties", "getProperties", "Problem when loading properties " + path + ". Exception=" + ioe.getMessage(), Logging.Component.REPOSITORY_PROVIDER);
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            @is.Close();
        //        }
        //        catch (Exception e)
        //        {
        //        }
        //    }
        //    return p;
        //}
    }
}