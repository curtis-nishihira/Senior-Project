import React, { useState, useEffect } from "react"
import "./ScheduleAddClass.css"

export const ScheduleAddClass = (props) => {
    const initialFormValues = { username: props.username, course: "", coursetype: "", building: "", room: "", days: "", starttime: "", endtime: "" };
    const [classValues, setClassValues] = useState(initialFormValues);
    const [classValuesErrors, setClassValuesErrors] = useState({});
    const [isSubmit, setIsSubmit] = useState(false);


    const handleChange = (e) => {
        const { name, value } = e.target;
        setClassValues({ ...classValues, [name]: value })
    }
    const handleSubmit = (e) => {
        e.preventDefault();
        fetch('https://arrownav.azurewebsites.net/schedule/scheduleadd', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                _Username: classValues.username,
                _course: classValues.course,
                _coursetype: classValues.coursetype,
                _building: classValues.building,
                _room: classValues.room,
                _days: classValues.days,
                _starttime: classValues.starttime,
                _endtime: classValues.endtime
            }),
        })
            .then(response => response.json())
            .then(data => {
                console.log(data);
                props.handleCloser();
            })
            .catch((error) => {
                console.error('Error', error);
            });
        setClassValuesErrors(validate(classValues));
        setIsSubmit(true);
    }
    useEffect(() => {
        console.log(classValuesErrors);
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
            {/*<pre>{JSON.stringify(classValues, undefined, 2)}</pre>*/}

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
                        <input type="text" name="course" placeholder="Enter Course Here" value={classValues.course} onChange={handleChange}></input>
                    </div>
                    <div className='input-errors'>
                        <p>{classValuesErrors.course}</p>
                    </div>

                    <div className='form-inputs'>
                        <label>Course Type</label>
                        <select name="coursetype" value={classValues.coursetype} onChange={handleChange}>
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
                        <select name="building" value={classValues.building} onChange={handleChange}>
                            <option value=""></option>
                            <option value="CDC">CDC - Child Development Center</option>
                            <option value="COB">COB - College of Business</option>
                            <option value="CORP">CORP - Corporation Yard</option>
                            <option value="CPAC">CPAC - Carpenter Performance Art Center</option>
                            <option value="CPIE">CPIE - College of Professional & International Education</option>
                            <option value="DC">DC - Dance Center</option>
                            <option value="DESN">DESN - Design</option>
                            <option value="ECS">ECS - Engineering & Computer Science</option>
                            <option value="ED2">ED2 - Education 2</option>
                            <option value="EED">EED - Bob & Brabara Ellis Education Building</option>
                            <option value="EN2">EN2 - Engineering 2</option>
                            <option value="EN3">EN3 - Engineering 3</option>
                            <option value="EN4">EN4 - Engineering 4</option>
                            <option value="ET">ET - Engineering Technology</option>
                            <option value="FA1">FA1 - Fine Arts 1</option>
                            <option value="FA2">FA2 - Fine Arts 2</option>
                            <option value="FA3">FA3 - Fine Arts 3</option>
                            <option value="FA4">FA4 - Fine Arts 4</option>
                            <option value="FCS">FCS - Family & Consumer Sciences</option>
                            <option value="FND">FND - Foundation</option>
                            <option value="HC">HC - Horn Center</option>
                            <option value="HSCI">HSCI - Hall of Science</option>
                            <option value="HSD">HSD - Human Services and Design</option>
                            <option value="KIN">KIN - Kinesiology</option>
                            <option value="LA1">LA1 - Liberal Arts 1</option>
                            <option value="LA2">LA2 - Liberal Arts 2</option>
                            <option value="LA3">LA3 - Liberal Arts 3</option>
                            <option value="LA4">LA4 - Liberal Arts 4</option>
                            <option value="LA5">LA5 - Liberal Arts 5</option>
                            <option value="LAB">LAB - Language Arts</option>
                            <option value="LH">LH - Lecture Hall</option>
                            <option value="MCIB">MCIB - Macintosh Humanities Building</option>
                            <option value="MIC">MIC - Microbiology</option>
                            <option value="MLCS">MLCS - Molecular & Life Science Center</option>
                            <option value="NUR">NUR - Nursing</option>
                            <option value="PH1">PH1 - Peterson Hall 1</option>
                            <option value="PSY">PSY - Psychology</option>
                            <option value="REPR">REPR - Reprographics</option>
                            <option value="SSPA">SSPA - Social Science/Public Affairs</option>
                            <option value="TA">TA - Theatre Arts</option>
                            <option value="UMC">UMC - University Music Center</option>
                            <option value="UT">UT - University Theatre</option>
                            <option value="VEC">VEC - Vivian Engineering Center</option>
                        </select>
                    </div>
                    <div className='input-errors'>
                        <p>{classValuesErrors.building}</p>
                    </div>

                    <div className='form-inputs'>
                        <label>Room</label>
                        <input type="text" name="room" placeholder="Enter Class Room Number Here" value={classValues.room} onChange={handleChange}></input>
                    </div>
                    <div className='input-errors'>
                        <p>{classValuesErrors.room}</p>
                    </div>


                    <div className='form-inputs'>
                        <label>Days</label>
                        <input type="text" name="days" placeholder="M=Monday T=Tuesday W=Wednesday TH=Thursday F=Friday S=Saturday S=Sunday" value={classValues.days} onChange={handleChange}></input>
                    </div>
                    <div className='input-errors'>
                        <p>{classValuesErrors.days}</p>
                    </div>

                    <div className='form-inputs'>
                        <label>Select Start Time</label>
                        <input type="time" name="starttime" value={classValues.starttime} onChange={handleChange}></input>
                    </div>
                    <div className='input-errors'>
                        <p>{classValuesErrors.starttime}</p>
                    </div>

                    <div className='form-inputs'>
                        <label>Select End Time</label>
                        <input type="time" name="endtime" value={classValues.endtime} onChange={handleChange}></input>
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