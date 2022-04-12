import React from 'react';
import { useNavigate } from 'react-router-dom';


function WellnessHydrationReminder() {

    let navigate = useNavigate();

    return (
        <div className="WellnessHydrationReminder">
            <div className="header">
                <h1> Hydration Reminder </h1>
            </div>
            <div className="body">
                Stuff
            </div>
            <div className="footer">
                <button onClick={() => { navigate("/wellnesshub") }}>Return</button>
            </div>
        </div>
    )
}

export default WellnessHydrationReminder;