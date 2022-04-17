import React, {useState} from 'react';
import { useNavigate } from 'react-router-dom';
import './WellnessHydrationReminder.css'

function WellnessHydrationReminder() {
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
        if (userValues.days && userValues.startTime && userValues.endTime && userValues.bodyWeight && userValues.repeat) {
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
                    <label>Days on Campus</label>
                    <input
                        onChange={handleChange}
                        value={userValues.days}
                        className="form-field"
                        name="days"
                        placeholder="M W F" />
                    {submitted && !userValues.days ? <span>Days required.</span> : null}
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
                    <label>Body Weight</label>
                    <input
                        onChange={handleChange}
                        value={userValues.bodyWeight}
                        className="form-field"
                        name="bodyWeight"
                        placeholder="Weight in lbs." />
                    {submitted && !userValues.bodyWeight ? <span>Weight required.</span> : null}
                    <label>Repeat Every...</label>
                    <input
                        onChange={handleChange}
                        value={userValues.repeat}
                        className="form-field"
                        name="repeat"
                        placeholder="30mins, 1hr, 2hrs" />
                    {submitted && !userValues.repeat ? <span>Repeat required.</span> : null}
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