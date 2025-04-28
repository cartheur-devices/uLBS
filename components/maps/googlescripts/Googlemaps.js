var usrMap = 'googlemaps';
var map = null;
var GeoCentLat, GeoCentLng; // Used for storing geofence
var geocoder = null;
 
function loadGoogleMaps() {
	if (GBrowserIsCompatible()) {
		map = new GMap2(document.getElementById('map'));
		map.addControl(new GLargeMapControl());
		map.addControl(new GMapTypeControl());
		map.addControl(new GScaleControl());
		map.enableScrollWheelZoom();
		map.setCenter(new GLatLng(0, 0));
		window.onunload = GUnload;

		GEvent.addListener(map, "click", function(overlay, latlng) {
			if (geofencePopupOpen) {
				map.clearOverlays();				
				SetNewGeoFence(latlng);
			}
		});
		 geocoder = new GClientGeocoder();
	}
	else {
	    alert(Javascript_google_NotCompatible);
	}
}

function GetLocations(data, compliant) {
	map.clearOverlays();
	var bounds = new GLatLngBounds();

	var baseIcon = new GIcon();
	baseIcon.iconSize = new GSize(11, 11);
	baseIcon.iconAnchor = new GPoint(9, 34);
	baseIcon.infoWindowAnchor = new GPoint(9, 2);
	baseIcon.infoShadowAnchor = new GPoint(18, 25);

	var Spots = new SpotList();
	Spots.ReadFromArray(data);
	var NumberOfSpots = Spots.Spots.length;
	var marker;

	var circleAdded = false;
	
	//Create cell icons
	if (NumberOfSpots > 0) {
	    for (i = 0; i < NumberOfSpots; i++) {
	        var spotData = Spots.Spots[i];
	        var point = new GLatLng(spotData.Y, spotData.X)

	        //Icon size
	        var size = 1;
	        if (spotData.IconId.toString().length == 3) {
	            range = spotData.IconId.toString().charAt(0) + spotData.IconId.toString().charAt(1);
	            size = spotData.IconId.toString().charAt(2);
	        }

	        if (spotData.IconId.toString().length == 2) {
	            range = spotData.IconId.toString().charAt(0);
	            size = spotData.IconId.toString().charAt(1);
	        }
	        //
	        if (spotData.AlertInfo && (spotData.AlertInfo.indexOf("_2") > -1 || spotData.AlertInfo.indexOf("_3") > -1)) { // cell or area
	            marker = createCellIcon(point, baseIcon, spotData.IconId, size);
	            map.addOverlay(marker);
	            bounds.extend(point);
	        }
	    }
	}

	if (NumberOfSpots > 0) {
		for (i = 0; i < NumberOfSpots; i++) {
			var spotData = Spots.Spots[i];
			var point = new GLatLng(spotData.Y, spotData.X);

			var town = "-";
			if (spotData.Town && spotData.Town.length > 0)
			    town = spotData.Town;

			marker = createMarker(point, baseIcon, spotData.Y, spotData.X, spotData.Label, spotData.RequestTime, town, spotData.PostCode, spotData.Speed, spotData.Direction, spotData.IconId);
			map.addOverlay(marker);
			bounds.extend(point);

			//Icon size
			var size = 1;
			if (spotData.IconId.toString().length == 3) {
			    range = spotData.IconId.toString().charAt(0) + spotData.IconId.toString().charAt(1);
			    size = spotData.IconId.toString().charAt(2);
			}

			if (spotData.IconId.toString().length == 2) {
			    range = spotData.IconId.toString().charAt(0);
			    size = spotData.IconId.toString().charAt(1);
			}

			//
			if (spotData.AlertInfoImage != null) {
			    marker = CreateStatusIcon(spotData.AlertInfoImage, baseIcon, point, size);
			    map.addOverlay(marker);
			    bounds.extend(point);
			}
//			if (CreateStatusIcon(spotData.AlertInfo, baseIcon, point, size) != null) {
//			    marker = CreateStatusIcon(spotData.AlertInfo, baseIcon, point, size);
//			    map.addOverlay(marker);
//			    bounds.extend(point);
//			}

			var accuracy = spotData.Accuracy;
			if (accuracy != null && accuracy > 1) {
	            var liColor = "#0000FF"
	            drawCircle(point, accuracy / 1000, 90, bounds, liColor, 1);
	            circleAdded = true;
            }
		}

		var zoomlevel = map.getBoundsZoomLevel(bounds);
		if (zoomlevel > 14) {
			if (circleAdded) {
				zoomlevel--;
			}
		}
		map.setCenter(bounds.getCenter(), zoomlevel);
	}
	else {
	    ShowNotificationText(Javascript_NoLocationsFound);
		return;
	}
}

function createMarker(point, baseIcon, Y, X, Label, RequestTime, Town, PostCode, Speed, Direction, IconId) {
    var custIcon = new GIcon(baseIcon);
    if(Speed != "")
        custIcon.image = getDirectionImage(Direction, IconId);
    else
        custIcon.image = getDirectionImage(0, IconId);

    var size = 1;
    if (IconId.toString().length == 3) {
        range = IconId.toString().charAt(0) + IconId.toString().charAt(1);
        size = IconId.toString().charAt(2);
    }

    if (IconId.toString().length == 2) {
        range = IconId.toString().charAt(0);
        size = IconId.toString().charAt(1);
    }

    if (size == 1) {
        custIcon.iconSize = new GSize(16, 16);
    }
    if (size == 2) {
        custIcon.iconSize = new GSize(24, 24);
    }
    if (size == 3) {
        custIcon.iconSize = new GSize(32, 32);
    }

    var marker = new GMarker(point, { draggable: false, icon: custIcon, zIndexProcess: importanceOrder });
    marker.importance = 100;
    var Proximity;
    if (PostCode != null && PostCode != 'null')
	    Proximity = PostCode + ' ' + Town;
	else
	    Proximity = Town;
	GEvent.addListener(marker, "mouseover", function()
	{ ShowSpotInfo(marker, Y, X, Label, RequestTime, Proximity, Speed, Direction); });

	return marker;
}

function createCellIcon(point, baseIcon, IconId, size) {
    var custIcon = new GIcon(baseIcon);
    custIcon.image = 'Misc/Graphics/PNGs/64cell.png';
    custIcon.iconSize = new GSize(64, 64);

    if (size == 1) {
        custIcon.iconAnchor = new GPoint(32, 58);
    }
    if (size == 2) {
        custIcon.iconAnchor = new GPoint(28, 54);
    }
    if (size == 3) {
        custIcon.iconAnchor = new GPoint(24, 50);
    }

    var marker = new GMarker(point, { draggable: false, icon: custIcon, zIndexProcess: importanceOrder });
    marker.importance = 1;

    return marker;
}

function CreateStatusIcon(alertInfoImage, baseIcon, point, size) {
    var custIcon = new GIcon(baseIcon);
    custIcon.iconSize = new GSize(16, 16);
    custIcon.iconAnchor = new GPoint(16, 24);

    if (size == 2) {
        custIcon.iconAnchor = new GPoint(20, 20);
    }
    if (size == 3) {
        custIcon.iconAnchor = new GPoint(16, 12);
    }

    custIcon.image = alertInfoImage;

    var marker = new GMarker(point, { draggable: false, icon: custIcon, zIndexProcess: importanceOrder });
    marker.importance = 90;

    return marker;
}

// Helpder function for setting Z-Index on a marker
function importanceOrder(marker, b) {
    return marker.importance;
}

function ShowSpotInfo(marker, Y, X, Member, RequestTime, Proximity, Speed, Direction) {
	if (marker) {
		htmlSpot = '<div id="infoSpotOne">';
		htmlSpot += '<table><tr><td colspan="3"><h4>' + Javascript_ballon_locationInfo + '</h4></td></tr>';
		htmlSpot += '<tr><td>' + Javascript_ballon_device + '</td><td>:&nbsp;</td><td>' + Member + '</td></tr>';
		htmlSpot += '<tr><td>' + Javascript_ballon_dateTime + '</td><td>:&nbsp;</td><td>' + RequestTime + '</td></tr>';
		htmlSpot += '<tr><td>' + Javascript_ballon_location + '</td><td>:&nbsp;</td><td>' + Proximity + '</td></tr>';
		htmlSpot += '<tr><td>' + Javascript_ballon_latitude + '</td><td>:&nbsp;</td><td>' + Y + '</td></tr>';
		htmlSpot += '<tr><td>' + Javascript_ballon_longitude + '</td><td>:&nbsp;</td><td>' + X + '</td></tr>';

		if (Speed != "") {
			htmlSpot += '<tr><td>' + Javascript_ballon_speedAndDir + '</td><td>:&nbsp;</td><td>' + Speed + ' ' + hdnDistanceUnit.value + ' / ' + getDirection(Direction) + ' </td></tr>';
		}
		htmlSpot += '</table></div>';
		marker.openInfoWindowHtml(htmlSpot);
	}
}

function SetNewGeoFence(GeoCentLatLng) {
	map.clearOverlays();
	radius = GetNewFenceRadius(map.getZoom());

	// Used for storing geofence
	
	GeoCentLat = GeoCentLatLng.lat();
	GeoCentLng = GeoCentLatLng.lng();

	drawCircle(GeoCentLatLng, radius, 90, new GLatLngBounds());

	map.setCenter(GeoCentLatLng, map.getZoom());
}

function SetGeoFenceItem(data) {
	map.clearOverlays();
	var lat_1 = data[0][0]; 	// left top
	var lng_1 = data[0][1];
	var lat_2 = data[1][0]; 	// left bottom
	var lng_2 = data[2][1];

	var lat = (Math.abs(lat_1) - Math.abs(lat_2)) / 2;
	var lng = (Math.abs(lng_1) - Math.abs(lng_2)) / 2;

	GeoCentLat = lat_1 - lat;
	GeoCentLng = lng_1 + Math.abs(lng);
	radius = data[0][2];

	var bounds = new GLatLngBounds();
	var zm = CalculateZoomLevel(radius * 1000)
	map.setCenter(new GLatLng(GeoCentLat, GeoCentLng), zm);
	drawCircle(new GLatLng(GeoCentLat, GeoCentLng), radius, 90, bounds);
}

function MoveGeoFence(direction) {
	if (GeoCentLat == null && GeoCentLng == null) {
	    lblInfoGeofence.innerHTML = Javascript_geofence_GiveAPointOnTheMap;
		return;
	}

	var variatie = 0.002;

	if (direction == 'E') { GeoCentLng = GeoCentLng + variatie; }  // East movement
	if (direction == 'N') { GeoCentLat = GeoCentLat + variatie; }  // North movement
	if (direction == 'W') { GeoCentLng = GeoCentLng - variatie; }  // West movement
	if (direction == 'S') { GeoCentLat = GeoCentLat - variatie; }  // South movement

	DrawGeofence(radius);
}

function ChangeRadius(parm) {
	if (GeoCentLat == null && GeoCentLng == null) {
	    lblInfoGeofence.innerHTML = Javascript_geofence_GiveAPointOnTheMap;
		return;
	}

	if (parm == '-') {
		if (radius < 0.10) {
		    lblInfoGeofence.innerHTML = Javascript_geofence_ToSmall;
			return;
		}
		radius = radius * 0.9;  //- fenceIncrement * (16 - map.getZoom());  //  radius = radius - (fenceIncrement * 53.6);
	}
	if (parm == '+') {
		if (radius > 1000000) {
		    lblInfoGeofence.innerHTML = Javascript_geofence_ToBig;
			return;
		}
		radius = radius * 1.1;  // + fenceIncrement * (16 - map.getZoom());
	}

	DrawGeofence(radius);
}

function DrawGeofence(geoRadius) {
	map.clearOverlays();

	drawCircle(new GLatLng(GeoCentLat, GeoCentLng), geoRadius, 90, new GLatLngBounds());

	map.setCenter(new GLatLng(GeoCentLat, GeoCentLng), CalculateZoomLevel(geoRadius * 1000));
}

function drawCircle(center, radius, nodes, bounds, liColor, liWidth, liOpa, fillColor, fillOpa) {
	//calculating km/degree
	var latConv = center.distanceFrom(new GLatLng(center.lat() + 0.1, center.lng())) / 100;
	var lngConv = center.distanceFrom(new GLatLng(center.lat(), center.lng() + 0.1)) / 100;

	//Loop 
	var points = [];
	var step = parseInt(360 / nodes) || 40;
	for (var i = 0; i <= 360; i += step) {
		var pint = new GLatLng(center.lat() + (radius / latConv * Math.cos(i * Math.PI / 180)), center.lng() +
	    (radius / lngConv * Math.sin(i * Math.PI / 180)));
		points.push(pint);
		bounds.extend(pint); //this is for fit function
	}
	fillColor = fillColor || liColor || "#0055ff";
	liWidth = liWidth || 2;
	var poly = new GPolygon(points, liColor, liWidth, liOpa, fillColor, fillOpa);
	map.addOverlay(poly);
}

function CancelGeofence() {
	geofencePopupOpen = false;
	map.clearOverlays();
}

function showAddress(address) {
    var address = txtGeocode.value;
    if (geocoder) {
        geocoder.getLatLng(
      address,
      function(point) {
          if (!point) {
              alert(address + " not found");
          } else {
              map.setCenter(point, 13);
              var marker = new GMarker(point);
              map.addOverlay(marker);
              marker.openInfoWindowHtml(address);
          }
      }
    );
    }
}

/*__________  E N D  ___________*/
if (typeof (Sys) != "undefined") { // nodig bij inlezen via AJAX Scriptmanager
	Sys.Application.notifyScriptLoaded();
}
