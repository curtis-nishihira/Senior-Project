import React, { useEffect, useRef, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './WellnessHydrationReminder.css'

function WellnessHydrationReminder() {
    const [varTracker, setTracker] = useState(false);
    const beginTime = useRef("");
    const finalTime = useRef("");
    const email = getEmailFromCookies();
    
    const isUpdated = useRef(false);

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

    function startTimer(duration, display, waterIntake) {
        var timer = duration, minutes, seconds;
        const interval = setInterval(function () {
            var breakout = isUpdated.current;
            minutes = parseInt(timer / 60, 10);
            seconds = parseInt(timer % 60, 10);

            minutes = minutes < 10 ? "0" + minutes : minutes;
            seconds = seconds < 10 ? "0" + seconds : seconds;

            display.textContent = minutes + ":" + seconds;
            if (breakout) {
                clearInterval(interval);
                isUpdated.current = false;
            }
            if (--timer < 0) {
                timer = duration;
            }
            if (minutes == 0 && seconds == 0) {
                alert("Remember to drink:" + waterIntake + "oz");
                toggleTracker();
            }
        }, 1000);
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
        bodyWeight: 0,
    };
    const [userValues, setUserValues] = useState(defaultValues);

    const [submitted, setSubmitted] = useState(false);
    const [valid, setValid] = useState(false);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setUserValues({ ...userValues, [name]: value })
    }
    useEffect(async () => {
        var doesExist = await fetchData(process.env.REACT_APP_FETCH + "/wellness/getReminder?Username=" + email, 'GET', []);
        if (doesExist) {
            var startTime = await fetchData(process.env.REACT_APP_FETCH + '/wellness/getStartTime?Username=' + email, 'GET', []);
            var endTime = await fetchData(process.env.REACT_APP_FETCH + '/wellness/getEndTime?Username=' + email, 'GET', []);
            var waterIntake = await fetchData(process.env.REACT_APP_FETCH + '/wellness/getWaterIntake?Username=' + email, 'GET', []);
            beginTime.current = startTime;
            finalTime.current = endTime;
            var startHour = parseInt(startTime.substring(0, 1));
            var endHour = parseInt(endTime.substring(0, 1));

            if (startHour == 0) {
                startHour = parseInt(startTime.substring(1, 2));
            }
            else {
                startHour = parseInt(startTime.substring(0, 2));
            }
            if (endHour == 0) {
                endHour = parseInt(endTime.substring(1, 2));
            }
            else {
                endHour = parseInt(endTime.substring(0, 2));
            }
            
            if (endTime.includes("PM")) {
                endHour += 12;
            }
            if (startTime.includes("PM")) {
                startTime += 12;
            }

            if (waterIntake != null && startTime != null && endTime != null) {
                document.getElementById("water-intake").innerHTML = waterIntake;
                document.getElementById("start-time").innerHTML = startTime;
                document.getElementById("end-time").innerHTML = endTime;
            }
            const today = new Date();

            const timeMins = today.getMinutes();

            const seconds = today.getSeconds();

            const timeHrs = today.getHours();

            const timeRemInSecs = ((60 - timeMins) * 60) + (60 - seconds);

            const getStr = (60 - timeMins) + ":" + (60 - seconds);

            if (startHour <= timeHrs && endHour > timeHrs) {
                document.getElementById('time').innerHTML = getStr;
                document.getElementById("time-container").style.visibility = 'visible';
                startTimer(timeRemInSecs, document.querySelector('#time'), waterIntake)
            }
        }

        
    }, [varTracker]);

   

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (userValues.bodyWeight && userValues.startTime && userValues.endTime) {
            setValid(true);
            var waterIntake = userValues.bodyWeight * 0.60;
            fetch(process.env.REACT_APP_FETCH + '/wellness/setForm', {
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
                })
                .catch((error) => {
                    console.error('Error', error);
                });
        }
        isUpdated.current = true;
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
                    <div className="timerRemind" id="time-container">Next Reminder in: <span id="time"></span> minutes!</div>
                    Reminder for hydration from: <span id="start-time"></span> - <span id="end-time"></span><br></br>
                    Water Intake per day: <span id="water-intake"></span> ozs
                </div>
            </div>

            <div className="footer">
                <button onClick={() => { navigate("/wellnesshub") }}>Return to Main</button>
            </div>
        </div>
    )
}

export default WellnessHydrationReminder;