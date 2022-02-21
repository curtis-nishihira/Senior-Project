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

    // Initialize map when component mounts
    useEffect(() => {
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


        map.on('move', () => {
            setLng(map.getCenter().lng.toFixed(4));
            setLat(map.getCenter().lat.toFixed(4));
            setZoom(map.getZoom().toFixed(2));
        });

        // Clean up on unmount
        return () => map.remove();
    }, []); // eslint-disable-line react-hooks/exhaustive-deps

    return (
        <div>
            <div className='map-container' ref={mapContainerRef} />
        </div>
    );
}


export default Map;