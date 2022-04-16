import React, { useState, useEffect } from "react"
//import "./ScheduleAddClass.css"

export const ScheduleEditClass = (props) => {
    const initialFormValues = { username: props.username, course: props.course, coursetype: props.courseType, building: props.building, room: props.room, days: props.days, starttime: props.startTime, endtime: props.endTime };
    const [classValues, setClassValues] = useState(initialFormValues);
    const [classValuesErrors, setClassValuesErrors] = useState({});
    const [isSubmit, setIsSubmit] = useState(false);

    async function fetchData(url, methodType, bodyData) {
        if (methodType === "GET") {
            const response = await fetch(url);
            const data = await response.json();
            return data;
        }
        else if (methodType === "POST") {
            const response = await fetch(url, { method: methodType })
            const data = await response.json();
            return data;
        }

    }


    async function fillBuildingsOption() {
        var fillBoxUrl = process.env.REACT_APP_FETCH + '/building/getAllBuildings';
        var x = await fetchData(fillBoxUrl, "GET", []);
        var listOfBuildings = x;
        var sel = document.getElementById('buildings');
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
    })


    const handleChange = (e) => {
        const { name, value } = e.target;
        setClassValues({ ...classValues, [name]: value })
    }
    const handleSubmit = async(e) => {
        e.preventDefault();
        var buildingAcronym;
        var buildingcontrollerurl = process.env.REACT_APP_FETCH + '/building/getAcronymbyBuildingName?BuildingName=' + classValues.building;

        try {
            buildingAcronym = await fetchData(buildingcontrollerurl, 'GET', []);
        }
        catch
        {
            buildingAcronym = "NVM";
        }
        fetch(process.env.REACT_APP_FETCH +'/schedule/scheduleedit', {
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
            {Object.keys(classValuesErrors).length === 0 && isSubmit ? (<div className="addclass-message-success"> Class Edited Successfully </div>) : (<div className="addclass-message-fail">Edit Class</div>)}
            {/*<pre>{JSON.stringify(classValues, undefined, 2)}</pre>*/}

            <form onSubmit={handleSubmit}>
                <h1>ENTER CLASS INFORMATION</h1>
                <div className='addclass-form'>
                    <div className='form-inputs'>
                        <label>Email</label>
                        <input type="email" disabled = "disabled" name="username" placeholder="Enter email Here" required value={classValues.username} onChange={handleChange}></input>
                    </div>
                    <div className='input-errors'>
                        <p>{classValuesErrors.username}</p>
                    </div>

                    <div className='form-inputs'>
                        <label>Course</label>
                        <input type="text" name="course" disabled="disabled" placeholder="Enter Course Here" required value={classValues.course} onChange={handleChange}></input>
                    </div>
                    <div className='input-errors'>
                        <p>{classValuesErrors.course}</p>
                    </div>

                    <div className='form-inputs'>
                        <label>Course Type</label>
                        <select name="coursetype" disabled="disabled" required value={classValues.coursetype} onChange={handleChange}>
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
                        <select name="building" required value={classValues.building} onChange={handleChange} >
                            <option value="AS">ACADEMIC SERVICES</option>
                            <option value="ANNEX">ART ANNEX</option>
                            <option value="BAC">BARRETT ATHLETIC ADMINISTRATION BUILDING</option>
                            <option value="BKS">BOOKSTORE</option>
                            <option value="BH">BROTMAN HALL</option>
                            <option value="COB">COLLEGE OF BUSINESS</option>
                            <option value="CAFÉ">CAFETERIA</option>
                            <option value="CDC">CHILD DEVELOPMENT CENTER</option>
                            <option value="CLA">COLLEGE OF LIBERAL ARTS ADMINISTRATION</option>
                            <option value="CPIE">COLLEGE OF PROFESSIONAL AND INTERNATIONAL EDUCATION</option>
                            <option value="CPAC">CARPENTER PERFORMING ARTS CENTER</option>
                            <option value="CP">CENTRAL PLANT</option>
                            <option value="CORP">CORPORATION YARD</option>
                            <option value="DC">DANCE CENTER</option>
                            <option value="DESN">DESIGN</option>
                            <option value="ED2">EDUCATION 2</option>
                            <option value="EED">BOB AND BARBARA ELLIS EDUCATION BUILDING</option>
                            <option value="EN2">ENGINEERING 2</option>
                            <option value="EN3">ENGINEERING 3</option>
                            <option value="EN4">ENGINEERING 4</option>
                            <option value="ECS">ENGINEERING AND COMPUTER SCIENCE</option>
                            <option value="ET">ENGINEERING TECHNOLOGY</option>
                            <option value="FM">FACILITIES MANAGEMENT</option>
                            <option value="FO2">FACULTY OFFICE 2</option>
                            <option value="FO3">FACULTY OFFICE 3</option>
                            <option value="FO4">FACULTY OFFICE 4</option>
                            <option value="FO5">FACULTY OFFICE 5</option>
                            <option value="FCS">FAMILY AND CONSUMER SCIENCES</option>
                            <option value="FA1">FINE ARTS 1</option>
                            <option value="FA2">FINE ARTS 2</option>
                            <option value="FA3">FINE ARTS 3</option>
                            <option value="FA4">FINE ARTS 4</option>
                            <option value="FND">FOUNDATION</option>
                            <option value="HSCI">HALL OF SCIENCE</option>
                            <option value="HHS1">HEALTH & HUMAN SERVICES 1</option>
                            <option value="HHS2">HEALTH & HUMAN SERVICES 2</option>
                            <option value="HSC">HILLSIDE SERVICE CENTER</option>
                            <option value="HC">HORN CENTER</option>
                            <option value="HRL">HOUSING & RESIDENTIAL LIFE OFFICE</option>
                            <option value="HSD">HUMAN SERVICES & DESIGN</option>
                            <option value="IH">INTERNATIONAL HOUSE</option>
                            <option value="JG">JAPANESE GARDEN</option>
                            <option value="KCAM">KLEEFELD CONTEMPORARY ART MUSEUM</option>
                            <option value="KIN">KINESIOLOGY</option>
                            <option value="LAB">LANGUAGE ARTS</option>
                            <option value="LH">LECTURE HALL 150-151</option>
                            <option value="LA1">LIBERAL ARTS 1</option>
                            <option value="LA2">LIBERAL ARTS 2</option>
                            <option value="LA3">LIBERAL ARTS 3</option>
                            <option value="LA4">LIBERAL ARTS 4</option>
                            <option value="LA5">LIBERAL ARTS 5</option>
                            <option value="LIB">LIBRARY</option>
                            <option value="LAH">LOS ALAMITOS HALL</option>
                            <option value="LCH">LOS CERRITOS HALL</option>
                            <option value="MHB">MCINTOSH HUMANITIES BLDG</option>
                            <option value="MIC">MICROBIOLOGY</option>
                            <option value="MLSC">MOLECULAR & LIFE SCIENCES CENTER</option>
                            <option value="MMC">MULTIMEDIA CENTER</option>
                            <option value="NUR">NURSING</option>
                            <option value="OP">OUTPOST</option>
                            <option value="PTS">PARKING & TRANSPORTATION SERVICES</option>
                            <option value="PSC">PARKSIDE COLLEGE</option>
                            <option value="PH1">PETERSON HALL 1</option>
                            <option value="PSY">PSYCHOLOGY</option>
                            <option value="PYR">PYRAMID</option>
                            <option value="RC">RECYCLING CENTER</option>
                            <option value="REPR">REPROGRAPHICS</option>
                            <option value="SSPA">SOCIAL SCIENCE/PUBLIC AFFAIRS</option>
                            <option value="SHS">SOCIAL SCIENCE/PUBLIC AFFAIRS</option>
                            <option value="SRWC">STUDENT RECREATION & WELLNESS CENTER</option>
                            <option value="SSC">STUDENT SUCCESS CENTER</option>
                            <option value="SSCH">SOCCER AND SOFTBALL CLUBHOUSE</option>
                            <option value="TA">THEATRE ARTS</option>
                            <option value="UMC">UNIVERSITY MUSIC CENTER</option>
                            <option value="UP">UNIVERSITY POLICE BLDG</option>
                            <option value="USU">UNIVERSITY STUDENT UNION</option>
                            <option value="UTC">UNIVERSITY TELECOMMUNICATIONS CENTER</option>
                            <option value="UT">UNIVERSITY THEATRE</option>
                            <option value="VIC">VISITOR INFORMATION CENTER</option>
                            <option value="VEC">VIVIAN ENGINEERING CENTER</option>

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
                            <option value="T TH">TUESDAY + THURSDAY</option>
                            <option value="M W F">MONDAY + WEDNESDAY + FRIDAY</option>
                            <option value="M">MONDAY</option>
                            <option value="T">TUESDAY</option>
                            <option value="W">WEDNESDAY</option>
                            <option value="TH">THURSDAY</option>
                            <option value="F">FRIDAY</option>
                            <option value="S">SATURDAY</option>
                            <option value="SU">SUNDAY</option>
                            <option value="M T W TH F">WEEKDAYS</option>
                            <option value="M T W TH F S SU">EVERYDAY</option>
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

export default ScheduleEditClass