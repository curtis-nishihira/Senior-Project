import React from 'react';
import { useNavigate } from 'react-router-dom';

function WellnessMentalCAPS() {

    let navigate = useNavigate();

    return (
        <div className="WellnessMentalCAPS">
            <div className="header">
                <h1> CAPS Services on Campus </h1>
            </div>
            <div className="body">
                Something goes here, not sure what. Words
            </div>
            <div className="footer">
                <button onClick={() => { navigate("/wellnesshub/wellnesshubmentalmain") }}>Return</button>
            </div>
        </div>
    )
}

export default WellnessMentalCAPS;