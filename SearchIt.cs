/*******************************************************************************
This simple sample demonstrates how to search for POIs.

For a complete explanation of all objects and their functionality please refer
to the API documentation located in the \mq\clients\CSharp directory or
http://support.mapquest.com.

NOTE: These samples are designed to work with sample data sets, so pool names
ports and server IPs may need to be modified for your use.
*******************************************************************************/

using System;

namespace SearchIt
{
   using MQClientInterface;
   using MQServers;

	class SimpleSearch
	{
		// This is a constant used to designate Gas Stations.
		// Our sample database denotes gas stations as having an ID of 3111
		const int GAS_STATIONS = 3111;

		// The main entry point for the application.
		[STAThread]
		static void Main(string[] args)
		{
	 
			/*
			MQClientInterface.Exec is the MapQuest client object.
			all server requests, such as Geocode and Search, are part of the Exec object.
			*/
			MQClientInterface.Exec spatialClient = new MQClientInterface.Exec();
	         
			//Client.mqGeocodeServerName refers to the name of the server where the MapQuest server resides.
			//Client.mqGeocodeServerPath refers to the virtual directory where the MapQuest server resides.
			//Client.mqMapServerPort refers to the port the client uses to communicate with the MapQuest

			spatialClient.ServerName = MQServers.MQServerDef.mqSpatialServerName;
			spatialClient.ServerPath = MQServers.MQServerDef.mqSpatialServerPath;
			spatialClient.ServerPort = MQServers.MQServerDef.mqSpatialServerPort;
			spatialClient.ClientId   = MQServers.MQServerDef.mqSpatialServerClientId;
			spatialClient.Password   = MQServers.MQServerDef.mqSpatialServerPassword;

			/*
			Create a DTCollection object.  In the Search method, the DTCollection is intended
			to be used as the primary filter for retrieving objects whether retrieving them
			from the mapping data, a user defined feature collection or a database.
			*/
			MQClientInterface.DTCollection dtCollection = new MQClientInterface.DTCollection();

			// Add the ID for gas stations to the DTCollection filter.
			dtCollection.Add(GAS_STATIONS);

			/*
			A DBLayerQuery object uniquely specifies a database table to be used when drawing
			maps or performing searches. It contains the information necessary to connect to a
			table within an ODBC datasource. DBLayerQuery points to a DB layer that the
			MapQuest server has been configured to use.
			*/
			MQClientInterface.DBLayerQuery dbLayerQuery = new MQClientInterface.DBLayerQuery();

			// Name of the MapQuest server DBPool to use
			dbLayerQuery.DBLayerName = "MQA.test";

			/*
			Optional extra SQL query information? (used in conjunction with spatial search criteria).
			Allows user to add further SQL restrictions on the results that would have been
			returned. This SQL fragment will be appended to the WHERE clause with an "AND"
			operator. This is to be used in conjunction with the DTCollection, not (usually)
			in place of.  In this example, DT 3111, GAS_STATIONS signifies Gas Stations so this search
			will find the 20 closest gas stations, that accept American Express, within 5 miles of our lat/lng.
			Note:  If utilizing spatial IDs, the extraCriteria will be 'AND'ed with each spatial ID being searched
			for.  This allows for better indexing possibilities as you can generate spatialId_UserDefinedColumn1 index
			for quicker searching.
			*/
			dbLayerQuery.ExtraCriteria = "(Variable1 = '1')";

			/*
			DBLayerQueryCollection object is a collection of DBLayerQuery objects. Use
			DBLayerQueryCollection objects to create maps with user-supplied features taken
			from a number of different databases or tables
			*/
			MQClientInterface.DBLayerQueryCollection dbLayerQueryCollection = new MQClientInterface.DBLayerQueryCollection();

			// Add the query to the query collection.
			dbLayerQueryCollection.Add(dbLayerQuery);

			// A RadiusSearchCriteria object specifies the geographic search criteria for a radius search.
			MQClientInterface.RadiusSearchCriteria radiusSearchCriteria = new MQClientInterface.RadiusSearchCriteria();

			// Latitude/Longitude of center point for search
			radiusSearchCriteria.Center = new MQClientInterface.LatLng(40.4445, -79.995399);

			// This property specifies the maximum number of features MapQuest should return for the Search method
			radiusSearchCriteria.MaxMatches = 20;

			/*
			This property specifies the radius of the circular area to be searched. For example,
			if the radius is 5 miles, MapQuest searches for points within 5 miles of the point
			described by the Center.Latitude and Center.Longitude properties
			*/
			radiusSearchCriteria.Radius = 5;

			// Show all the parameters used in the radius search
			Console.WriteLine("\nRadius Search Parameters:\n");
			Console.WriteLine("{0,-17}{1,-10}   {2}","Parameter Name","Value","Description");
			Console.WriteLine("Center.Latitude  {0,-10}   Latitude of center point for search.", radiusSearchCriteria.Center.Latitude);
			Console.WriteLine("Center.Longitude {0,-10}   Longitude of center point for search.", radiusSearchCriteria.Center.Longitude);
			Console.WriteLine("MaxMatches       {0,-10}   Maximum number of features that will be returned.", radiusSearchCriteria.MaxMatches);
			Console.WriteLine("Radius           {0,-10}   The radius of the circular area to be searched.\n", radiusSearchCriteria.Radius);

			/*
			Generates a request to the MapQuest server (as specified by the server name, path, and
			port number) to perform a search and return the results in a FeatureCollection object.
			When performing a search against a database, the FeatureCollection will contain PointFeature
			Objects exclusively.  If you attempt to search the mapping data, the FeatureCollection may
			contain PointFeatures, LineFeatures, or PolygonFeatures.
			*/

			/*
			A FeatureCollection object holds a collection of feature objects, potentially
			containing a mixture of different feature types.
			This may be a user collection or results from a request such as search.
			*/
			MQClientInterface.FeatureCollection searchResults = new MQClientInterface.FeatureCollection();

			try
			{
				spatialClient.Search(radiusSearchCriteria, searchResults, null, dbLayerQueryCollection, null, dtCollection);
			}
			catch(System.Exception e)
			{
				Console.WriteLine("Search failed. {0}", e.Message);
				return;
			}

			// Display the results in a table
			Console.WriteLine("Radius Search Found {0} Matches", searchResults.Size);
			Console.WriteLine("{0}\t{1,-30}\t{2,-4}\t{3}","Match","Name","Key","Distance");
	      
			/*
			The POIs found get stored in the FeatureCollection named SearchResults. Loop
			through the FeatureCollection to display the information pertaining to the POIs
			found.  The key is tied to a unique ID in the database.  After a search is
			performed, the key is used to requery the database through an external mechanism
			(such as ADO) to retrieve extended information about the given record.
			*/

			int i;
			MQClientInterface.PointFeature pointFeature = new MQClientInterface.PointFeature();
			for (i = 0; i < searchResults.Size; i++)
			{
				pointFeature = (MQClientInterface.PointFeature)searchResults.GetAt(i);
				Console.WriteLine(" {0}\t{1,-30}\t{2,-4}\t{3}", i+1,  pointFeature.Name,
					pointFeature.Key, pointFeature.Distance);
			}

			// Delete the Features from previous search
			searchResults.RemoveAll();

			// A RectSearchCriteria object specifies the geographic search criteria for a rectangle search.
			MQClientInterface.RectSearchCriteria rectSearchCriteria = new MQClientInterface.RectSearchCriteria();

			// This property specifies the maximum number of features MapQuest should return for the Search method
			rectSearchCriteria.MaxMatches = 20;

			// Set Latitude/Longitude of the upper left corner of the rectangular area to be searched.
			rectSearchCriteria.UpperLeft = new MQClientInterface.LatLng(40.517069, -80.089632);

			// Set Latitude/Longitude of the lower right corner of the rectangular area to be searched.
			rectSearchCriteria.LowerRight = new MQClientInterface.LatLng(40.371931, -79.901166);

			// Show all the parameters used in the rectangular search
			Console.WriteLine("\n");
			Console.WriteLine("Rectangular Search Parameters:\n");
			Console.WriteLine("{0,-21}{1,-10}\t{2}","Parameter Name","Value","Description");
			Console.WriteLine("UpperLeft.Latitude   {0,-10}\tLatitude of Upper Left corner of search area.", rectSearchCriteria.UpperLeft.Latitude);
			Console.WriteLine("UpperLeft.Longitude  {0,-10}\tLongitude of Upper Left corner of search area.", rectSearchCriteria.UpperLeft.Longitude);
			Console.WriteLine("LowerRight.Latitude  {0,-10}\tLatitude of Lower Right corner of search area.", rectSearchCriteria.LowerRight.Latitude);
			Console.WriteLine("LowerRight.Longitude {0,-10}\tLongitude of Lower Right corner of search area.", rectSearchCriteria.LowerRight.Longitude);
			Console.WriteLine("MaxMatches           {0,-10}\tMaximum number of features that will be returned\n", rectSearchCriteria.MaxMatches);
	         
			/*
			Generate a request to the MapQuest server to perform a search and return the
			results in a FeatureCollection object.  When performing a search against a
			database, the FeatureCollection will contain PointFeature objects exclusively.
			If you attempt to search the mapping data, the FeatureCollection may
			contain PointFeatures, LineFeatures, or PolygonFeatures.
			*/
			try
			{
				spatialClient.Search(rectSearchCriteria, searchResults, null, 
					dbLayerQueryCollection, null, dtCollection);
			}
			catch(System.Exception e)
			{
				Console.WriteLine("Search failed. {0}", e.Message);
				return;
			}

			// Display the results in a table
			Console.WriteLine("Rectangular Search Found {0} Matches", searchResults.Size);
			Console.WriteLine("{0}\t{1,-30}\t{2}","Match","Name","Key");

			/*
			The POIs found get stored in the FeatureCollection named SearchResults. Loop
			through the FeatureCollection to display the information pertaining to the POIs
			found.  The key is tied to a unique ID in the database.  After a search is
			performed, the key is used to requery the database through an external mechanism
			(such as ADO) to retrieve extended information about the given record.
			*/

			/*
			Notice when performing a rectanglur search, distance is of no use.  This is
			because a retangular search does not contain a point of reference where as
			with the radius search a point of reference exists, the center point.
			*/
			for (i = 0; i < searchResults.Size; i++)
			{
				pointFeature = (MQClientInterface.PointFeature)searchResults.GetAt(i);
				Console.WriteLine(" {0}\t{1,-30}\t{2}", i+1, pointFeature.Name, 
					pointFeature.Key);
			}
		}
	}
};

