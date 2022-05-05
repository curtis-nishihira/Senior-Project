import React from 'react';
import { useNavigate } from 'react-router-dom';
import './WellnessHubBMAC.css';


function WellnessHubBMAC() {

    function findOnMap() {
        fetch(process.env.REACT_APP_FETCH + '/building/getBuildingbyAcronym?acronym=SSC', {
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
        <div className="WellnessHubBMAC">
            <div className="header">
                <h1> Bob Murphy Access Center (BMAC) Resources Available </h1>
            </div>
            <div className="information-wrapper-bmac">
                <div className="information-wrapper-bmac-info">
                    <div className="altTesting">
                        <h2>Alternative Testing Accommodations</h2>
                        Accommodations for course-related examinations are arranged through BMAC.
                        Requests for face-to-face alternative testing appointments may be submitted through
                        MyBMAC. Requests for remote proctoring alternative testing appointments may
                        be submitted to BMAC-Exams@csulb.edu, and are approved on a case-by-case,
                        limited basis. Students are advised to communicate their exam scheduling needs with
                        their professors as early in the semester as possible. Additionally, students should
                        contact BMAC to obtain information regarding alternative testing accommodations
                        for University (GPE, CPT) and standardized (GRE, CBEST, CSET) exams.
                    </div>
                    <div className="supportAnimals">
                        <h2>Service & Emotional Support Animals</h2>
                        Individuals with disabilities who utilize service animals on campus grounds
                        are strongly encouraged to connect with BMAC. Emotional Support Animals
                        (ESA) are permitted on a case-by-case basis as a qualifying disability-related
                        accommodation.
                    </div>
                    <div className="noteTaking">
                        <h2>Note-Takers</h2>
                        BMAC assists in facilitating note-taking requests and services for students who
                        have difficulty taking notes due to the impact of their disability in the classroom
                        setting. BMAC offers various options for note-taking support including technology
                        and peer based solutions.
                    </div>
                    <div className="wrapper-bmac">
                        <div className="accessParking">
                            <h2>Accessible/Medical Parking</h2>
                            BMAC can provide temporary accessible parking permits with medical verification.
                            Students who already have a disabled placard or license plate must also
                            have a paid parking permit in order to park on campus.
                        </div>
                        <div className="otherServices">
                            <h2>Other Services</h2>
                            Other services include mobility assistance, tutorial support, assistive technology,
                            accessible materials and furniture, and other support necessary to accommodate
                            a student’s specific disability.
                        </div>
                        <div className="get-here">
                            <h3> Get Here: </h3>
                            Bob Murphy Access Center Location: SSC building, room 110 ; Phone: (562) 985-5401 ; E-mail: bmac@csulb.edu. Mon.-Fri., 10am-4pm.
                        </div>
                    </div>
                </div>
            </div>
            <div className="footer">
                <button onClick={() => { findOnMap() }}> Find BMAC </button>
                <button onClick={() => { navigate("/wellnesshub") }}>Return to Wellness Hub Main</button>
            </div>
        </div>
    )
}

export default WellnessHubBMAC;