import React from 'react';
import { useNavigate } from 'react-router-dom';
import './WellnessPhysicalRecreation.css';


function WellnessPhysicalRecreation() {
    function findOnMap() {
        fetch(process.env.REACT_APP_FETCH + '/building/getBuildingbyAcronym?acronym=SRWC', {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
        })
            .then(response => response.json())
            .then(data => {
                console.log(data);
                navigate("/", { state: { building: data } });
            })
            .catch((error) => {
                console.error('Error', error);
            });
    }
    let navigate = useNavigate();

    return (
        <div className="WellnessPhysicalRecreation">
            <div className="header">
                <h1> Physical Recreation </h1>
            </div>
            <div className="information-wrapper">
                <div className="information-wrapper-info">
                    <div className="SRWC">
                        <h2> Student Recreation and Wellness Center </h2>
                        The Student Recreation and Wellness Center (SRWC) is a 126,500-square-foot, two-story, state-of-the-art recreation facility located on the east side of the California State University, Long Beach campus. The facility is a hub for recreational activities, programs, and opportunities for Intramural Sports, Fitness, and Wellness services.

                        The SRWC is managed by Associated Students Inc. Recreation and is open to all CSULB students, associates, and affiliates. The facility contains a three-court gym, a multi activity court gym, indoor jogging track, 20,000 square feet of weight and cardio equipment, racquetball courts, activity rooms for group exercise, a custom-made rock climbing wall, a wellness center, swimming pool and spa, as well as many other services. The SRWC is LEED (Leadership in Environmental and Energy Design) certified and offers many technological advances, such as biometric hand scanners for entry, filtered water fountains, and flat screens with touch technology.
                    </div>
                    <div className="getHere">
                        <h3> Get Here: </h3>
                        Located in the Student Recreation Center (SRC), Mon.-Fri., 7am-10pm | Sat., 9am-1pm | Sun., 4pm-9pm.
                    </div>
                </div>
            </div>
            <div className="footer">
                <button onClick={() => { findOnMap() }}> Find SRC </button>
                <button onClick={() => { navigate("/wellnesshub/wellnesshubphysicalmain") }}>Return</button>
            </div>
        </div>
    )
}

export default WellnessPhysicalRecreation;