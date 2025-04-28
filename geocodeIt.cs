/*******************************************************************************
Simple sample to demonstrate how to geocode a location.

For a complete explanation of all objects and their functionality please refer
to the API documentation located in the \mq\clients\CSharp directory or
http://support.mapquest.com.

NOTE: These samples are designed to work with sample data sets, so pool names
ports and server IPs may need to be modified for your use.
*******************************************************************************/

using System;

namespace GeocodeIt
{
	using MQClientInterface;
	using MQServers;

	class SimpleGeocode
	{
		// The main entry point for the application.
		[STAThread]
		static void Main(string[] args)
		{
			/*
			MQClientInterface.Exec is the MapQuest client object.
			All server requests, such as Geocode and Search, are part of the Exec object.
			*/
			MQClientInterface.Exec geocodeClient = new MQClientInterface.Exec();

			//Client.mqGeocodeServerName refers to the name of the server where the MapQuest server resides.
			//Client.mqGeocodeServerPath refers to the virtual directory where the MapQuest server resides.
			//Client.mqMapServerPort refers to the port the client uses to communicate with the MapQuest
			geocodeClient.ServerName = MQServers.MQServerDef.mqGeocodeServerName;
			geocodeClient.ServerPath = MQServers.MQServerDef.mqGeocodeServerPath;
			geocodeClient.ServerPort = MQServers.MQServerDef.mqGeocodeServerPort;
			geocodeClient.ClientId   = MQServers.MQServerDef.mqGeocodeServerClientId;
			geocodeClient.Password   = MQServers.MQServerDef.mqGeocodeServerPassword;

			/*Create an Address object to contain the location to be geocoded.*/
			MQClientInterface.Address address = new MQClientInterface.Address();

			/*
			The GeocodeResults collection will contain the results of the geocode. A collection
			is used so that multiple potential matches or ambiguities can be returned when an
			exact match cannot be found.
			*/
			MQClientInterface.LocationCollection geocodeResults = new MQClientInterface.LocationCollection();

			/*
			Remove the address number (100 from 100 Penn St) to see multiple matches or
			ambiguities returned.
			*/
			address.Init();
			address.Street     = "100 Penn St";
			address.City       = "Pittsburgh";
			address.State      = "Pa";
			address.PostalCode = "15215";
			address.Country    = "US";

			try
			{
				/*This is the first communication with the MapQuest server.*/
				geocodeClient.Geocode(address, geocodeResults);
			}
			catch (System.Exception e)
			{
				Console.WriteLine("Failed to geocode origin. {0}", e.Message);
				return;
			}

			if (geocodeResults.Size == 0)
			{
				Console.WriteLine("ERROR - The address entered could not be geocoded");
			}
			/*Location geocoded, so display the match(es).*/
			else
			{
				// Location geocoded, so display the match(es).
				Console.WriteLine("MapQuest found {0} match(es) for the address:",geocodeResults.Size);
				Console.WriteLine("{0}", address.Street);
				Console.WriteLine("{0}, {1}", address.City, address.State);
				Console.WriteLine("{0}\n", address.PostalCode);

				// Output all possible matches returned by the geocoder.
				// Each object in the collection is a potential match.
				// The LocationCollection (GeocodeResults) contains GeoAddress objects.
				// If an exact match is found, the collection will contain only one object.
				for (int i = 0, iCount = geocodeResults.Size; i < iCount; i++)
				{
					MQClientInterface.GeoAddress geoAddress = (MQClientInterface.GeoAddress)geocodeResults.GetAt(i);
					Console.WriteLine("Match # {0}", i+1);
					Console.WriteLine("\t{0}", geoAddress.Street);
					Console.WriteLine("\t{0}", geoAddress.City);
					Console.WriteLine("\t{0}", geoAddress.County);
					Console.WriteLine("\t{0}", geoAddress.State);
					Console.WriteLine("\t{0}", geoAddress.Country);
					Console.WriteLine("\t{0}", geoAddress.PostalCode);
					Console.WriteLine("\t{0}, {1}", geoAddress.LatLng.Latitude,geoAddress.LatLng.Longitude);
					Console.WriteLine("\t{0}\n", geoAddress.ResultCode);
				}
			}
		}
	}
};

