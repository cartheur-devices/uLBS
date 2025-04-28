using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;

namespace PositionSenderMobile.Configuration
{
    public class AppConfiguration
    {
        private string _webServiceHostName = string.Empty;
        private int _portNumber = 8081;
        private string _GoogleAPIKey = "";

        public string GoogleMapAPIKey
        {
            get
            {
                return _GoogleAPIKey;
            }
            set
            {
                _GoogleAPIKey = value;
            }
        }

        public string WebServiceHostName
        {
            get
            {
                return _webServiceHostName;
            }
            set
            {
                _webServiceHostName = value;
            }
        }
        public int PortNumber
        {
            get
            {
                return _portNumber;
            }
            set
            {
                _portNumber = value;
            }
        }

        public static AppConfiguration ApplicationConfiguration()
        {
            AppConfiguration cfg = null;
            string rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            rootPath += Path.DirectorySeparatorChar + "AppConfig.xml";

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AppConfiguration));
                StreamReader reader = new StreamReader(rootPath);
                cfg = (AppConfiguration)serializer.Deserialize(reader);
                reader.Close();
            }
            catch
            {
                cfg = new AppConfiguration();
                cfg.PortNumber = 8081;
                cfg.WebServiceHostName = "";
                cfg.GoogleMapAPIKey = "";
            }
            return cfg;
        }

        public void Save()
        {
            string rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            rootPath += Path.DirectorySeparatorChar + "AppConfig.xml";

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AppConfiguration));
                StreamWriter writer = new StreamWriter(rootPath);
                serializer.Serialize(writer, this);
                writer.Close();
            }
            catch
            {
            }
        }
    }
}
