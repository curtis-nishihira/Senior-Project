import React, {useState} from 'react';
import { useNavigate } from 'react-router-dom';
import './WellnessHydrationReminder.css'

function WellnessHydrationReminder() {
    async function fetchData(url, methodType, bodyData) {
        if (methodType === "GET") {
            const response = await fetch(url, {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                }
            });
            const data = await response.json();
            return data;
        }
        else if (methodType === "POST") {
            const response = await fetch(url, {
                method: methodType,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                bdy: JSON.stringify(bodyData),
            })
            const data = await response.json();
            return data;
        }
    }


    const defaultValues = {
        days: "",
        startTime: "",
        endTime: "",
        bodyWeight: "",
        repeat: "",
    };
    const [userValues, setUserValues] = useState(defaultValues);

    const [submitted, setSubmitted] = useState(false);
    const [valid, setValid] = useState(false);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setUserValues({ ...userValues, [name]: value })
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        if (userValues.bodyWeight && userValues.startTime && userValues.endTime) {
            setValid(true);
        }
        setSubmitted(true);
    }

    let navigate = useNavigate();

    return (
        <div className="WellnessHydrationReminder">
            <div className="header">
                <h1> Hydration Reminder </h1>
            </div>
            <div className="form-container">
                <form classname="register-form" onSubmit={handleSubmit}>
                    {submitted && valid ? <div className="submitted-message"> Information Submitted! </div> : null}
                    <label>Body Weight</label>
                    <input
                        onChange={handleChange}
                        value={userValues.bodyWeight}
                        className="form-field"
                        name="bodyWeight"
                        placeholder="Weight in lbs." />
                    {submitted && !userValues.bodyWeight ? <span>Weight required.</span> : null}
                    <label>First Class Start Time</label>
                    <input
                        onChange={handleChange}
                        value={userValues.startTime}
                        className="form-field"
                        name="startTime"
                        placeholder="09:30 AM" />
                    {submitted && !userValues.startTime ? <span>Time your first class starts required.</span> : null}
                    <label>Last Class End Time</label>
                    <input
                        onChange={handleChange}
                        value={userValues.endTime}
                        className="form-field"
                        name="endTime"
                        placeholder="01:45 PM" />
                    {submitted && !userValues.endTime ? <span>Time your last class starts required.</span> : null}
                    <button>Confirm</button>
                </form>

            </div>
            <div className="footer">
                <button onClick={() => { navigate("/wellnesshub") }}>Return to Main</button>
            </div>
        </div>
    )
}

export default WellnessHydrationReminder;