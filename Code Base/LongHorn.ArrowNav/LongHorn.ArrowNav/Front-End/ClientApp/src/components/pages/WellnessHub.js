import React, { useState,useEffect } from 'react';
import Popup from './Popup';
import TrafficSurvey from './TrafficSurvey';
import { useLocation } from 'react-router-dom';
import "./WellnessHub.css";

export const WellnessHub = (props) => {
    const [isOpen, setIsOpen] = useState(false);
    const [message, setMessage] = useState('');
    const location = useLocation();

    const togglePopup = () => {
        setIsOpen(!isOpen);
    }

    useEffect(() => {
        if (location.state == undefined) {
            console.log("nothing");
        }
        else {
            document.getElementById('notification').style.visibility = 'visible';
            setMessage(location.state.message);
            console.log(message);
            setTimeout(() => {
                document.getElementById('notification').style.visibility = 'hidden';
            }, 7000);
        }


    });

    return (
        <>
            <div id='notification' className='notification-container' >
                {message}

            </div >
            <div>
                <input id = "button"
                    type="button"
                    value="Click to Open Popup"
                    onClick={togglePopup}
                />
                {isOpen && <Popup
                    content={<>
                        <TrafficSurvey handleCloser={togglePopup} />
                    </>}
                    handleClose={togglePopup}
                />}
            </div>
        </>
        );
    

}