import React from 'react';
import { useNavigate } from 'react-router-dom';


function WellnessHubBMAC() {

    let navigate = useNavigate();

    return (
        <div className="WellnessHubBMAC">
            <div className="header">
                <h1> BMAC Resources Available </h1>
            </div>
            <div className="body">
                Something goes here, not sure what. Is it one location?
            </div>
            <div className="footer">
                <button onClick={() => { navigate("/wellnesshub") }}>Return to Wellness Hub Main</button>
            </div>
        </div>
    )
}

export default WellnessHubBMAC;