import React, { useState } from 'react';
import Popup from './Popup';
import TrafficSurvey from './TrafficSurvey'

export const WellnessHub = (props) => {
    const [isOpen, setIsOpen] = useState(false);

    const togglePopup = () => {
        setIsOpen(!isOpen);
    }

    return <div>
        <input
            type="button"
            value="Click to Open Popup"
            onClick={togglePopup}
        />
        {isOpen && <Popup
            content={<>
                <TrafficSurvey/>
            </>}
            handleClose={togglePopup}
        />}
    </div>
}