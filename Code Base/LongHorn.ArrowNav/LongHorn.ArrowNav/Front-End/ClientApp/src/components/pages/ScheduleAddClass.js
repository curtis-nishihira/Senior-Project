import { error } from "jquery";
import React, { useState, useEffect } from "react"
import "./ScheduleAddClass.css"

export const ScheduleAddClass = (props) => {
    const initialFormValues = { username: props.username, course: "", coursetype: "", building: "", room: "", days: "", starttime: "", endtime: "" };
    const [classValues, setClassValues] = useState(initialFormValues);
    const [classValuesErrors, setClassValuesErrors] = useState({});
    const [isSubmit, setIsSubmit] = useState(false);

    async function fetchData(url, methodType, bodyData) {
        if (methodType === "GET") {
            const response = await fetch(url, {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                }
            })
            const data = await response.json();
            return data;
        }
        else if (methodType === "POST" && bodyData.length != 0) {
            const response = await fetch(url, {
                method: methodType,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(bodyData),
            })
            const data = await response.json();
            return data;
        }

    }


    async function fillBuildingsOption() {
        var fillBoxUrl = process.env.REACT_APP_FETCH + '/building/getAllBuildings';
        var x = await fetchData(fillBoxUrl, "GET", []);
        var listOfBuildings = x;
        var sel = document.getElementById('buildings-list');
        for (var i = 0; i < listOfBuildings.length; i++) {
            var opt = document.createElement('option');
            opt.innerHTML = listOfBuildings[i];
            opt.textContent = listOfBuildings[i];
            opt.value = listOfBuildings[i];
            sel.appendChild(opt);
        }

    }
    useEffect(() => {
        fillBuildingsOption();
        return function cleanup() {
            var select = document.getElementById('buildings-list');
            if (select != null) {
                select.innerHTML = '';
            }
        }
    },[])
    const handleChange = (e) => {
        const { name, value } = e.target;
        setClassValues({ ...classValues, [name]: value })
    }
    const handleSubmit = async (e) => {
        e.preventDefault();
        var buildingAcronym;
        var url = process.env.REACT_APP_FETCH + '/building/getAcronymbyBuildingName?BuildingName=' + classValues.building;
        
        try {
            buildingAcronym = await fetchData(url, "GET", []);
        }
        catch(e)
        {
            console.log(e);
        }
        var payload = {
            _Username: classValues.username,
            _course: classValues.course,
            _coursetype: classValues.coursetype,
            _building: buildingAcronym,
            _room: classValues.room,
            _days: classValues.days,
            _starttime: classValues.starttime,
            _endtime: classValues.endtime
        };
        var sentData = await fetchData(process.env.REACT_APP_FETCH + '/schedule/scheduleadd', "POST", payload)
        props.handleCloser();
        setClassValuesErrors(validate(classValues));
        setIsSubmit(true);
    }
    useEffect(() => {
        if (Object.keys(classValuesErrors).length === 0 && isSubmit) {
            console.log(classValues);
        }
    }, [classValuesErrors])
    const validate = (values) => {
        const errors = {}
        if (!values.username) {
            errors.username = "Email is Required"
        }
        if (!values.course) {
            errors.course = "Course is Required";
        }

        if (!values.coursetype) {
            errors.coursetype = "Course Type is Required";
        }

        if (!values.building) {
            errors.building = "Class Building is Required";
        }

        if (!values.room) {
            errors.room = "Class Room Number is Required";
        }

        if (!values.days) {
            errors.days = "Class Days are Required";
        }


        if (!values.starttime) {
            errors.starttime = "Class Start time is required";
        }
        else if (values.starttime < "06:00") {
            errors.starttime = "Choose a time after 6:00 AM";
        }
        else if (values.starttime > "22:00") {
            errors.starttime = "Choose a time before 10:00 PM"
        }

        if (!values.endtime) {
            errors.endtime = "Class End time is required";
        }
        else if (values.endtime === values.starttime) {
            errors.endtime = "Class End time can not be the same as the start time";
        }
        else if (values.endtime < values.starttime) {
            errors.endtime = "Class End time can not be before the Class Start time";
        }
        else if (values.endtime < "06:00") {
            errors.endtime = "Choose a time after 6:00 AM";
        }
        else if (values.endtime > "22:00") {
            errors.endtime = "Choose a time before 10:00 PM"
        }
        return errors;
    }

    return (
        <div className="addclass-container">
            {Object.keys(classValuesErrors).length === 0 && isSubmit ? (<div className="addclass-message-success"> Class Added Successfully </div>) : (<div className="addclass-message-fail">Fill Out the Required Fields to Add Class to Schedule</div>)}
            

            <form onSubmit={handleSubmit}>
                <h1>ENTER CLASS INFORMATION</h1>
                <div className='addclass-form'>
                    <div className='form-inputs'>
                        <label>Email</label>
                        <input type="email" disabled="disabled" name="username" placeholder="Enter email Here" value={classValues.username} onChange={handleChange}></input>
                    </div>
                    <div className='input-errors'>
                        <p>{classValuesErrors.username}</p>
                    </div>

                    <div className='form-inputs'>
                        <label>Course</label>
                        <input type="text" name="course" placeholder="Enter Course Here" required value={classValues.course} onChange={handleChange}></input>
                    </div>
                    <div className='input-errors'>
                        <p>{classValuesErrors.course}</p>
                    </div>

                    <div className='form-inputs'>
                        <label>Course Type</label>
                        <select name="coursetype" required value={classValues.coursetype} onChange={handleChange}>
                            <option value=""></option>
                            <option value="Seminar">SEMINAR</option>
                            <option value="Lab">LAB</option>
                        </select>
                    </div>
                    <div className='input-errors'>
                        <p>{classValuesErrors.coursetype}</p>
                    </div>


                    <div className='form-inputs'>
                        <label>Select Building</label>
                        <select name="building" required value={classValues.building} onChange={handleChange} id='buildings-list'>
                            
                        </select>
                    </div>
                    <div className='input-errors'>
                        <p>{classValuesErrors.building}</p>
                    </div>

                    <div className='form-inputs'>
                        <label>Room</label>
                        <input type="text" name="room" placeholder="Enter Class Room Number Here" required value={classValues.room} onChange={handleChange}></input>
                    </div>
                    <div className='input-errors'>
                        <p>{classValuesErrors.room}</p>
                    </div>


                    <div className='form-inputs'>
                        <label>Days</label>
                        <select name="days" required value={classValues.days} onChange={handleChange}>
                            <option value=""></option>
                            <option value="M W">MONDAY + WEDNESDAY</option>
                            <option value="T H">TUESDAY + THURSDAY</option>
                            <option value="M W F">MONDAY + WEDNESDAY + FRIDAY</option>
                            <option value="M">MONDAY</option>
                            <option value="T">TUESDAY</option>
                            <option value="W">WEDNESDAY</option>
                            <option value="H">THURSDAY</option>
                            <option value="F">FRIDAY</option>
                            <option value="S">SATURDAY</option>
                            <option value="U">SUNDAY</option>
                            <option value="M T W H F">WEEKDAYS</option>
                            <option value="M T W H F S U">EVERYDAY</option>
                        </select>
                    </div>
                    <div className='input-errors'>
                        <p>{classValuesErrors.days}</p>
                    </div>

                    <div className='form-inputs'>
                        <label>Select Start Time</label>
                        <input type="time" name="starttime" required value={classValues.starttime} onChange={handleChange}></input>
                    </div>
                    <div className='input-errors'>
                        <p>{classValuesErrors.starttime}</p>
                    </div>

                    <div className='form-inputs'>
                        <label>Select End Time</label>
                        <input type="time" name="endtime" required value={classValues.endtime} onChange={handleChange}></input>
                    </div>
                    <div className='input-errors'>
                        <p>{classValuesErrors.endtime}</p>
                    </div>

                    <button type="submit">SUBMIT</button>
                </div>
            </form>
        </div>
    );
}

export default ScheduleAddClass