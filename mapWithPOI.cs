/*******************************************************************************
Simple sample to demonstrate how to generate a maps with a POI using server
and client side sessions.

For a complete explanation of all objects and their functionality please refer
to the API documentation located in the \mq\clients\CSharp directory or
http://support.mapquest.com.

NOTE: These samples are designed to work with sample data sets, so pool names
ports and server IPs may need to be modified for your use.
*******************************************************************************/

using System;

namespace MapWithPOI
{
   using MQClientInterface;
   using MQServers;

	class SimpleMapWithPOI
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

			// The MapState object contains the information necessary to display the map,
			// such as size, scale, and latitude/longitude coordinates for centering the map.
			MQClientInterface.MapState mapState = new MQClientInterface.MapState();

			// Define the width and height of the map in pixels.
			mapState.WidthPixels  = 392;
			mapState.HeightPixels = 245;

			// The MapScale property tells the server the scale at which to display the map.
			// Level of detail displayed varies depending on the scale of the map.
			mapState.MapScale = 12000;

			// Specify the latitude/longitude coordinate to center the map.
			mapState.Center = new MQClientInterface.LatLng(40.44569, -79.890393);

			// The featureCollection object contains Feature objects.  In this example,
			// a PointFeature is added to the collection.
			MQClientInterface.FeatureCollection featureCollection = new MQClientInterface.FeatureCollection();

			// A PointFeature object contains information about where to display a
			// POI (Point of Interest) on a map, as well information about the point, such as the
			// distance from the center in a radius search.
			MQClientInterface.PointFeature pointFeature = new MQClientInterface.PointFeature();

			// A DTStyle object is an object that contains graphical information about a
			// point to display on the map.  This information includes, but is not limited to,
			// the symbol for a point, whether to label the point, and if so, the font to use.
			MQClientInterface.DTStyle pointDTStyle = new MQClientInterface.DTStyle();

			// For a PointFeature to use this DTStyle, the DT Property of the
			// PointFeature must equal the DT assigned to the DTStyle object.  Refer to the
			// API documentation for valid DT ranges of user-defined DTStyle objects.
			pointDTStyle.DT = 3072;

			// The SymbolType property defines whether the symbol for display is
			// a GRF (Image type i.e. Bitmap) file defined as mqRaster, or a GMF
			// (Vector data) file defined as mqVector = 1.
			// Utilities are provided with the distribution to create GRF or GMF files.
			// These symbols need to be stored on the MapQuest Server to be used.
			pointDTStyle.SymbolType = MQClientInterface.SymbolType.VECTOR;

			// The SymbolName property specifies the name of the particular symbol to display
			// when a PointFeature designates using this DTStyle to represent itself.
			// The symbol can be in GRF or GMF format.
			pointDTStyle.SymbolName = "MQ00031";

			// This property determines whether or not the symbol should be displayed with a text label.
			pointDTStyle.LabelVisible = true;

			// This property determines whether or not a POI is to be displayed  on the map.
			// It may be useful to hold but not display a POI class
			// until particular time.
			pointDTStyle.Visible = true;

			// The FontStyle property defines the style of the font to use when displaying the
			// label. Refer to the API documentation for valid FontStyle values.
			// Multiple font styles may be used to form different types of label styles.
			// This example produces a boxed font.
			pointDTStyle.FontStyle = MQClientInterface.FontStyle.BOXED;

			// When setting the FontStyle to boxed, a background color must also be defined.
			// The box will not display unless the background color is defined.
			pointDTStyle.FontBoxBkgdColor = MQClientInterface.ColorStyle.BLUE;

			// The coverageStyle object contains user-defined DTStyle (Display Type) objects, which can
			// override default styles set in the style pool.
			MQClientInterface.CoverageStyle coverageStyle = new MQClientInterface.CoverageStyle();

			// This adds a DTStyle object to the coverageStyle object.
			coverageStyle.Add(pointDTStyle);

			// This property must coincide with the DT of the DTStyle object used
			// in determining the display characteristics of this PointFeature.
			pointFeature.DT = 3072;

			// When a DTStyle object's LabelVisible property is set to true, the Name property
			// is displayed as the label.
			pointFeature.Name = "Hello";

			// The CenterLatLng object contains the latitude/longitude coordinate
			// used to determine where to display the point on a map.
			// In this example, a point is displayed at the center of the map.
			pointFeature.CenterLatLng = mapState.Center;

			// This example adds a PointFeature to a featureCollection.  The Features in the
			// collection will be added to the map that is returned to the end user.
			featureCollection.Add(pointFeature);

			// The MapQuest Session object is composed of multiple objects,
			// such as the MapState and CoverageStyle.
			MQClientInterface.Session mqSession = new MQClientInterface.Session();

			// Add objects to the session.
			mqSession.AddOne(mapState);
			mqSession.AddOne(featureCollection);
			mqSession.AddOne(coverageStyle);

			// Process 1: Server side session

			// Create a new MapQuest session on the server.  This call the MapQuest server creates
			// and stores the MapQuest Session object on the MapQuest server.

			System.String sessionId;
			try
			{
				sessionId = mapClient.CreateSessionEx(mqSession);
			}
			catch (System.Exception e)
			{
				Console.WriteLine("Failed to create session. {0}", e.Message);
				return;
			}

			// This call generates the actual GIF image resulting from the given Session Identifier.
			// It generates a GIF image based on the server information set earlier.
			sbyte[] mapImage1;
			try
			{
				mapImage1 = mapClient.GetMapImageFromSession(sessionId);
			}
			catch (System.Exception e)
			{
				Console.WriteLine("Failed to get map image. {0}", e.Message);
				return;
			}

			Console.WriteLine("writing mapImage1.gif....");

			const System.String filename = "mapImage1.gif";
			System.IO.FileStream fs = new System.IO.FileStream(filename, 
				System.IO.FileMode.Create);

			// Create the writer for data and write to the file
			System.IO.BinaryWriter w = new System.IO.BinaryWriter(fs);
			for (int i = 0; i < mapImage1.GetLength(0); i++)
				w.Write(mapImage1[i]);

			w.Close();
			fs.Close();

			// Process 2: Client side session or sessionless

			// This call generates the actual GIF image resulting from the given Session Object.

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
			Console.WriteLine("writing mapImage2.gif....");

			const System.String filename2 = "mapImage2.gif";
			fs = new System.IO.FileStream(filename2, System.IO.FileMode.Create);

			// Create the writer for data and write to the file
			w = new System.IO.BinaryWriter(fs);
			for (int i = 0; i < mapImage2.GetLength(0); i++)
				w.Write(mapImage2[i]);

			w.Close();
			fs.Close();
			return;
		}
	}
};
