import React from 'react';
import { useNavigate } from 'react-router-dom';
import './WellnessMentalCAPS.css';

function WellnessMentalCAPS() {
    function findOnMap() {
        fetch(process.env.REACT_APP_FETCH + '/building/getBuildingbyAcronym?acronym=BH', {
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
        <div className="WellnessMentalCAPS">
            <div className="header">
                <h1> CAPS Services on Campus </h1>
            </div>
            <div className="information-wrapper-ment-caps">
                <div className="information-wrapper-ment-caps-info">
                    <div className="CAPS">
                        <h2> Counseling and Psychological Services (CAPS) </h2>
                        Counseling and Psychological Services (CAPS) helps students meet the personal challenges associated with identifying and accomplishing academic, career, and life goals. Our services include short-term counseling for individuals, group counseling, career development counseling, referral services, psychoeducational workshops, and crisis intervention. Counseling is provided by mental health professionals and by advanced doctoral psychology interns under the supervision of licensed psychologists. CAPS welcomes students of all backgrounds, value systems, and lifestyles.
                    </div>
                    <div className="getHere">
                        <h3> Get Here: </h3>
                        We are located in Brotman Hall, Room 226 Mon.-Fri., 8am-5pm.
                    </div>
                </div>
            </div>
            <div className="footer">
                <button onClick={() => { findOnMap() }}> Find CAPS </button>
                <button onClick={() => { navigate("/wellnesshub/wellnesshubmentalmain") }}>Return</button>
            </div>
        </div>
    )
}

export default WellnessMentalCAPS;