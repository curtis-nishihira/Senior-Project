import React, { useRef, useEffect, useState } from 'react';
// eslint-disable-next-line import/no-webpack-loader-syntax
import mapboxgl from '!mapbox-gl';
import './Map.css';

mapboxgl.accessToken = "pk.eyJ1IjoiYnJheWFuLWZ1ZW50ZXMyMSIsImEiOiJja3hxdW5ycWo0ZjRmMzBvNHM5ODdxZ2poIn0.MoTF9LUSyOlwGx7L-pCCjw";

export const Map = () => {
    const mapContainerRef = useRef(null);
    const [lng, setLng] = useState(-118.112437);
    const [lat, setLat] = useState(33.782105);
    const [zoom, setZoom] = useState(14.75);
    //lat,long or y,z
    const Zone1 = [33.781604, -118.114287, 33.782103, -118.112431];
    const Zone2 = [33.779446, -118.113831, 33.780275, -118.112984];
    const Zone3 = [33.783709, -118.110060, 33.784859, -118.108612];

    const [firstRouteDuration, setFirstRouteDuration] = useState(0);
    const [secondRouteDuration, setSecondRouteDuration] = useState(0);
    const [firstRouteDistance, setFirstRouteDistance] = useState(0);
    const [secondRouteDistance, setSecondRouteDistance] = useState(0);

    // Initialize map when component mounts
    useEffect(() => {

        function drivingRoute() {
            if (map.getSource('route')) {
                map.removeLayer('route')
                map.removeSource('route')
            }
            if (map.getSource('route2')) {
                map.removeLayer('route2')
                map.removeSource('route2')
            }
            document.getElementById("information").style.visibility = "visible";
            if ("geolocation" in navigator) {
                navigator.geolocation.getCurrentPosition(position => {
                    var userLat = position.coords.latitude;
                    var userLng = position.coords.longitude;
                    const lngLat = endPoint.getLngLat();
                    let locationLat = lngLat.lat;
                    let locationLng = lngLat.lng;
                    const url = "https://api.mapbox.com/directions/v5/mapbox/driving-traffic/" + userLng + "," + userLat + ";" + locationLng + "," + locationLat + "?waypoints=0;1&alternatives=true&steps=true&access_token=" + mapboxgl.accessToken;
                    const route = [];
                    const secondRoute = [];
                    fetch(url, {
                        method: 'GET'
                    })
                        .then(response => response.json())
                        .then(data => {
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
                                map.addLayer({
                                    "id": "route",
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
                                        "line-color": "#0096FF",
                                        "line-width": 12,
                                        "line-opacity": 0.8
                                    }
                                });
                                map.addLayer({
                                    "id": "route2",
                                    "type": "line",
                                    "source": {
                                        "type": "geojson",
                                        "data": {
                                            "type": "Feature",
                                            "properties": {},
                                            "geometry": {
                                                "type": 'LineString',
                                                'coordinates': secondRoute
                                            }
                                        }
                                    },
                                    "layout": {
                                        "line-join": "round",
                                        "line-cap": "round"
                                    },
                                    "paint": {
                                        "line-color": "#ff0000",
                                        "line-width": 12,
                                        "line-opacity": 0.8
                                    }
                                });
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
                                map.addLayer({
                                    "id": "route",
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
                                        "line-color": "#0096FF",
                                        "line-width": 12,
                                        "line-opacity": 0.8
                                    }
                                });
                                document.getElementById("first-route-info").style.visibility = "visible";
                                document.getElementById("second-route-info").style.visibility = "hidden";
                            }
                        })
                        .catch((error) => {
                            console.error('Error:', error);
                        });
                });
            }
            else {
                console.log("can't obtain user location");
            }

        };
        function cyclingRoute() {
            if (map.getSource('route')) {
                map.removeLayer('route')
                map.removeSource('route')
            } if (map.getSource('route2')) {
                map.removeLayer('route2')
                map.removeSource('route2')
            }
            document.getElementById("information").style.visibility = "visible";
            if ("geolocation" in navigator) {
                navigator.geolocation.getCurrentPosition(position => {
                    var userLat = position.coords.latitude;
                    var userLng = position.coords.longitude;
                    const lngLat = endPoint.getLngLat();
                    let locationLat = lngLat.lat;
                    let locationLng = lngLat.lng;
                    const url = "https://api.mapbox.com/directions/v5/mapbox/cycling/" + userLng + "," + userLat + ";" + locationLng + "," + locationLat + "?waypoints=0;1&alternatives=true&steps=true&access_token=" + mapboxgl.accessToken;
                    const route = [];
                    const secondRoute = [];
                    fetch(url, {
                        method: 'GET'
                    })
                        .then(response => response.json())
                        .then(data => {
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
                                map.addLayer({
                                    "id": "route",
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
                                        "line-color": "#0096FF",
                                        "line-width": 12,
                                        "line-opacity": 0.8
                                    }
                                });

                                map.addLayer({
                                    "id": "route2",
                                    "type": "line",
                                    "source": {
                                        "type": "geojson",
                                        "data": {
                                            "type": "Feature",
                                            "properties": {},
                                            "geometry": {
                                                "type": 'LineString',
                                                'coordinates': secondRoute
                                            }
                                        }
                                    },
                                    "layout": {
                                        "line-join": "round",
                                        "line-cap": "round"
                                    },
                                    "paint": {
                                        "line-color": "#ff0000",
                                        "line-width": 12,
                                        "line-opacity": 0.8
                                    }
                                });
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
                                if (map.getSource('route')) {
                                    map.removeLayer('route')
                                    map.removeSource('route')
                                } else {
                                    map.addLayer({
                                        "id": "route",
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
                                            "line-color": "#0096FF",
                                            "line-width": 12,
                                            "line-opacity": 0.8
                                        }
                                    });
                                };

                                document.getElementById("first-route-info").style.visibility = "visible";
                                document.getElementById("second-route-info").style.visibility = "hidden";
                            }
                        })
                        .catch((error) => {
                            console.error('Error:', error);
                        });
                });
            }
            else {
                console.log("can't obtain user location");
            }
        };

        function zonesPassed(listOfCoordinates) {
            const listofPassedZones = []
            for (let x = 0; x < listOfCoordinates.length; x++)
            {
                if ((listOfCoordinates[x][0] >= Zone1[1] && listOfCoordinates[x][0] <= Zone1[3]) && (listOfCoordinates[x][1] >= Zone1[0] && listOfCoordinates[x][1] <= Zone1[2]))
                {
                    if (!listofPassedZones.includes("Zone1"))
                    {
                        listofPassedZones.push("Zone1");
                    }
                }
                else if ((listOfCoordinates[x][0] >= Zone2[1] && listOfCoordinates[x][0] <= Zone2[3]) && (listOfCoordinates[x][1] >= Zone2[0] && listOfCoordinates[x][1] <= Zone2[2]))
                {
                    if (!listofPassedZones.includes("Zone1"))
                    {
                        listofPassedZones.push("Zone1");
                    }
                }
                else if ((listOfCoordinates[x][0] >= Zone3[1] && listOfCoordinates[x][0] <= Zone3[3]) && (listOfCoordinates[x][1] >= Zone3[0] && listOfCoordinates[x][1] <= Zone3[2]))
                {
                    if (!listofPassedZones.includes("Zone1"))
                    {
                        listofPassedZones.push("Zone1");
                    }
                }
            }
            return listofPassedZones;
        }

        const map = new mapboxgl.Map({
            container: mapContainerRef.current,
            style: 'mapbox://styles/brayan-fuentes21/cky54exmf1uvl14qcvixitiqo',
            center: [lng, lat],
            zoom: zoom
        });

        const geolocateControl = new mapboxgl.GeolocateControl({
            positionOptions: { enableHighAccuracy: true },
            showUserHeading: true
        })

        map.addControl(geolocateControl, 'bottom-right');

        const endPoint = new mapboxgl.Marker();

        const walkingBtn = document.getElementById("walking-btn");

        walkingBtn.addEventListener("click", () => {
            if (map.getSource('route')) {
                map.removeLayer('route')
                map.removeSource('route')
            }
            if (map.getSource('route2')) {
                map.removeLayer('route2')
                map.removeSource('route2')
            }
            if ("geolocation" in navigator) {
                navigator.geolocation.getCurrentPosition(position => {
                    var userLat = position.coords.latitude;
                    var userLng = position.coords.longitude;
                    const lngLat = endPoint.getLngLat();
                    let locationLat = lngLat.lat;
                    let locationLng = lngLat.lng;
                    const url = "https://api.mapbox.com/directions/v5/mapbox/walking/" + userLng + "," + userLat + ";" + locationLng + "," + locationLat + "?waypoints=0;1&walkway_bias=1&alternatives=true&steps=true&access_token=" + mapboxgl.accessToken;
                    const coordinates = [];
                    fetch(url, {
                        method: 'GET'
                    })
                        .then(response => response.json())
                        .then(data => {
                            const zoneUrl = "https://arrownav.azurewebsites.net/trafficsurvey";
                            var TimeAdditions = [];
                            for (let i = 0; i < data.routes[0].legs[0].steps.length; i++) {
                                coordinates.push(data.routes[0].legs[0].steps[i].maneuver.location);
                            }
                            fetch(zoneUrl, {
                                method: 'GET',
                            })
                                .then(response => response.json())
                                .then(data2 => {
                                    //loop through the keys and save them to the list which will be used later to update the duration of the route 
                                    for (const [key, value] of Object.entries(data2)) {
                                        var zoneValue = value["item1"];
                                        var surveyCount = value["item2"];
                                        var timeAdded = zoneValue / surveyCount;
                                        if (isNaN(timeAdded)) {
                                            TimeAdditions.push([key, 0]);
                                        }
                                        else {
                                            TimeAdditions.push([key, Math.ceil(timeAdded)]);
                                        }
                                    }
                                    const zones = zonesPassed(coordinates);
                                    var timeAddedForRoute1 = 0;
                                    if (zones.includes("Zone1")) {
                                        timeAddedForRoute1 = timeAddedForRoute1  + TimeAdditions[0][1];

                                    }
                                    if (zones.includes("Zone2")) {
                                        timeAddedForRoute1 = timeAddedForRoute1 + TimeAdditions[1][1];

                                    }
                                    if (zones.includes("Zone3")) {
                                        timeAddedForRoute1 = timeAddedForRoute1 + TimeAdditions[2][1];
                                    }

                                    let distance = data.routes[0].distance / 1609;
                                    setFirstRouteDistance(distance.toFixed(2));
                                    setFirstRouteDuration(Math.round((data.routes[0].duration) / 60) + timeAddedForRoute1);
                                })
                                .catch((error) => {
                                    console.error('Error', error);
                                });

                            map.addLayer({
                                "id": "route",
                                "type": "line",
                                "source": {
                                    "type": "geojson",
                                    "data": {
                                        "type": "Feature",
                                        "properties": {},
                                        "geometry": {
                                            "type": 'LineString',
                                            'coordinates': coordinates
                                        }
                                    }
                                },
                                "layout": {
                                    "line-join": "round",
                                    "line-cap": "round"
                                },
                                "paint": {
                                    "line-color": "#0096FF",
                                    "line-width": 12,
                                    "line-opacity": 0.8
                                }
                            });
                        })
                        .catch((error) => {
                            console.error('Error:', error);
                        });
                    const coords = [];
                    const url2 = "https://api.mapbox.com/directions/v5/mapbox/walking/" + userLng + "," + userLat + ";" + locationLng + "," + locationLat + "?waypoints=0;1&walkway_bias=-1&alternatives=true&steps=true&access_token=" + mapboxgl.accessToken;
                    fetch(url2, {
                        method: 'GET'
                    })
                        .then(response => response.json())
                        .then(data => {
                            const zoneUrl = "https://arrownav.azurewebsites.net/trafficsurvey";
                            var TimeAdditions = [];
                            for (let i = 0; i < data.routes[0].legs[0].steps.length; i++) {
                                coords.push(data.routes[0].legs[0].steps[i].maneuver.location);
                            }
                            fetch(zoneUrl, {
                                method: 'GET',
                            })
                                .then(response => response.json())
                                .then(data2 => {
                                    //loop through the keys and save them to the list which will be used later to update the duration of the route 
                                    for (const [key, value] of Object.entries(data2)) {
                                        var zoneValue = value["item1"];
                                        var surveyCount = value["item2"];
                                        var timeAdded = zoneValue / surveyCount;
                                        if (isNaN(timeAdded)) {
                                            TimeAdditions.push([key, 0]);
                                        }
                                        else {
                                            TimeAdditions.push([key, Math.ceil(timeAdded)]);
                                        }
                                    }
                                    const zones = zonesPassed(coords);
                                    var timeAddedForRoute2 = 0;
                                    if (zones.includes("Zone1"))
                                    {
                                        timeAddedForRoute2 = timeAddedForRoute2 + TimeAdditions[0][1];

                                    }
                                    if (zones.includes("Zone2")) {
                                        timeAddedForRoute2 = timeAddedForRoute2 + TimeAdditions[1][1];

                                    }
                                    if (zones.includes("Zone3")) {
                                        timeAddedForRoute2 = timeAddedForRoute2 + TimeAdditions[2][1];
                                    }
                                    let distance = data.routes[0].distance / 1609;
                                    setSecondRouteDistance(distance.toFixed(2));
                                    setSecondRouteDuration(Math.round((data.routes[0].duration) / 60) + timeAddedForRoute2);
                                })
                                .catch((error) => {
                                    console.error('Error', error);
                                });
                            
                            map.addLayer({
                                "id": "route2",
                                "type": "line",
                                "source": {
                                    "type": "geojson",
                                    "data": {
                                        "type": "Feature",
                                        "properties": {},
                                        "geometry": {
                                            "type": 'LineString',
                                            'coordinates': coords
                                        }
                                    }
                                },
                                "layout": {
                                    "line-join": "round",
                                    "line-cap": "round"
                                },
                                "paint": {
                                    "line-color": "#ff0000",
                                    "line-width": 8,
                                    "line-opacity": 0.8
                                }
                            });
                        })
                        .catch((error) => {
                            console.error('Error:', error);
                        });
                    document.getElementById("information").style.visibility = "visible";
                    document.getElementById("first-route-info").style.visibility = "visible";
                    document.getElementById("second-route-info").style.visibility = "visible";
                });
            } else {
                console.log("can't obtain user location");
            }
        })
        const drivingBtn = document.getElementById("driving-btn");
        drivingBtn.addEventListener('click', () => {
            drivingRoute();
        })
        const cyclingBtn = document.getElementById("cycling-btn");
        cyclingBtn.addEventListener('click', () => {
            cyclingRoute();
        })

        map.on('move', () => {
            setLng(map.getCenter().lng.toFixed(4));
            setLat(map.getCenter().lat.toFixed(4));
            setZoom(map.getZoom().toFixed(2));
        });
        map.on('click', (event) => {
            endPoint.setLngLat(event.lngLat);
            endPoint.addTo(map);
            document.getElementById('button-container').style.visibility = 'visible';
        })

        // Clean up on unmount
        return () => map.remove();
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

            <div className='map-container' ref={mapContainerRef} />
        </div>
    );
}

export default Map;