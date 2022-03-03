import React, { useRef, useEffect, useState } from 'react';
// eslint-disable-next-line import/no-webpack-loader-syntax
import mapboxgl from '!mapbox-gl';
import * as MapboxGeocoder from '@mapbox/mapbox-gl-geocoder';
import './Map.css';

mapboxgl.accessToken = "pk.eyJ1IjoiYnJheWFuLWZ1ZW50ZXMyMSIsImEiOiJja3hxdW5ycWo0ZjRmMzBvNHM5ODdxZ2poIn0.MoTF9LUSyOlwGx7L-pCCjw";

export const Map = () => {
    const mapContainerRef = useRef(null);
    const [lng, setLng] = useState(-118.112437);
    const [lat, setLat] = useState(33.782105);
    const [zoom, setZoom] = useState(14.75);

    //geocoder lats and long
    const [geoLat, setGeoLat] = useState(0);
    const [geoLong, setGeoLong] = useState(0);

    //where the values for the user's lat and long will be located 
    const [userLat, setUserLat] = useState(0);
    const [userLong, setUserLong] = useState(0);

    const layersCoords = []

    const url = 'https://api.mapbox.com/directions/v5/mapbox/walking/' + userLong + ',' + userLat + ';' + geoLong + ',' + geoLat + "?waypoints=0;1&steps=true&access_token=" + mapboxgl.accessToken;

    // Initialize map when component mounts
    useEffect(() => {
        const findRoute = () => {
            console.log("user lat: " + userLat);
            console.log("user long: " + userLong);
            console.log("geo lat: " + geoLat);
            console.log("geo long: " + geoLong);
            fetch(url, {
                method: 'GET'
            })
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                    //for (let i = 0; i < data.routes[0].legs[0].steps.length; i++) {
                    //    layersCoords.push(data.routes[0].legs[0].steps[i].maneuver.location);
                    //}
                    console.log(layersCoords);
                })
                .catch((error) => {
                    console.error('Error:', error);
                });
        }

        const map = new mapboxgl.Map({
            container: mapContainerRef.current,
            style: 'mapbox://styles/brayan-fuentes21/cky54exmf1uvl14qcvixitiqo',
            center: [lng, lat],
            zoom: zoom
        });

    

        const geocoder = new MapboxGeocoder({
            accessToken: mapboxgl.accessToken,
            mapboxgl: mapboxgl,
            //limits the search to the US
            countries: 'us',
            //limits the search to CSULB specifically(orienteied as longitude, latitude)
            bbox: [-118.124185, 33.775086, -118.107233, 33.788708],

        });

        const geolocateControl = new mapboxgl.GeolocateControl({
            positionOptions: { enableHighAccuracy: true },
            showUserHeading: true
        })

        
        map.addControl(geocoder, "bottom-left");
        map.addControl(geolocateControl, 'bottom-right');


        map.on('move', () => {
            setLng(map.getCenter().lng.toFixed(4));
            setLat(map.getCenter().lat.toFixed(4));
            setZoom(map.getZoom().toFixed(2));
        });

        const routing = document.getElementById("routing");
        routing.addEventListener("click", () => {
            console.log("button clicked");
            findRoute();
        })

        //navigator is a built in js geolocation API that you can use without importing additional dependencies
        geocoder.on('result', e => {
            setGeoLong(e.result.center[0]);
            setGeoLat(e.result.center[1]);
            document.getElementById('info').style.visibility = 'visible';
            geolocateControl.trigger();
            geolocateControl.on("geolocate", () => {
                if ("geolocation" in navigator) {
                    navigator.geolocation.getCurrentPosition(position => {
                        setUserLat(position.coords.latitude);
                        setUserLong(position.coords.longitude);
                    });
                } else {
                    console.log("it ain't working fam");
                }

            })

        })

        // Clean up on unmount
        return () => map.remove();
    }, []); // eslint-disable-line react-hooks/exhaustive-deps

    return (
        <div>
            <div className="info" id="info">
                <button type="button" id="routing" className="btn btn-dark btn-lg btn-block">Routing</button>
            </div>
            <div className='map-container' ref={mapContainerRef} />
        </div>
    );
}


export default Map;