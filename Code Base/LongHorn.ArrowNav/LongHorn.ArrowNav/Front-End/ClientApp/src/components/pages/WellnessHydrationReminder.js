﻿import { get } from 'jquery';
import React, {useEffect,useState} from 'react';
import { useNavigate } from 'react-router-dom';
import './WellnessHydrationReminder.css'

function WellnessHydrationReminder() {
    const [varTracker, setTracker] = useState(false);
    const [beginTime, setBeginTimer] = useState("");
    const [finalTime, setFinalTimer] = useState("");
    const [totalReminders, setTotalReminders] = useState(0);
    const email = getEmailFromCookies();

    async function fetchData(url, methodType, bodyData) {
        if (methodType === "GET") {
            const response = await fetch(url, {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
            });
            const data = await response.json();
            return data;
        }
        else if (methodType === "POST") {
            const response = await fetch(url, { method: methodType })
            const data = await response.json();
            return data;
        }

    }

    function toggleTracker() {
        setTracker(!varTracker);
    }
    function getEmailFromCookies() {
        var decodedCookies = decodeURIComponent(document.cookie);
        var listOfCookies = decodedCookies.split("; ");
        for (var i = 0; i < listOfCookies.length; i++) {
            let temp = listOfCookies[i].split("=");
            if (temp[0] == process.env.REACT_APP_COOKIE_KEY) {
                let cookieSplit = listOfCookies[i].split('"');
                let name = cookieSplit[3];
                let newName = name.replace("'", "");
                return newName;
            }
        }
    }
    const defaultValues = {
        startTime: "",
        endTime: "",
        bodyWeight: "",
    };
    const [userValues, setUserValues] = useState(defaultValues);

    const [submitted, setSubmitted] = useState(false);
    const [valid, setValid] = useState(false);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setUserValues({ ...userValues, [name]: value })
    }

    useEffect(() => {
        updateReminderBox();
        console.log(beginTime);
    }, [varTracker])

    async function updateReminderBox() {
        var startTime = await fetchData('https://localhost:44465/wellness/getStartTime?Username=' + email, 'GET', []);
        var endTime = await fetchData('https://localhost:44465/wellness/getEndTime?Username=' + email, 'GET', []);
        setBeginTimer(beginTime);
        setFinalTimer(finalTime);
        console.log(startTime, endTime);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (userValues.bodyWeight && userValues.startTime && userValues.endTime) {
            setValid(true);
            var waterIntake = userValues.bodyWeight * 0.60;
            fetch('https://localhost:44465/wellness/setForm', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    _Username: email,
                    _bodyWeight: parseInt(userValues.bodyWeight),
                    _startTime: userValues.startTime,
                    _endTime: userValues.endTime,
                    _waterIntake: waterIntake
                }),
            })
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                })
                .catch((error) => {
                    console.error('Error', error);
                });
        }
        setSubmitted(true);

        toggleTracker();
    }



    let navigate = useNavigate();

    return (
        <div className="WellnessHydrationReminder">
            <div className="header">
                <h1> Hydration Reminder </h1>
            </div>
            <div className="form-container">
                <form className="register-form" onSubmit={handleSubmit}>
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
            <div className="hydrationReminderList">
                <div className="header">
                    <h2>Reminders</h2>
                </div>
                <div className="box">
                    Reminder for hydration at: {beginTime} {finalTime} { totalReminders}
                </div>
            </div>

            <div className="footer">
                <button onClick={() => { navigate("/wellnesshub") }}>Return to Main</button>
            </div>
        </div>
    )
}

export default WellnessHydrationReminder;