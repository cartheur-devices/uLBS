/*******************************************************************************
This simple sample demonstrates how to search for POIs, display those POIs on a
map, and compare arc distance vs. route distance.

For a complete explanation of all objects and their functionality please refer
to the API documentation located in the \mq\clients\CSharp directory or
http://support.mapquest.com.

NOTE: These samples are designed to work with sample data sets, so pool names
ports and server IPs may need to be modified for your use.
*******************************************************************************/

using System;

namespace SearchAndDisplayItWithRouteDistance
{
   using MQClientInterface;
   using MQServers;

   class SimpleSearchAndDisplayWRD
   {
      // This is a constant used to designate Gas Stations.
      // Our sample database denotes gas stations as having an Id of 3111
      const int GAS_STATIONS = 3111;

      // Search radius
      const double SEARCH_RADIUS = 2.5;

      // The main entry point for the application.
      [STAThread]
      static void Main(string[] args)
      {
      
         /*
         MQClientInterface.Exec is the MapQuest client object.
         all server requests, such as Geocode and Search, are part of the Exec object.
         */
         MQClientInterface.Exec mapClient = new MQClientInterface.Exec();

         //Client.mqGeocodeServerName refers to the name of the server where the MapQuest server resides.
         //Client.mqGeocodeServerPath refers to the virtual directory where the MapQuest server resides.
         //Client.mqMapServerPort refers to the port the client uses to communicate with the MapQuest

         mapClient.ServerName = MQServers.MQServerDef.mqMapServerName;
         mapClient.ServerPath = MQServers.MQServerDef.mqMapServerPath;
         mapClient.ServerPort = MQServers.MQServerDef.mqMapServerPort;
         mapClient.ClientId   = MQServers.MQServerDef.mqMapServerClientId;
         mapClient.Password   = MQServers.MQServerDef.mqMapServerPassword;

         // Route client
         MQClientInterface.Exec routeClient = new MQClientInterface.Exec();
         routeClient.ServerName = MQServers.MQServerDef.mqRouteServerName;
         routeClient.ServerPath = MQServers.MQServerDef.mqRouteServerPath;
         routeClient.ServerPort = MQServers.MQServerDef.mqRouteServerPort;
         routeClient.ClientId   = MQServers.MQServerDef.mqRouteServerClientId;
         routeClient.Password   = MQServers.MQServerDef.mqRouteServerPassword;

         MQClientInterface.Exec spatialClient = new MQClientInterface.Exec();
         spatialClient.ServerName = MQServers.MQServerDef.mqSpatialServerName;
         spatialClient.ServerPath = MQServers.MQServerDef.mqSpatialServerPath;
         spatialClient.ServerPort = MQServers.MQServerDef.mqSpatialServerPort;
         spatialClient.ClientId   = MQServers.MQServerDef.mqSpatialServerClientId;
         spatialClient.Password   = MQServers.MQServerDef.mqSpatialServerPassword;

         // The MapState object contains the information necessary to display
         // the map, such as size, scale, and latitude/longitude coordinates for
         // centering the map
         MQClientInterface.MapState mapState = new MQClientInterface.MapState();

         // Define the width and height of the map in pixels.
         mapState.WidthPixels  = 420;
         mapState.HeightPixels = 262;

         // The MapScale property tells the server the scale at which to display
         // the map Level of detail displayed varies depending on the scale
         // of the map
         mapState.MapScale = 100000;

         // Specify the latitude/longitude coordinate to center the map.
         mapState.Center = new MQClientInterface.LatLng(40.4445, -79.995399);

         // The MapQuest Session object is composed of multiple objects,
         // such as the MapState and CoverageStyle.
         MQClientInterface.Session mqSession = new MQClientInterface.Session();

         // A DTStyle object is an object that contains graphical information
         // about a point to display on the map.  This information includes,
         // but is not limited to, the symbol for a point, whether to label
         // the point, and if so, the font to use
         MQClientInterface.DTStyle originDTStyle = new MQClientInterface.DTStyle();

         // For a PointFeature to use this DTStyle, the DT Property of the
         // PointFeature must equal the DT assigned to the DTStyle object.
         // Refer to the API documentation for valid DT ranges of
         // user-defined DTStyle objects.
         originDTStyle.DT = 3150;

         // The SymbolType property defines whether the symbol for display is
         // a GRF (Image type i.e. Bitmap) file defined as mqRaster = 0,
         // or a GMF (Vector data) file defined as mqVector = 1.
         // Utilities are provided with the distribution to create GRF or GMF
         // files. These symbols need to be stored on the MapQuest Server to
         // be used
         originDTStyle.SymbolType = MQClientInterface.SymbolType.VECTOR;

         // The SymbolName property specifies the name of the particular symbol
         // to display when a PointFeature designates using this DTStyle to
         // represent itself.  The symbol can be in GRF or GMF format.
         originDTStyle.SymbolName = "MQ00031";

         // This property determines whether or not the symbol should be
         // displayed with a text label.
         originDTStyle.LabelVisible = true;
         
         // This property determines whether or not a POI is to be displayed
         // on the map.  It may be useful to hold but not display a POI class
         // until particular time
         originDTStyle.Visible = true;

         // A DTStyle object is an object that contains graphical information about a
         // point to display on the map.  This information includes, but is not limited to,
         // the symbol for a point, whether to label the point, and if so, the font to use.
         MQClientInterface.DTStyle gasStationDTStyle = new MQClientInterface.DTStyle();

         // For a PointFeature to use this DTStyle, the DT Property of the
         // PointFeature must equal the DT assigned to the DTStyle object.
         // Refer to the API documentation for valid DT ranges of user-defined
         // DTStyle objects.
         gasStationDTStyle.DT = GAS_STATIONS;

         // The SymbolType property defines whether the symbol for display is
         // a GRF (Image type i.e. Bitmap) file defined as mqRaster = 0,
         // or a GMF (Vector data) file defined as mqVector 1.  Utilities
         // are provided with the distribution to create GRF or GMF files.
         // These symbols need to be stored on the MapQuest Server to be used
         gasStationDTStyle.SymbolType = MQClientInterface.SymbolType.RASTER;

         // The SymbolName property specifies the name of the particular symbol
         // to display when a PointFeature designates using this DTStyle to
         // represent itself.  The symbol can be in GRF or GMF format
         gasStationDTStyle.SymbolName = "MQ00245";

         // This property determines whether or not the symbol should be
         // displayed with a text label
         gasStationDTStyle.LabelVisible = false;

         // This property determines whether or not a POI is to be displayed
         // on the map.  It may be useful to hold but not display a POI class
         // until particular time
         gasStationDTStyle.Visible = true;

         // The CoverageStyle object contains user-defined DTStyle(Display Type)
         // objects, which can override default styles set in the style pool
         MQClientInterface.CoverageStyle coverageStyle = new MQClientInterface.CoverageStyle();

         // This adds a DTStyle object to the CoverageStyle object.  The server
         // will use this to determine how to display the origin point
         coverageStyle.Add(originDTStyle);

         // This adds a DTStyle object to the CoverageStyle object.  The server
         // will use this to determine how to display the gas station locations
         coverageStyle.Add(gasStationDTStyle);

         // A PointFeature object contains information about where to display a
         // POI on a map, as well information about the particular point, such as the
         // distance from the center in a radius search.
         MQClientInterface.PointFeature originPoint = new MQClientInterface.PointFeature();

         // This property must coincide with the DT of the DTStyle object we want to use
         // in determining the display characteristics of this PointFeature.
         originPoint.DT = 3150;

         // The CenterLatLng object contains the latitude and longitude
         // coordinate used to determine where to display the point on a map.
         // In this instance, we are displaying a point representing the center
         // of our search area
         originPoint.CenterLatLng = new MQClientInterface.LatLng(40.4445, -79.995399);

         // Create a DTCollection object. In the Search method, the DTCollection
         // is intended to be used as the primary filter for retrieving objects
         // whether retrieving them from the mapping data, a user defined
         // feature collection or a database
         MQClientInterface.DTCollection dtCollection = new MQClientInterface.DTCollection();

         // Add the ID for gas stations to the DTCollection filter.
         dtCollection.Add(GAS_STATIONS);

         // A DBLayerQuery object uniquely specifies a database table to be used
         // when drawing maps or performing searches. It contains the
         // information necessary to connect to a table within an ODBC
         // datasource. DBLayerQuery points to a DB layer that the MapQuest
         // server has been configured to use
         MQClientInterface.DBLayerQuery dbLayerQuery = new MQClientInterface.DBLayerQuery();

         // Name of the MapQuest server DBPool to use
         dbLayerQuery.DBLayerName = "MQA.test";

         // DBLayerQueryCollection object is a collection of DBLayerQuery
         // objects. Use DBLayerQueryCollection objects to create maps with
         // user-supplied features taken from a number of different databases or
         // tables
         MQClientInterface.DBLayerQueryCollection dbLayerQueryCollection = new MQClientInterface.DBLayerQueryCollection();

         // Add the query to the query collection.
         dbLayerQueryCollection.Add(dbLayerQuery);

         // A RadiusSearchCriteria object specifies the geographic search criteria for a radius search.
         MQClientInterface.RadiusSearchCriteria radiusSearchCriteria = new MQClientInterface.RadiusSearchCriteria();

         // Latitude/Longitude of center point for search
         radiusSearchCriteria.Center = new MQClientInterface.LatLng(40.4445, -79.995399);

         // This property specifies the maximum number of features MapQuest should return for the Search method
         radiusSearchCriteria.MaxMatches = 15;

         // This property specifies the radius of the circular area to be searched. For example,
         // if the radius is 5 miles, MapQuest searches for points within 5 miles of the point
         // described by the Center.Latitude and Center.Longitude properties
         radiusSearchCriteria.Radius = SEARCH_RADIUS;

         // A FeatureCollection object holds a collection of feature objects, potentially
         // containing a mixture of different feature types.
         // This may be a user collection or results from a request such as search.
         MQClientInterface.FeatureCollection searchResults = new MQClientInterface.FeatureCollection();

         // Generates a request to the MapQuest server (as specified by the server name, path, and
         // port number) to perform a search and return the results in a FeatureCollection object.
         // When performing a search against a database, the FeatureCollection will contain PointFeature
         // Objects exclusively.  If you attempt to search the mapping data, the FeatureCollection may
         // contain PointFeatures, LineFeatures, or PolygonFeatures.
         spatialClient.Search(radiusSearchCriteria, searchResults, null, dbLayerQueryCollection, null, dtCollection);

         // Create route matrix location collection and add origin
         MQClientInterface.GeoAddress location = new MQClientInterface.GeoAddress();
         location.LatLng = new MQClientInterface.LatLng(40.4445, -79.995399);;
         MQClientInterface.LocationCollection locations = new MQClientInterface.LocationCollection();
         locations.Add(location);

         // Print label
         Console.WriteLine("\nGas stations within {0} miles of origin. Max matches = 25",
            SEARCH_RADIUS);

         // Display arc distance results in a table
         // Also, populate locations to send to route matrix
         Console.WriteLine("\n>> Arc Distance Results ===================================");
         Console.WriteLine(">> Found {0} Matches", searchResults.Size);
         Console.WriteLine("-------+-------------------------------+----------+--------");
         Console.WriteLine("Match   Name                            Key        Distance");
         Console.WriteLine("-------+-------------------------------+----------+--------");
         MQClientInterface.PointFeature pointFeature;
         for (int i = 0, n = searchResults.Size; i < n; i++)
         {
            pointFeature = (PointFeature)searchResults.GetAt(i);
            Console.WriteLine(" {0}\t{1,-30}\t{2,-11}{3}", 
               i+1, pointFeature.Name, pointFeature.Key, 
               pointFeature.Distance);

            location = new MQClientInterface.GeoAddress();
            location.LatLng = pointFeature.CenterLatLng;
            locations.Add(location);
         }

         // The arc distance features
         MQClientInterface.FeatureCollection adFeatures = new MQClientInterface.FeatureCollection();
         adFeatures.Append(searchResults);

         // Add the Origin to the FeatureCollection that also contains the returned POIs
         // from the Search method.  Then, pass the FeatureCollection back to the server
         // so the map that is returned to the end user will contain the points.
         adFeatures.Add(originPoint);

         // Add objects to the session to be sent to the server.
         mqSession.AddOne(mapState);
         mqSession.AddOne(coverageStyle);
         mqSession.AddOne(adFeatures);

         // This call generates the actual GIF image resulting from the given
         // Session Object.
         sbyte[] mapImage2;
         try
         {
            mapImage2 = mapClient.GetMapImageDirect(mqSession);
         }
         catch (System.Exception e)
         {
            Console.WriteLine("Failed to get map image. {0}", e.Message);
            return;
         }
         Console.WriteLine("writing adMapImage.gif....");

         const System.String filename = "adMapImage.gif";
         System.IO.FileStream fs = new System.IO.FileStream(filename, 
            System.IO.FileMode.Create);

         // Create the writer for data and write to the file
         System.IO.BinaryWriter w = new System.IO.BinaryWriter(fs);
         for (int i = 0; i < mapImage2.GetLength(0); i++)
            w.Write(mapImage2[i]);

         w.Close();
         fs.Close();

         // Calculate one to all route matrix
         MQClientInterface.RouteMatrixResults routeMatrixResults = new MQClientInterface.RouteMatrixResults();
         MQClientInterface.RouteOptions       routeOptions = new MQClientInterface.RouteOptions();
         routeClient.DoRouteMatrix(locations, false, routeOptions, routeMatrixResults);

         // Only keep points within search radius
         double distance;
         MQClientInterface.FeatureCollection rdFeatures = new MQClientInterface.FeatureCollection();
         for (int j = 0, m = searchResults.Size; j < m; j++)
         {
            pointFeature = (MQClientInterface.PointFeature)searchResults.GetAt(j);
            distance = routeMatrixResults.GetDistance(0, j+1);

            if (distance <= SEARCH_RADIUS)
            {
               pointFeature.Distance = distance;
               rdFeatures.Add(pointFeature);
            }
         }

         // Display route distance results in a table
         Console.WriteLine("\n>> Route Distance Results =================================");
         Console.WriteLine(">> Found {0} Matches\n", rdFeatures.Size);
         Console.WriteLine("-------+-------------------------------+----------+--------");
         Console.WriteLine("Match   Name                            Key        Distance");
         Console.WriteLine("-------+-------------------------------+----------+--------");
         for (int k = 0, p = rdFeatures.Size; k < p; k++)
         {
            pointFeature = (MQClientInterface.PointFeature)rdFeatures.GetAt(k);
            Console.WriteLine(" {0}\t{1,-30}\t{2,-11}{3}", 
               k+1, pointFeature.Name, pointFeature.Key, pointFeature.Distance);
         }

         // Add the Origin to the FeatureCollection that also contains the returned POIs
         // from the Search method.  Then, pass the FeatureCollection back to the server
         // so the map that is returned to the end user will contain the points.
         rdFeatures.Add(originPoint);

         // Add objects to the session to be sent to the server.
         MQClientInterface.Session mqSession2 = new MQClientInterface.Session();
         mqSession2.AddOne(mapState);
         mqSession2.AddOne(coverageStyle);
         mqSession2.AddOne(rdFeatures);

         // This call generates the actual GIF image resulting from the given Session Object.
         sbyte[] rdMapImage;
         try
         {
            rdMapImage = mapClient.GetMapImageDirect(mqSession2);
         }
         catch (System.Exception e)
         {
            Console.WriteLine("Failed to get map image. {0}", e.Message);
            return;
         }
         Console.WriteLine("writing rdMapImage.gif....");

         const System.String filename2 = "rdMapImage.gif";
         fs = new System.IO.FileStream(filename2, System.IO.FileMode.Create);

         // Create the writer for data and write to the file
         w = new System.IO.BinaryWriter(fs);
         for (int i = 0; i < rdMapImage.GetLength(0); i++)
            w.Write(rdMapImage[i]);

         w.Close();
         fs.Close();
      }
   }
};