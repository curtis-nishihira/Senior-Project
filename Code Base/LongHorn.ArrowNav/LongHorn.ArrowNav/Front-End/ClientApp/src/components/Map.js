import React, { useRef, useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
// eslint-disable-next-line import/no-webpack-loader-syntax
import mapboxgl from '!mapbox-gl';
import './Map.css';

mapboxgl.accessToken = process.env.REACT_APP_MAPBOX_TOKEN;

//Run a few walking options and possibly updated the survey values to check if the time is being added

export const Map = () => {
    const mapContainerRef = useRef(null);
    const [lng, setLng] = useState(-118.112437);
    const [lat, setLat] = useState(33.782105);
    const [zoom, setZoom] = useState(14.75);
    const buildingName = useRef("");
    const buildingLat = useRef(0.0);
    const buildingLong = useRef(0.0);
    const location = useLocation();

    //USU
    const Zone1 = [33.781604, -118.114287, 33.782103, -118.112431];
    //Bookstore
    const Zone2 = [33.779446, -118.113831, 33.780275, -118.112984];
    //Library
    const Zone3 = [33.783709, -118.110060, 33.784859, -118.108612];

    const [firstRouteDuration, setFirstRouteDuration] = useState(0);
    const [secondRouteDuration, setSecondRouteDuration] = useState(0);
    const [firstRouteDistance, setFirstRouteDistance] = useState(0);
    const [secondRouteDistance, setSecondRouteDistance] = useState(0);



    // Initialize map when component mounts
    useEffect(() => {

        //this function is async which means it will return a promise if it isn't already.
        //when calling this function you'll have to unwrap it using await or .then
        async function fetchData(url, methodType, bodyData) {
            if (methodType === "GET") {
                const response = await fetch(url);
                const data = await response.json();
                return data;
            }
            else if (methodType === "POST")
            {
                const response = await fetch(url, { method: methodType })
                const data = await response.json();
                return data;
            }

        }
        
        async function fetchWalkingData(url, zoneUrl,routeID,RouteColor) {
            const coordinates = [];
            var TimeAdditions = [];
            var data = await fetchData(url, "GET", [])
            for (let i = 0; i < data.routes[0].legs[0].steps.length; i++) {
                coordinates.push(data.routes[0].legs[0].steps[i].maneuver.location);
            }
            var zoneData = await fetchData(zoneUrl, "GET", []);
            console.log(zoneData)
            for (const [key, value] of Object.entries(zoneData)) {
                var zoneValue = value["item1"];
                console.log(zoneValue);
                var surveyCount = value["item2"];
                console.log(surveyCount);
                var timeAdded = zoneValue / surveyCount;
                if (isFinite(timeAdded)) {
                    TimeAdditions.push([key, Math.ceil(timeAdded)]);
                }
                else {
                    TimeAdditions.push([key, 0]);
                }
            }
            const timeAddedForRoute = zonesPassed(coordinates, TimeAdditions);
            let distance = data.routes[0].distance / 1609;
            if (routeID === "route") {
                setFirstRouteDistance(distance.toFixed(2));
                setFirstRouteDuration(Math.round((data.routes[0].duration) / 60) + timeAddedForRoute);
            }
            else
            {
                setSecondRouteDistance(distance.toFixed(2));
                setSecondRouteDuration(Math.round((data.routes[0].duration) / 60) + timeAddedForRoute);
            }
            
            

            addLayer(coordinates, RouteColor, routeID);
        }
        //adds the route onto the map
        function addLayer(route,routeColor,routeID) {
            map.addLayer({
                "id": routeID,
                "type": "line",
                "source": {
                    "type": "geojson",
                    "data": {
                        "type": "Feature",
                        "properties": {},
                        "geometry": {
                            "type": 'LineString',
                            'coordinates': route
                        }
                    }
                },
                "layout": {
                    "line-join": "round",
                    "line-cap": "round"
                },
                "paint": {
                    "line-color": routeColor,
                    "line-width": 12,
                    "line-opacity": 0.8
                }
            });
        }
        /*
         * removes any of the routes that are currently displayed on the map. This is used when we are creating new routes.
         * To avoid overlapping routes or multiple routes for different destinations.
         */ 
        function removeAllRoutes()
        {
            if (map.getSource('route')) {
                map.removeLayer('route')
                map.removeSource('route')
            }
            if (map.getSource('route2')) {
                map.removeLayer('route2')
                map.removeSource('route2')
            }
        }
        //this function is filling in the datalist that is used for the search bar
        async function fillComboBox() {
            var fillBoxUrl = process.env.REACT_APP_FETCH + '/building/getAllBuildings';
            var x = await fetchData(fillBoxUrl,"GET",[]);
            var listOfBuildings = x;
            var sel = document.getElementById('buildings');
            for (var i = 0; i < listOfBuildings.length; i++) {
                var opt = document.createElement('option');
                opt.innerHTML = listOfBuildings[i];
                opt.textContent = listOfBuildings[i];
                opt.value = listOfBuildings[i];
                sel.appendChild(opt);
            }
            
        }

        function calculateRouteInfo(data,route,secondRoute) {
            if (data.routes.length >= 2) {
                let distance = data.routes[0].distance / 1609;
                setFirstRouteDistance(distance.toFixed(2));
                setFirstRouteDuration(Math.round((data.routes[0].duration) / 60));

                for (let i = 0; i < data.routes[0].legs[0].steps.length; i++) {
                    route.push(data.routes[0].legs[0].steps[i].maneuver.location);
                }
                let secondRouteDistance = data.routes[1].distance / 1609;
                setSecondRouteDistance(secondRouteDistance.toFixed(2));
                setSecondRouteDuration(Math.round((data.routes[1].duration) / 60));

                for (let i = 0; i < data.routes[1].legs[0].steps.length; i++) {
                    secondRoute.push(data.routes[1].legs[0].steps[i].maneuver.location);
                }
                addLayer(route, "#0096FF", "route");
                addLayer(secondRoute, "#ff0000", "route2");

                document.getElementById("first-route-info").style.visibility = "visible";
                document.getElementById("second-route-info").style.visibility = "visible";
            }
            else {
                let distance = data.routes[0].distance / 1609;
                setFirstRouteDistance(distance.toFixed(2));
                setFirstRouteDuration(Math.round((data.routes[0].duration) / 60));

                for (let i = 0; i < data.routes[0].legs[0].steps.length; i++) {
                    route.push(data.routes[0].legs[0].steps[i].maneuver.location);
                }
                addLayer(route, "#0096FF", "route");

                document.getElementById("first-route-info").style.visibility = "visible";
                document.getElementById("second-route-info").style.visibility = "hidden";
            }
        }
        const map = new mapboxgl.Map({
            container: mapContainerRef.current,
            style: 'mapbox://styles/brayan-fuentes21/cky54exmf1uvl14qcvixitiqo',
            center: [lng, lat],
            zoom: zoom
        });

        const endPoint = new mapboxgl.Marker();

        if (location.state != undefined) {
            putPin(location.state.building);
        }

        fillComboBox();

        //calls the mapbox api for the routes and route info which are then added to their respective placeholders
        async function drivingRoute() {
            removeAllRoutes()
            document.getElementById("information").style.visibility = "visible";
            if ("geolocation" in navigator) {
                navigator.geolocation.getCurrentPosition(async (position) => {
                    var userLat = position.coords.latitude;
                    var userLng = position.coords.longitude;
                    const lngLat = endPoint.getLngLat();
                    let locationLat = lngLat.lat;
                    let locationLng = lngLat.lng;
                    const url = "https://api.mapbox.com/directions/v5/mapbox/driving-traffic/" + userLng + "," + userLat + ";" + locationLng + "," + locationLat + "?waypoints=0;1&alternatives=true&steps=true&access_token=" + mapboxgl.accessToken;
                    const route = [];
                    const secondRoute = [];
                    var apiData = await fetchData(url,"GET", []);
                    calculateRouteInfo(apiData,route,secondRoute);
                });
            }
            else {
                console.log("can't obtain user location");
            }

        };
        //calls the mapbox api for the routes and route info which are then added to their respective placeholders
        function cyclingRoute() {
            removeAllRoutes()
            document.getElementById("information").style.visibility = "visible";
            if ("geolocation" in navigator) {
                navigator.geolocation.getCurrentPosition(async (position) => {
                    var userLat = position.coords.latitude;
                    var userLng = position.coords.longitude;
                    const lngLat = endPoint.getLngLat();
                    let locationLat = lngLat.lat;
                    let locationLng = lngLat.lng;
                    const url = "https://api.mapbox.com/directions/v5/mapbox/cycling/" + userLng + "," + userLat + ";" + locationLng + "," + locationLat + "?waypoints=0;1&alternatives=true&steps=true&access_token=" + mapboxgl.accessToken;
                    const route = [];
                    const secondRoute = [];
                    var apiData = await fetchData(url, "GET", []);
                    calculateRouteInfo(apiData, route, secondRoute);
                });
            }
            else {
                console.log("can't obtain user location");
            }
        };

        function zonesPassed(listOfCoordinates,TimeAdditions) {
            const listofPassedZones = []
            var addedTime = 0;
            for (let x = 0; x < listOfCoordinates.length; x++) {
                if ((listOfCoordinates[x][0] >= Zone1[1] && listOfCoordinates[x][0] <= Zone1[3]) && (listOfCoordinates[x][1] >= Zone1[0] && listOfCoordinates[x][1] <= Zone1[2])) {
                    if (!listofPassedZones.includes("Zone1")) {
                        listofPassedZones.push("Zone1");
                    }
                }
                else if ((listOfCoordinates[x][0] >= Zone2[1] && listOfCoordinates[x][0] <= Zone2[3]) && (listOfCoordinates[x][1] >= Zone2[0] && listOfCoordinates[x][1] <= Zone2[2])) {
                    if (!listofPassedZones.includes("Zone2")) {
                        listofPassedZones.push("Zone2");
                    }
                }
                else if ((listOfCoordinates[x][0] >= Zone3[1] && listOfCoordinates[x][0] <= Zone3[3]) && (listOfCoordinates[x][1] >= Zone3[0] && listOfCoordinates[x][1] <= Zone3[2])) {
                    if (!listofPassedZones.includes("Zone3")) {
                        listofPassedZones.push("Zone3");
                    }
                }
            }
            if (listofPassedZones.includes("Zone1")) {
                addedTime = addedTime + TimeAdditions[0][1];

            }
            if (listofPassedZones.includes("Zone2")) {
                addedTime = addedTime + TimeAdditions[1][1];

            }
            if (listofPassedZones.includes("Zone3")) {
                addedTime = addedTime + TimeAdditions[2][1];
            }
            return addedTime;
        }

        async function putPin(building) {
            var data = await fetchData(process.env.REACT_APP_FETCH + "/building/getLatLong?BuildingName=" + building, "POST", []);
            buildingLat.current = data.latitude;
            buildingLong.current = data.longitude;
            console.log(buildingLong.current)
            endPoint.setLngLat([buildingLong.current, buildingLat.current]);
            endPoint.addTo(map);
            document.getElementById('button-container').style.visibility = 'visible';

        }

        // Called when building icon is clicked and creates popup
        function getCapacity(building, coordinates) {
            fetch(process.env.REACT_APP_FETCH + "/capacity/getCapacity?BuildingName=" + building, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
            })
                .then(response => response.json())
                .then(data => {
                    new mapboxgl.Popup()
                        .setLngLat(coordinates)
                        .setHTML('<strong>'+ building +' Capacity</strong><p>Hours: ' + data._Time + '</p><p>Website: <a href=' + data._WebLink + '>' + data._WebLink + '</a></p><p>Busy Level: ' + data._CapacityValue + '</p>')
                        .addTo(map);
                    return data;
                })
                .catch((error) => {
                    console.error('Error', error);
                });
        }

        // all images to be used as icons with mapbox must loaded using this function
        function loadImage(filename) {
            map.loadImage(
                process.env.REACT_APP_IMAGES + filename,
                (error, image) => {
                    if (error) throw error;
                    // remove file ending and front slash
                    map.addImage(filename.substring(1, filename.length - 4), image);
                }
            );
        }

        const geolocateControl = new mapboxgl.GeolocateControl({
            positionOptions: { enableHighAccuracy: true },
            showUserHeading: true
        })

        map.addControl(geolocateControl, 'bottom-right');

        const walkingBtn = document.getElementById("walking-btn");

        walkingBtn.addEventListener("click", () => {
            removeAllRoutes()
            if ("geolocation" in navigator) {
                navigator.geolocation.getCurrentPosition(position => {
                    var userLat = position.coords.latitude;
                    var userLng = position.coords.longitude;
                    const lngLat = endPoint.getLngLat();
                    let locationLat = lngLat.lat;
                    let locationLng = lngLat.lng;
                    const zoneUrl = process.env.REACT_APP_FETCH + "/trafficsurvey";
                    const walkingUrl = "https://api.mapbox.com/directions/v5/mapbox/walking/" + userLng + "," + userLat + ";" + locationLng + "," + locationLat + "?waypoints=0;1&walkway_bias=1&alternatives=true&steps=true&access_token=" + mapboxgl.accessToken;
                    fetchWalkingData(walkingUrl, zoneUrl, "route", "#0096FF");
                    const walkingUrl2 = "https://api.mapbox.com/directions/v5/mapbox/walking/" + userLng + "," + userLat + ";" + locationLng + "," + locationLat + "?waypoints=0;1&walkway_bias=-1&alternatives=true&steps=true&access_token=" + mapboxgl.accessToken;
                    fetchWalkingData(walkingUrl2, zoneUrl, "route2", "#ff0000");
                    document.getElementById("information").style.visibility = "visible";
                    document.getElementById("first-route-info").style.visibility = "visible";
                    document.getElementById("second-route-info").style.visibility = "visible";
                });
            } else {
                console.log("can't obtain user location");
            }
        })

        const searchBtn = document.getElementById("search-btn");
        searchBtn.addEventListener("click", () => {
            console.log(buildingName.current);
            putPin(buildingName.current);
        })

        const drivingBtn = document.getElementById("driving-btn");
        drivingBtn.addEventListener('click', () => {
            drivingRoute();
        })
        const cyclingBtn = document.getElementById("cycling-btn");
        cyclingBtn.addEventListener('click', () => {
            cyclingRoute();
        })

        // json values representing the three capacity feature buildings
        const capacityBuildingsGeojson = {
            'type': 'FeatureCollection',
            'features': [
                {
                    'type': 'Feature',
                    'properties': {
                        'description': "LIB",
                        'icon': 'LibraryIcon',
                        'iconSize': [50, 50]

                    },
                    'geometry': {
                        'type': 'Point',
                        'coordinates': [-118.114428601179, 33.7772435747339]
                    }
                },
                {
                    'type': 'Feature',
                    'properties': {
                        'description': "USU",
                        'icon': 'UsuIcon',
                        'iconSize': [50, 50]
                    },
                    'geometry': {
                        'type': 'Point',
                        'coordinates': [-118.113444706599, 33.7816514218059]
                    }
                },
                {
                    'type': 'Feature',
                    'properties': {
                        'description': "SRWC",
                        'icon': 'GymIcon',
                        'iconSize': [50, 50]
                    },
                    'geometry': {
                        'type': 'Point',
                        'coordinates': [-118.109251028879, 33.785059019334]
                    }
                }
            ]
        };

        map.on('move', () => {
            setLng(map.getCenter().lng.toFixed(4));
            setLat(map.getCenter().lat.toFixed(4));
            setZoom(map.getZoom().toFixed(2));
        });

        // places icons on the map and programs them to call method for creating popups
        map.on('load', () => {
            // Load an image from an internal URL.
            loadImage("/LibraryIcon.png");
            loadImage("/GymIcon.png");
            loadImage("/UsuIcon.png");

            map.addSource('points', {
                'type': 'geojson',
                'data': capacityBuildingsGeojson
            });

            // Add a layer to use the image to represent the data.
            map.addLayer({
                'id': 'points',
                'type': 'symbol',
                'source': 'points',
                'layout': {
                    'icon-image': '{icon}',
                    'icon-allow-overlap': true,
                    'icon-size': 0.1
                }
            });

            map.on('click', 'points', (e) => {
                const coordinates = e.features[0].geometry.coordinates.slice();
                const description = e.features[0].properties.description;

                getCapacity(description, coordinates);
                
            });
        });
        // Clean up on unmount      
        return function cleanup() {
            map.remove();
            var body = document.getElementById('buildings');
            if (body != null) {
                body.innerHTML = '';
            }
        }
    }, []); // eslint-disable-line react-hooks/exhaustive-deps


    return (
        <div>
            <div id="button-container" className="routing-button-div">
                <button type="button" id="walking-btn"> Walking</button>
                <button type="button" id="driving-btn"> Driving</button>
                <button type="button" id="cycling-btn"> Cycling </button>
                <div id="information" className="route-information">
                    <p id="first-route-info" className="first-route-container">Blue Route = Time:{firstRouteDuration} min | Distance: {firstRouteDistance} mi</p>
                    <p id="second-route-info" className="second-route-container">Red Route = Time:{secondRouteDuration} min | Distance: {secondRouteDistance} mi</p>
                </div>
            </div>
            <div className="search-div">
                <datalist id="buildings">
                </datalist>
                <input className = "search-bar" placeholder="Enter Building Name" autoComplete="on" list="buildings" onSelect={(e) => buildingName.current = e.target.value} />
                <button type="button" id="search-btn" > search</button>
            </div>
            <div className='map-container' ref={mapContainerRef} />
        </div>
    );
}

export default Map;