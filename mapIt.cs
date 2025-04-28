/******************************************************************************
Simple sample to demonstrate how to generate a maps using server and client
side sessions.

For a complete explanation of all objects and their functionality please refer
to the API documentation located in the \mq\clients\CSharp directory or
http://support.mapquest.com.

NOTE: These samples are designed to work with sample data sets, so pool names
ports and server IPs may need to be modified for your use.
******************************************************************************/

using System;

namespace MapIt
{
   using MQClientInterface;
   using MQServers;

	class SimpleMap
	{
		// The main entry point for the application.
		[STAThread]
		static void Main(string[] args)
		{
			// MQClientInterface.Exec is the MapQuest client object.
			MQClientInterface.Exec mapClient = new MQClientInterface.Exec();
			
			// All server requests, such as Geocode and Search, are part of the Exec object.
			// Client.mqMapServerName refers to the name of the server where the MapQuest server resides.
			// Client.mqMapServerPath refers to the virtual directory where the MapQuest server resides.
			// Client.mqMapServerPort refers to the port the client uses to communicate with the MapQuest
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
			mapState.MapScale = 48000;

			// Specify the latitude/longitude coordinate to center the map.
			mapState.Center = new MQClientInterface.LatLng(40.44569, -79.890393);

			// The MapQuest Session object is composed of multiple objects,
			// such as the MapState and CoverageStyle.
			MQClientInterface.Session mqSession = new MQClientInterface.Session();

			// Add objects to the session.
			mqSession.AddOne(mapState);

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
				Console.WriteLine("Failed to get map image1. {0}", e.Message);
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
			// Session Object. 

			sbyte[] mapImage2;
			try
			{
				mapImage2 = mapClient.GetMapImageDirect(mqSession);
			}
			catch (System.Exception e)
			{
				Console.WriteLine("Failed to get map image2. {0}", e.Message);
				return;
			}
			Console.WriteLine("writing mapImage2.gif....");

			const System.String filename2 = "mapImage2.gif";
			fs = new System.IO.FileStream(filename2, 
				System.IO.FileMode.Create);

			// Create the writer for data and write to the file
			w = new System.IO.BinaryWriter(fs);
			for (int i = 0; i < mapImage2.GetLength(0); i++)
				w.Write(mapImage2[i]);

			w.Close();
			fs.Close();
			return;
		}
	}
}
