var radius = 0.25; 				// radius.value
var lngMove = 0;
var latMove = 0;
var fenceIncrement = 0.03; 	// Change value Fence border.    oud 0.0004;
var fenceDistance = 0.004; 		// Min Max value Fence c.q. Radius.
// var geofencePopupOpen = false Set in General.js;
var Sort;
var showFenceDiv;
var newGeofenceName = ''; // set the refreshed dropdown to this one 

function selectInsertGeofence(sort) {
    Sort = sort;
	if (sort == 'select') {
		geofencePopupOpen = false;
		$get('ModalPopupOutsideBehaviorID_backgroundElement').style.display = 'inline';
		$get('selectDiv').style.display = 'block';
		$get('insertDiv').style.display = 'none';
		$get('NewGeofenceButton').style.display = 'none';
		$get('RegularButtons').style.display = 'block';
		$get('Dynamic_TabContainer1_GeoFenceTab2_tab').style.display = 'inline';
		$get('Dynamic_TabContainer1_GeoFenceTab3_tab').style.display = 'inline';
	}
	else {
	    // dit is de 'insert'.
	    
	    if (dosScreenToggle == 'expandmessage') { DosScreenSelect('standard'); }
		geofencePopupOpen = true;
		// Gray backGround ModalPopUp disabled.
		$get('ModalPopupOutsideBehaviorID_backgroundElement').style.display = 'none';
		$get('Dynamic_TabContainer1_GeoFenceTab2_tab').style.display = 'none';
		$get('Dynamic_TabContainer1_GeoFenceTab3_tab').style.display = 'none';
		$get('selectDiv').style.display = 'none';
		$get('insertDiv').style.display = 'block';
		$get('divGeoFenceHide_2').style.display = 'block';
		$get('NewGeofenceButton').style.display = 'block';
		$get('RegularButtons').style.display = 'none';

		GeoCentLat = null;
		GeoCentLng = null;
		
		if (usrMap == 'virtualearth' && map != null) {
		    clearVEMap();
		    //ClearMapWorkAround();
		}
		else if (usrMap == 'googlemaps' && map != null) {
			map.clearOverlays();
		}
	}
}

function ShowGeoFence(show) {
    if (dosScreenToggle == 'expandmessage'){ DosScreenSelect('standard'); }
    
    showFenceDiv = show;
	if (GeofenceListID != null) {
		geofencePopupOpen = true;

		if (showFenceDiv) {
		    $get('divGeoFenceHide_1').style.display = 'block';
		    $get('divGeoFenceHide_2').style.display = 'block';
		}
		var geoFenceID = GeofenceListID.value;
		// After selected item in the list -> display GeoFence
		if (geoFenceID != -1)
			FindWhere.Locations.PlotService.GetGeoFenceItem(geoFenceID, SetGeoFenceItem, onResultFalse);
	}
}
function StoreNewGeofence() {
    var nameExists = false;
    for (i = 0; i < GeofenceListID.options.length; i++) {
        if (GeofenceListID.options[i].text == txtGeoFenceID.value.substring(0, 19))
        { nameExists = true; } 
    }
    if (nameExists){
        lblInfoGeofence.innerHTML = Javascript_geofence_nameAlreadyExists;
        return;
    }

	if (txtGeoFenceID.value.length > 2) {
	    if (GeoCentLat != null && GeoCentLng != null) {
	        newGeofenceName = txtGeoFenceID.value.substring(0, 19);
	        txtGeoFenceID.value = '';
	        FindWhere.Locations.PlotService.StoreNewGeofence(newGeofenceName, radius, GeoCentLat, GeoCentLng, onResultStoreGeofenceTrue, onResultFalse)
	        // Close new div
	        selectInsertGeofence('select');
		}
		else
		    lblInfoGeofence.innerHTML = Javascript_geofence_GiveAPointOnTheMap;
	}
	else
	    lblInfoGeofence.innerHTML = Javascript_geofence_GiveAName;
}

function EditGeofence() {
	if (GeofenceListID.value > 0) {
		// TODO Dezelfde tekst wordt hier teruggegeven zonder deze te kunnen wijzigen.
		FindWhere.Locations.PlotService.EditGeofence(GeofenceListID.value, GeofenceListID.options[GeofenceListID.selectedIndex].text, radius, GeoCentLat, GeoCentLng, onResultStoreGeofenceTrue, onResultFalse)
	}
	else
	    lblInfoGeofence.innerHTML = Javascript_geofence_SelectAGeofence;
}

function DeleteGeofence() {
    if (GeofenceListID.value > 0) {
        // TODO Dezelfde tekst wordt hier teruggegeven zonder deze te kunnen wijzigen.
        FindWhere.Locations.PlotService.DeleteGeofence(GeofenceListID.value, onResultDeleteGeofenceTrue, onResultDeleteGeofenceFalse)
    }
    else
        lblInfoGeofence.innerHTML = Javascript_geofence_SelectAGeofence;
}

function onResultStoreGeofenceTrue(data) {
	if (data == true) {
	    lblInfoGeofence.innerHTML = Javascript_geofence_Stored;

	    // refresh the geofences dropdown
	    // restore the slection div
	    geofencePopupOpen = false;
	    document.styleSheets[0].disabled = false;
	    $get('selectDiv').style.display = 'block';
	    $get('insertDiv').style.display = 'none';

	    // update the dropdown!
	    if (newGeofenceName!="")
	    FindWhere.Locations.PlotService.GetGeofences(onResultGetGeofencesTrue, onResultFalse)
	}
	else
	    lblInfoGeofence.innerHTML = Javascript_geofence_NotStored;
}

function onResultDeleteGeofenceTrue(data) {
    if (data == true) {
        lblInfoGeofence.innerHTML = Javascript_geofence_Deleted;
        // refresh the geofences dropdown
        // restore the slection div
        geofencePopupOpen = false;
        document.styleSheets[0].disabled = false;
        $get('selectDiv').style.display = 'block';
        $get('insertDiv').style.display = 'none';

        // update the dropdown!
         FindWhere.Locations.PlotService.GetGeofences(onResultGetGeofencesTrue, onResultFalse)
    }
    else
        lblInfoGeofence.innerHTML = Javascript_geofence_NotDeleted;
}

function onResultDeleteGeofenceFalse(ag) {
    if (ag != null) {
        if (ag._message != null) {
         lblInfoGeofence.innerHTML = ag._message; }
        else {
            lblInfoGeofence.innerHTML = Javascript_geofence_NotDeleted;
        }
        }
    
    else
        lblInfoGeofence.innerHTML = Javascript_geofence_NotDeleted;
}

function onResultGetGeofencesTrue(data) {
    GeofenceListID.options.length = 0;

    if (data != null) {
        var NumberOfFences = data.length;

        GeofenceListID.options.add(new Option(Javascript_SelectAOption, '0'));
        
        for (i = 0; i < NumberOfFences; i++) {
            GeofenceListID.options.add(new Option(data[i][0], data[i][1]));
        }
        i = 0;
        for (i = 0; i < NumberOfFences; i++) {
            if (data[i][0] == newGeofenceName)
            GeofenceListID[i+1].selected = 'selected';
        }
    }
    newGeofenceName = "";
}

function CalculateZoomLevel(gr) {
    gr = gr / 1000;
    if (gr < 0.15) return 17;
    if (gr < 0.3)  return 16;
    if (gr < 0.75) return 15;
    if (gr < 1.3)  return 14;
    if (gr < 3.0)  return 13;
    if (gr < 6.7)  return 12;
    if (gr < 8)    return 11;
    if (gr < 15)   return 10;
    if (gr < 30)   return 9;
    if (gr < 50)   return 8;
    if (gr < 80)   return 7;
    if (gr < 150)  return 6;
    if (gr < 250)  return 5;
    if (gr < 450)  return 4;
    if (gr < 800) return  3;
    if (gr < 1000) return 2;
    return 1;
}

function GetNewFenceRadius(zoomlevel) {
    if (zoomlevel == 17) return 0.1;
    if (zoomlevel == 16) return 0.2;
    if (zoomlevel == 15) return 0.4;
    if (zoomlevel == 14) return 0.75;
    if (zoomlevel == 13) return 1.3;
    if (zoomlevel == 12) return 3.0;
    if (zoomlevel == 11) return 6.7;
    if (zoomlevel == 10) return 10;
    if (zoomlevel == 9) return 20;
    if (zoomlevel == 8) return 40;
    if (zoomlevel == 7) return 60;
    if (zoomlevel == 6) return 100;
    if (zoomlevel == 5) return 200;
    if (zoomlevel == 4) return 300;
    if (zoomlevel == 3) return 600;
    if (zoomlevel == 2) return 1000;
    if (zoomlevel == 1) return 1000;

}

function ClearMapWorkAround() {
    map.DeleteAllShapes();
    map.DeleteRoute();
    map.DeleteAllShapeLayers();
}

/*______________  E N D  _____________________*/
if (typeof (Sys) != 'undefined') { // nodig bij inlezen via AJAX Scriptmanager
	Sys.Application.notifyScriptLoaded();
}





