/*
   Read / write settings to config (xml) file. Does not use ConfigurationSettings.AppSettings since it's not supported on 
  .NET Compact Framework and does not support writing settings. 
	
  Uses same schema as app.config file. Example:

      <configuration>
          <appSettings>
              <add key="Name" value="Live Oak" />
              <add key="LogEvents" value="True" />
          </appSettings>
      </configuration>	
	
  Default settings file name is the same as app.config, 
  appends .config to the end of the assembly name. Example:
	
      <appname.exe>.config.
*/

using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Text;

namespace MobileShared
{
    /// <summary>
    /// Read / write app.config settings file.
    /// </summary>
    public class Settings
    {
        #region internal fields
        Hashtable m_list = new Hashtable();
        string m_filePath = string.Empty;
        bool m_autoWrite = true;
        #endregion

        #region properties
        /// <summary>
        /// Specifies if the settings file is updated whenever a value 
        /// is set. If false, you need to call Write to update the 
        /// underlying settings file.
        /// </summary>
        public bool AutoWrite
        {
            get { return m_autoWrite; }
            set { m_autoWrite = value; }
        }

        /// <summary>
        /// Full path to settings file.
        /// </summary>
        public string FilePath
        {
            get { return m_filePath; }
            set { m_filePath = value; }
        }
        #endregion

        /// <summary>
        /// Default constructor. 
        /// </summary>
        public Settings()
        {
            // get full path to file
            m_filePath = GetFilePath();

            // populate list with settings from file
            Read();
        }

        #region public methods
        /// <summary>
        /// Set setting value. Update underlying file if AutoUpdate is true.
        /// </summary>
        public void SetValue(string key, object value)
        {
            // update internal list
            m_list[key] = value;

            // update settings file
            if (m_autoWrite)
                Write();
        }

        /// <summary>
        /// Return specified settings as string.
        /// </summary>
        public string GetString(string key)
        {
            object result = m_list[key];
            return (result == null) ? String.Empty : result.ToString();
        }

        /// <summary>
        /// Return specified settings as integer.
        /// </summary>
        public int GetInt(string key)
        {
            string result = GetString(key);
            return (result == String.Empty) ? 0 : Convert.ToInt32(result);
        }

        /// <summary>
        /// Return specified settings as boolean.
        /// </summary>
        public bool GetBool(string key)
        {
            string result = GetString(key);
            return (result == String.Empty) ? false : Convert.ToBoolean(result);
        }

        /// <summary>
        /// Read settings file.
        /// </summary>
        public void Read()
        {
            try
            {
                // first remove all items from list
                m_list.Clear();

                // populate list with items from file

                // open settings file
                XmlTextReader reader = new XmlTextReader(m_filePath);

                // go through file and read the xml file and 
                // populate internal list with 'add' elements
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "add"))
                        m_list[reader.GetAttribute("key")] = reader.GetAttribute("value");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Write settings to file.
        /// </summary>
        public void Write()
        {
            try
            {
                // header elements
                StreamWriter writer = File.CreateText(m_filePath);
                writer.WriteLine("<configuration>");
                writer.WriteLine("\t<appSettings>");

                // go through internal list and create 'add' element for each item
                IDictionaryEnumerator enumerator = m_list.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    writer.WriteLine("\t\t<add key=\"{0}\" value=\"{1}\" />",
                        enumerator.Key.ToString(),
                        enumerator.Value);
                }

                // footer elements
                writer.WriteLine("\t</appSettings>");
                writer.WriteLine("</configuration>");

                writer.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void RestoreDefaults()
        {
            try
            {
                // easiest way is to delete the file and
                // repopulate internal list with defaults
                File.Delete(FilePath);
                Read();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// Return full path to settings file. Appends .config to the assembly name.
        /// </summary>
        public static string GetFilePath()
        {
            return Assembly.GetExecutingAssembly().GetName().CodeBase + ".config";
        }
    }

    /// <summary>
    /// Encrypt / decrypt string using base64.
    /// </summary>
    public class SimpleEncrypt
    {
        private SimpleEncrypt()
        {
        }

        /// <summary>
        /// Return base64 version of string.
        /// </summary>
        public static string Encrypt(string text)
        {
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(text);
                return Convert.ToBase64String(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return String.Empty;
            }
        }

        /// <summary>
        /// Return string version of base64 string.
        /// </summary>
        public static string Decrypt(string text)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(text);
                return Encoding.ASCII.GetString(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return String.Empty;
            }
        }
    }
}
