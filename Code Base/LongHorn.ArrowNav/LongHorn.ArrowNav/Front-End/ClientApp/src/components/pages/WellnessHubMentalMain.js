import React from 'react';
import { useNavigate } from 'react-router-dom';


function WellnessHubMentalMain() {

    let navigate = useNavigate();

    return (
        <div className="WellnessHubMentalMain">
            <div className="header">
                <h1> Mental Health Resources Available on Campus </h1>
            </div>
            <div className="body">
                <button
                    onClick={() => { navigate("/wellnesshub/wellnesshubmentalmain/wellnessmentalcaps") }}> CAPS services </button>
                <button
                    onClick={() => { navigate("/wellnesshub/wellnesshubbmac") }}> BMAC for Mental Imparement </button>
            </div>
            <div className="footer">
                <button onClick={() => { navigate("/wellnesshub") }}>Return</button>
            </div>
        </div>
    )
}

export default WellnessHubMentalMain;