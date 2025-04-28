/******************************************************************************
This simple sample demonstrates how to route between two locations without
utilizing MapQuest's server side session management.  To utilize MapQuest's
server side session management, please view the routeItWithServerSession sample.

For a complete explanation of all objects and their functionality please refer
to the API documentation located in the \mq\clients\CSharp directory or
http://support.mapquest.com.

NOTE: These samples are designed to work with sample data sets, so pool names
ports and server IPs may need to be modified for your use.
******************************************************************************/

using System;

namespace RouteNoServerSession
{
   using MQClientInterface;
   using MQServers;

   class RouteIt
   {
      // The main entry point for the application.
      [STAThread]
      static void Main(string[] args)
      {

         /*
         MQClientInterface.Exec is the MapQuest client object.
         All server requests, such as Geocode and Search, are part of the Exec object.
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

         // Geocode client
         MQClientInterface.Exec geocodeClient = new MQClientInterface.Exec();
         geocodeClient.ServerName = MQServers.MQServerDef.mqGeocodeServerName;
         geocodeClient.ServerPath = MQServers.MQServerDef.mqGeocodeServerPath;
         geocodeClient.ServerPort = MQServers.MQServerDef.mqGeocodeServerPort;
         geocodeClient.ClientId   = MQServers.MQServerDef.mqGeocodeServerClientId;
         geocodeClient.Password   = MQServers.MQServerDef.mqGeocodeServerPassword;

         // Route client
         MQClientInterface.Exec routeClient = new MQClientInterface.Exec();
         routeClient.ServerName = MQServers.MQServerDef.mqRouteServerName;
         routeClient.ServerPath = MQServers.MQServerDef.mqRouteServerPath;
         routeClient.ServerPort = MQServers.MQServerDef.mqRouteServerPort;
         routeClient.ClientId   = MQServers.MQServerDef.mqRouteServerClientId;
         routeClient.Password   = MQServers.MQServerDef.mqRouteServerPassword;

         // Create origin address
         MQClientInterface.Address address = new MQClientInterface.Address();
         address.Init();
         address.Street     = "100 Penn St";
         address.City       = "Pittsburgh";
         address.State      = "PA";
         address.PostalCode = "";
         address.Country    = "US";

         // Create LocationCollection for results
         MQClientInterface.LocationCollection originResults = new MQClientInterface.LocationCollection();
            
         // Geocode origin location
         try
         {
            geocodeClient.Geocode(address, originResults);
         }
         catch (System.Exception e)
         {
            Console.WriteLine("Failed to geocode origin. {0}", e.Message);
            return;
         }

         // If the results collection's Count property is zero, no matches could be found for the location
         if (originResults.Size == 0)
         {
            Console.WriteLine("Failed to geocode origin");
            return;
         }

         // Create destination address
         address.Init();
         address.Street     = "2015 Saw Mill Run Blvd";
         address.City       = "Pittsburgh";
         address.State      = "PA";
         address.PostalCode = "";
         address.Country    = "US";

         // Create LocationCollection for results
         MQClientInterface.LocationCollection destResults = new MQClientInterface.LocationCollection();
            
         // Geocode destination location
         try
         {
            geocodeClient.Geocode(address, destResults);
         }
         catch (System.Exception e)
         {
            Console.WriteLine("Failed to geocode destination. {0}", e.Message);
            return;
         }

         // If the results collection's Count property is zero, no matches could be found for the location
         if (destResults.Size == 0)
         {
            Console.WriteLine("Failed to geocode destination");
            return;
         }

         // This is the collection that will hold the geocoded locations to be utilized in the call to DoRoute.
         MQClientInterface.LocationCollection routeLocations = new MQClientInterface.LocationCollection();

         MQClientInterface.GeoAddress origAddr = (MQClientInterface.GeoAddress)originResults.GetAt(0);
         MQClientInterface.GeoAddress destAddr = (MQClientInterface.GeoAddress)destResults.GetAt(0);
         routeLocations.Add(origAddr);
         routeLocations.Add(destAddr);

         // Routes may be customized (i.e., shortest or fastest route, whether to avoid
         // toll roads or limited access highways, etc.).  In this sample the number of
         // shape points per maneuver is being set, which provides the detail of the
         // highlight to be drawn. In addition, the CoverageName is also being set, which
         // must match a configured route on the MapQuest server.

         // The RouteOptions object contains information pertaining to the Route to be performed.
         MQClientInterface.RouteOptions routeOptions = new MQClientInterface.RouteOptions();
         routeOptions.MaxShapePointsPerManeuver = 50;

         // The RouteResults object will contain the results of the DoRoute call.  The
         // results contains information such as the narrative, drive time and distance.
         MQClientInterface.RouteResults routeResults = new MQClientInterface.RouteResults();

         // This call to the server actually generates the route.
         try
         {
            routeClient.DoRoute(routeLocations, routeOptions, routeResults, "");
         }
         catch (System.Exception e)
         {
            Console.WriteLine("Route failed. {0}", e.Message);
            return;
         }

         // To see a demonstration of the error handling comment out the call to DoRoute
         if (routeResults.ResultCode != MQClientInterface.RouteResultsCode.SUCCESS)
         {
            for (int i = 0; i < routeResults.ResultMessages.Size; i++)
               Console.WriteLine("\n{}", routeResults.ResultMessages.GetAt(i));
         }

         // Create DisplayTypes and pointFeatures for the origin and
         // destination locations to be displayed.  For details regarding
         // DisplayTypes and PointFeatures, see the mapWithPoi sample.
         MQClientInterface.DTStyle originDTStyle = new MQClientInterface.DTStyle();
         originDTStyle.DT           = 3073;
         originDTStyle.SymbolType   = MQClientInterface.SymbolType.RASTER;
         originDTStyle.SymbolName   = "MQ09191";
         originDTStyle.Visible      = true;
         originDTStyle.LabelVisible = false;

         MQClientInterface.DTStyle destDTStyle = new MQClientInterface.DTStyle();
         destDTStyle.DT           = 3074;
         destDTStyle.SymbolType   = MQClientInterface.SymbolType.RASTER;
         destDTStyle.SymbolName   = "MQ09192";
         destDTStyle.Visible      = true;
         destDTStyle.LabelVisible = false;

         MQClientInterface.CoverageStyle coverageStyle = new MQClientInterface.CoverageStyle();
         coverageStyle.Add(originDTStyle);
         coverageStyle.Add(destDTStyle);

         MQClientInterface.PointFeature ptfOrigin = new MQClientInterface.PointFeature();
         ptfOrigin.DT = 3073;
         ptfOrigin.CenterLatLng = origAddr.LatLng;

         MQClientInterface.PointFeature ptfDest = new MQClientInterface.PointFeature();
         ptfDest.DT = 3074;
         ptfDest.CenterLatLng = destAddr.LatLng;

         MQClientInterface.FeatureCollection fcRouteNodes = new MQClientInterface.FeatureCollection();
         fcRouteNodes.Add(ptfOrigin);
         fcRouteNodes.Add(ptfDest);
         // End origin and destination point generation

         // Create the MapState object
         MQClientInterface.MapState mapState = new MQClientInterface.MapState();
         mapState.WidthPixels  = 450;
         mapState.HeightPixels = 300;

         // Create the Session object.
         MQClientInterface.Session mqSession = new MQClientInterface.Session();
         mqSession.AddOne(mapState);
         mqSession.AddOne(fcRouteNodes);
         mqSession.AddOne(coverageStyle);

         // Create the routeHighlight to be displayed on the map using the LinePrimitive
         // object.  This object allows you to insert lines of your own creation into the map.
         MQClientInterface.LinePrimitive lpRtHlt = new MQClientInterface.LinePrimitive();
         lpRtHlt.Color          = MQClientInterface.ColorStyle.GREEN;
         lpRtHlt.Key            = "RouteShape";
         lpRtHlt.Style          = MQClientInterface.PenStyle.SOLID;
         lpRtHlt.CoordinateType = MQClientInterface.CoordinateType.GEOGRAPHIC;
         lpRtHlt.DrawTrigger    = MQClientInterface.DrawTrigger.AFTER_ROUTE_HIGHLIGHT;
         lpRtHlt.Width          = 200;

         // The Generalize method reduces the amount of shape points used to represent a line.
         // This example combines all shape points within .01 miles of each other to 1 single
         // shape point.  This removes unnecessary shape points and shortens the URL to minimize
         // the possibility of exceeding some browsers' URL size limitations.  There may still
         // be cases where URL length is an issue, in which case you will need to utilize your
         // own server side storage.
         lpRtHlt.LatLngs = routeResults.ShapePoints;
         lpRtHlt.LatLngs.Generalize(0.01);

         // Add the line primitive to a primitiveCollection
         MQClientInterface.PrimitiveCollection pcMap = new MQClientInterface.PrimitiveCollection();
         pcMap.Add(lpRtHlt);
         mqSession.AddOne(pcMap);

         // The best fit object is used to draw a map at an appropriate scale determined
         // by the features you have added to the session, along with the optional primitives.
         // In this case we want the map to be displayed at a scale that includes the origin
         // and destination locations (pointFeatures) as well as the routeHighlight(linePrimitive).
         // The scaleAdjustmentFactor is then used to increase the scale by this factor based
         // upon the best fit that was performed.  This results in a border around the edge of
         // the map that does not include any features so the map appears clearer.
         MQClientInterface.BestFit bfMap = new MQClientInterface.BestFit();
         bfMap.ScaleAdjustmentFactor = 1.2;
         bfMap.IncludePrimitives     = true;
         mqSession.AddOne(bfMap);

         // This call generates the actual GIF image resulting from the given Session Object.
         sbyte[] mapImage;
         try
         {
            mapImage = mapClient.GetMapImageDirect(mqSession);
         }
         catch (System.Exception e)
         {
            Console.WriteLine("Failed to get map image. {0}", e.Message);
            return;
         }
         Console.WriteLine("writing mapImage.gif....");

         const System.String filename = "mapImage.gif";
         System.IO.FileStream fs = new System.IO.FileStream(filename, 
            System.IO.FileMode.Create);

         // Create the writer for data and write to the file
         System.IO.BinaryWriter w = new System.IO.BinaryWriter(fs);
         for (int i = 0; i < mapImage.GetLength(0); i++)
            w.Write(mapImage[i]);

         w.Close();
         fs.Close();

         // Generate the route narrative.
         Console.WriteLine("\nRoute Narrative:\n");
         Console.WriteLine("{0}\t{1,-50}\t{2,-8}  {3}\n", "Man #", "Narrative","Distance", "Time");

         // Each manuever object contains its own narrative, distance and drive time.
         int mc;
         for (mc = 0; mc < routeResults.TrekRoutes.GetAt(0).Maneuvers.Size; mc++)
         {
            MQClientInterface.Maneuver m = routeResults.TrekRoutes.GetAt(0).Maneuvers.GetAt(mc);
            Console.WriteLine(" {0}\t{1,-50}\t{2,-8}  {3}", mc+1, m.Narrative, 
               m.Distance, (m.Time / 60.0));
         }
         return;
      }
   }
}
