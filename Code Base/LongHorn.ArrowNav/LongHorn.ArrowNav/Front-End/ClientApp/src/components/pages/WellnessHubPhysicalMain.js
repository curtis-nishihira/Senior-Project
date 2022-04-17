import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';


function WellnessHubPhysicalMain() {

    let navigate = useNavigate();

    return (
        <div className="WellnessHubPhysicalMain">
            <div className="header">
                <h1> Physical Health Resources Available on Campus </h1>
            </div>
            <div className="body">
                <button
                    onClick={() => { navigate("/wellnesshub/wellnesshubphysicalmain/wellnessphysicalrecreation") }}> Physical Recreation </button>
                <button
                    onClick={() => { navigate("/wellnesshub/wellnesshubphysicalmain/wellnessphysicalmedical") }}> Medical Aid </button>
                <button
                    onClick={() => { navigate("/wellnesshub/wellnesshubbmac") }}> BMAC for Imparement Services </button>
            </div>
            <div className="footer">
                <button onClick={() => { navigate("/wellnesshub") }}>Return</button>
            </div>
        </div>
    )
}

export default WellnessHubPhysicalMain;