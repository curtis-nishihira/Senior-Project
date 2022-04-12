import React from 'react';
import { useNavigate } from 'react-router-dom';


function WellnessPhysicalRecreation() {

    let navigate = useNavigate();

    return (
        <div className="WellnessPhysicalRecreation">
            <div className="header">
                <h1> Physical Recreation </h1>
            </div>
            <div className="body">
                Something goes here, not sure what.
            </div>
            <div className="footer">
                <button onClick={() => { navigate("/wellnesshub/wellnesshubphysicalmain") }}>Return</button>
            </div>
        </div>
    )
}

export default WellnessPhysicalRecreation;