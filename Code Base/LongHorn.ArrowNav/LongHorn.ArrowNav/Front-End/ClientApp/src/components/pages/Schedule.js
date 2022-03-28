import React, { useState } from "react";
import Popup from './Popup';
import ScheduleAddClass from './ScheduleAddClass'



export const Schedule = () => {
    const [isOpen, setIsOpen] = useState(false);
    const togglePopup = () => {
        setIsOpen(!isOpen);
    }

    return (

        <>
            <div className="schedule-list">
                <ul>
                    <li>CECS 491B 06 KIN 059</li>
                </ul>
                <button type="button">EDIT SCHEDULE</button>
            </div>
            <div>
                <input id="button"
                    type="button"
                    value="ADD CLASS"
                    onClick={togglePopup}
                />
                {isOpen && <Popup
                    content={<>
                        <ScheduleAddClass handleCloser={togglePopup} />
                    </>}
                    handleClose={togglePopup}
                />}
            </div>

        </>
    );

}


export default Schedule;