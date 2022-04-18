import React from 'react';
import { useNavigate } from 'react-router-dom';
import './WellnessPhysicalMedical.css';


function WellnessPhysicalMedical() {

    let navigate = useNavigate();

    return (
        <div className="WellnessPhysicalMedical">
            <div className="header">
                <h1> Medical Aid Resources </h1>
            </div>
            <div className="information-wrapper-phys-medical">
                <div className="information-wrapper-phys-medical-info">
                    <div className="CARES">
                        <h2>Campus Assessment, Response, and Evaluation for Students (CARES) Team</h2>
                        562-985-8670: studentdean@csulb.edu
                        Students who exhibit behaviors or disclose personal challenges in relation to their personal, physical, and emotional well-being should be referred to the CARES Team for connection to appropriate campus and community resources. Students exhibiting intimidating, disruptive, aggressive, or violent behaviors should also be referred to CARES, unless there is an immediate safety concern (at which time, follow up with UPD). In collaboration with students, the CARES Team will review all information available on the students’ behavior and background, to develop an individual action plan and provide on-going case management support.
                    </div>
                    <div className="physical-therapy">
                        <h2>Physical Therapy Services</h2>
                        562.985.8286: www.csulb.edu/college-of-health-human-services/pt-at-the-beach
                        A University faculty practice that educates, consults, and provides expert physical therapy evaluation and treatment of movement dysfunction to optimize health, wellness, function, and quality of life for our campus and local community
                    </div>
                    <div className="wellness-health-promotion">
                        <h2>Student Health Services Wellness and Health Promotion</h2>
                        562.985.4771: wellness@csulb.edu
                        On-campus clinic provides quality medical care for students with licensed medical providers, x-ray, lab, and pharmacy. SHS also offers case management, sexual assault advocates, health education, nutrition services, and mind-body wellness programs such as acupuncture.
                    </div>
                    <div className="get-here">
                        <h3> Get Here: </h3>
                        We are located in the Office of Wellness and Health Promotion (SHS) building Mon.-Fri., 10am-4pm.
                    </div>
                </div>
            </div>

            <div className="footer">
                <button onClick={() => { navigate("/wellnesshub/wellnesshubphysicalmain") }}>Return</button>
            </div>
        </div>
    )
}

export default WellnessPhysicalMedical;