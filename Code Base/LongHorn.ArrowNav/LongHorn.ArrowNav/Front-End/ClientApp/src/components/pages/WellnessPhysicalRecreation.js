import React from 'react';
import { useNavigate } from 'react-router-dom';
import './WellnessPhysicalRecreation.css';


function WellnessPhysicalRecreation() {

    let navigate = useNavigate();

    return (
        <div className="WellnessPhysicalRecreation">
            <div className="header">
                <h1> Physical Recreation </h1>
            </div>
            <div className="information-wrapper">
                <div className="SRWC">
                    <h2> Student Recreation and Wellness Center </h2>
                    The Student Recreation and Wellness Center (SRWC) is a 126,500-square-foot, two-story, state-of-the-art recreation facility located on the east side of the California State University, Long Beach campus. The facility is a hub for recreational activities, programs, and opportunities for Intramural Sports, Fitness, and Wellness services.

                    The SRWC is managed by Associated Students Inc. Recreation and is open to all CSULB students, associates, and affiliates. The facility contains a three-court gym, a multi activity court gym, indoor jogging track, 20,000 square feet of weight and cardio equipment, racquetball courts, activity rooms for group exercise, a custom-made rock climbing wall, a wellness center, swimming pool and spa, as well as many other services. The SRWC is LEED (Leadership in Environmental and Energy Design) certified and offers many technological advances, such as biometric hand scanners for entry, filtered water fountains, and flat screens with touch technology.
                </div>
            </div>
            <div className="footer">
                <button onClick={() => { navigate("/wellnesshub/wellnesshubphysicalmain") }}>Return</button>
            </div>
        </div>
    )
}

export default WellnessPhysicalRecreation;