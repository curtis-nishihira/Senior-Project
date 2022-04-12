import React from 'react';
import { useNavigate } from 'react-router-dom';


function WellnessPhysicalMedical() {

    let navigate = useNavigate();

    return (
        <div className="WellnessPhysicalMedical">
            <div className="header">
                <h1> Medical Aid Locations on Campus </h1>
            </div>
            <div className="body">
                Something goes here, not sure what. List?
            </div>
            <div className="footer">
                <button onClick={() => { navigate("/wellnesshub/wellnesshubphysicalmain") }}>Return</button>
            </div>
        </div>
    )
}

export default WellnessPhysicalMedical;