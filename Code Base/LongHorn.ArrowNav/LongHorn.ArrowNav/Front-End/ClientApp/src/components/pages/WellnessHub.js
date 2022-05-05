import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Popup from "./Popup.js";
import "./WellnessHub.css";

export const WellnessHub = (props) => {
    
    let navigate = useNavigate();

    function checkEmailFromCookies() {
        var bool = false;
        var decodedCookies = decodeURIComponent(document.cookie);
        var listOfCookies = decodedCookies.split("; ");

        for (var i = 0; i < listOfCookies.length; i++) {
            let temp = listOfCookies[i].split("=");
            if (temp[0] == process.env.REACT_APP_COOKIE_KEY) {
                bool = true;
                break;
            }
        }
        if (bool === true) {
            navigate("/wellnesshydrationreminder");
        }
        else {
            navigate("/account");
        }
    }

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
                        onClick={checkEmailFromCookies}> Setup Hydration Reminder </button>
                </div>
                <div className="footer">
                </div>
            </div>
        </div>
    );
    

}