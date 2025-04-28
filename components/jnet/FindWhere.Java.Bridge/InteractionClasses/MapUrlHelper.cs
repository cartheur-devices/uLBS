
namespace com.teleca.fleetonline.web.bean
{
    /// <summary>
    /// Applys the map to the url.
    /// </summary>
    [System.Serializable]
    public class MapUrlHelper
    {
        private string url;

        public MapUrlHelper()  { this.url = ""; }

        public MapUrlHelper(string url) { this.url = url;  }

        public virtual string getUrl()  { return url;  }
    }
}