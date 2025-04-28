/*******************************************************************************
Simple sample to demonstrate how to generate a maps with drawing primitivies
included using server and client side sessions.

For a complete explanation of all objects and their functionality please refer
to the API documentation located in the \mq\clients\CSharp directory or
http://support.mapquest.com.

NOTE: These samples are designed to work with sample data sets, so pool names
ports and server IPs may need to be modified for your use.
*******************************************************************************/

using System;

namespace MapItWithPrimitives
{
   using MQClientInterface;
   using MQServers;

   class SimpleMapWithPrimitives
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
			mapState.WidthPixels  = 550;
			mapState.HeightPixels = 350;

			// The MapScale property tells the server the scale at which to display the map.
			// Level of detail displayed varies depending on the scale of the map.
			mapState.MapScale = 12000;

			// Specify the latitude/longitude coordinate to center the map.
			mapState.Center = new MQClientInterface.LatLng(40.44569, -79.890393);

			// Create the collection to hold all the individual primitives.
			MQClientInterface.PrimitiveCollection primitives = new MQClientInterface.PrimitiveCollection();

			// Create one of each kind of primitive and save them to the server
			// so they get drawn on top of the next map.

			MQClientInterface.EllipsePrimitive ellipsePrimitive = new MQClientInterface.EllipsePrimitive();
			ellipsePrimitive.Color = MQClientInterface.ColorStyle.GREEN;
			ellipsePrimitive.CoordinateType = MQClientInterface.CoordinateType.GEOGRAPHIC;
			ellipsePrimitive.Width = 70;
			ellipsePrimitive.UpperLeftPoint  = new MQClientInterface.Point(20, 20);
			ellipsePrimitive.LowerRightPoint = new MQClientInterface.Point(150, 150);
			primitives.Add(ellipsePrimitive);

			MQClientInterface.LinePrimitive linePrimitive = new MQClientInterface.LinePrimitive();
			linePrimitive.Color = MQClientInterface.ColorStyle.ORANGE;
			linePrimitive.Width = 140;
			linePrimitive.Points.Add(new MQClientInterface.Point(30, 30));
			linePrimitive.Points.Add(new MQClientInterface.Point(100, 100));
			linePrimitive.Points.Add(new MQClientInterface.Point(100, 150));
			linePrimitive.Points.Add(new MQClientInterface.Point(150, 150));
			primitives.Add(linePrimitive);

			MQClientInterface.RectanglePrimitive rectanglePrimitive = new MQClientInterface.RectanglePrimitive();
			rectanglePrimitive.Color = MQClientInterface.ColorStyle.BLACK;
			rectanglePrimitive.CoordinateType = MQClientInterface.CoordinateType.GEOGRAPHIC;
			rectanglePrimitive.Width = 70;
			rectanglePrimitive.FillStyle = MQClientInterface.FillStyle.FDIAGONAL;
			rectanglePrimitive.UpperLeftPoint = new MQClientInterface.Point(250, 20);
			rectanglePrimitive.LowerRightPoint = new MQClientInterface.Point(400, 200);
			primitives.Add(rectanglePrimitive);

			MQClientInterface.TextPrimitive textPrimitive = new MQClientInterface.TextPrimitive();
			textPrimitive.Color = MQClientInterface.ColorStyle.RED;
			textPrimitive.CoordinateType = MQClientInterface.CoordinateType.DISPLAY;
			textPrimitive.BkgdColor = MQClientInterface.ColorStyle.YELLOW;
			textPrimitive.BoxOutlineColor = MQClientInterface.ColorStyle.BLUE;
			textPrimitive.Style = MQClientInterface.FontStyle.BOXED;
			textPrimitive.FontName = "Helvetica";           // Unix Server
			//textPrimitive.FontName = "Arial";				// Windows Server
			textPrimitive.FontSize = 18;
			textPrimitive.UpperLeftPoint = new MQClientInterface.Point(250,250);
			textPrimitive.Width = 50;
			textPrimitive.Text = "Sample Text";
			primitives.Add(textPrimitive);

			MQClientInterface.PolygonPrimitive polygonPrimitive = new MQClientInterface.PolygonPrimitive();
			polygonPrimitive.Color = MQClientInterface.ColorStyle.RED;
			polygonPrimitive.CoordinateType = MQClientInterface.CoordinateType.DISPLAY;
			polygonPrimitive.FillColor = MQClientInterface.ColorStyle.BLUE;
			polygonPrimitive.FillStyle = MQClientInterface.FillStyle.SOLID;
			polygonPrimitive.Width = 42;
			polygonPrimitive.Points.Add(new MQClientInterface.Point(50, 200));
			polygonPrimitive.Points.Add(new MQClientInterface.Point(50, 275));
			polygonPrimitive.Points.Add(new MQClientInterface.Point(150, 275));
			polygonPrimitive.Points.Add(new MQClientInterface.Point(150, 200));
			polygonPrimitive.Points.Add(new MQClientInterface.Point(100, 240));
			primitives.Add(polygonPrimitive);

			MQClientInterface.SymbolPrimitive symbolPrimitive = new MQClientInterface.SymbolPrimitive();
			symbolPrimitive.CoordinateType = MQClientInterface.CoordinateType.GEOGRAPHIC;
			symbolPrimitive.CenterPoint = new MQClientInterface.Point(450, 250);
			symbolPrimitive.SymbolName = "MQ00033";
			symbolPrimitive.SymbolType= MQClientInterface.SymbolType.VECTOR;
			primitives.Add(symbolPrimitive);

			// The MapQuest Session object is composed of multiple objects,
			// such as the MapState and CoverageStyle.
			MQClientInterface.Session mqSession = new MQClientInterface.Session();

			// Add objects to the session.
			mqSession.AddOne(mapState);
			mqSession.AddOne(primitives);

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

			// This call generates the actual GIF image resulting from the given
			//   Session Object.
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