import React from 'react';
import { useNavigate } from 'react-router-dom';
import "./WellnessHub.css";

export const WellnessHub = (props) => {

    let navigate = useNavigate();

    return (
        <div className="WellnessHub">
            <div className="wellness-container">
                <div className="header">
                    <h1>Welcome to the Wellness Hub!</h1>
                </div>
                <div className="body">
                    <button
                        onClick={() => { navigate("/wellnesshub/wellnesshubphysicalmain") }}>Physical Health</button>
                    <button
                        onClick={() => { navigate("/wellnesshub/wellnesshubmentalmain") }}>Mental Health</button>
                    <button
                        onClick={() => { navigate("/wellnesshub/wellnesshydrationreminder") }}> Setup Hydration Reminder </button>
                </div>
                <div className="footer">
                </div>
            </div>
        </div>
    );
    

}