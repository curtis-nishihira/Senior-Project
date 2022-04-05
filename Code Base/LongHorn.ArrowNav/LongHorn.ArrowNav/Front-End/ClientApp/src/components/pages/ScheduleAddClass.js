import React, { useState, useEffect } from "react"

export const ScheduleAddClass = () => {
    const initialFormValues = { subject: "", course: "", section: "", building: "CDC", room: "", day: "", secondday:"", starttime:"", endtime:"" };
    const [classValues, setClassValues] = useState(initialFormValues);
    const [classValuesErrors, setClassValuesErrors] = useState({});
    const [isSubmit, setIsSubmit] = useState(false);



    const handleChange = (e) => {
        const { name, value } = e.target;
        setClassValues({ ...classValues, [name]: value })
    }
    const handleSubmit = (e) => {
        e.preventDefault();
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
        if (!values.subject.trim()) {
            errors.subject = "Class Subject is Required";
        }
        else if (values.subject.length < 2) {
            errors.subject = 'Class Subject Abbreviation needs to be 2 characters or more';
        }
        else if (values.subject.length > 5) {
            errors.subject = 'Class Subject Abbreviation can not exceed 5 characters';
        }

        if (!values.course) {
            errors.course = "Class Course Number is Required";
        }

        if (!values.section) {
            errors.section = "Class Section Number is Required";
        }

        if (!values.building) {
            errors.building = "Class Building is Required";
        }

        if (!values.room) {
            errors.room = "Class Room Number is Required";
        }

        if (values.secondday === values.day) {
            errors.secondday = "Class Days must be different. Second Day must be a different day or empty.";
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
            <pre>{JSON.stringify(classValues, undefined, 2)}</pre>
            
            <form onSubmit={ handleSubmit}>
                <h1>ENTER CLASS INFORMATION</h1>
                <div className='addclass-form'>
                    <div className='form-inputs'>
                        <label>Subject</label>
                            <input type="text" name="subject" placeholder="Enter Class Subject Here" value={classValues.subject} onChange={ handleChange}></input>
                    </div>
                    <p>{classValuesErrors.subject}</p>

                    <div className='form-inputs'>
                        <label>Course</label>
                        <input type="text" name="course" placeholder="Enter Class Course Number Here" value={classValues.course} onChange={handleChange}></input>
                    </div>
                    <p>{classValuesErrors.course}</p>

                    <div className='form-inputs'>
                        <label>Section</label>
                        <input type="number" name="section" placeholder="Enter Class Section Number Here" value={classValues.section} onChange={handleChange}></input>
                    </div>
                    <p>{classValuesErrors.section}</p>
                    //lat long
                    <div className='form-inputs'>
                        <label>Select Building</label>
                        <select name="building" value={classValues.building} onChange={handleChange}>
                            <option value="33.78840102644194, -118.12053225184958">CDC - Child Development Center</option>
                            <option value="33.784233240788986, -118.11593318957405">COB - College of Business</option>
                            <option value="CORP">CORP - Corporation Yard</option>
                            <option value="CPAC">CPAC - Carpenter Performance Art Center</option>
                            <option value="CPIE">CPIE - College of Professional & International Education</option>
                            <option value="DC">DC - Dance Center</option>
                            <option value="DESN">DESN - Design</option>
                            <option value="33.783386537699705, -118.11014219165916">ECS - Engineering & Computer Science</option>
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
                            <option value="33.778856037632266, -118.11211372390811">PH1 - Peterson Hall 1</option>
                            <option value="PSY">PSY - Psychology</option>
                            <option value="REPR">REPR - Reprographics</option>
                            <option value="SSPA">SSPA - Social Science/Public Affairs</option>
                            <option value="TA">TA - Theatre Arts</option>
                            <option value="UMC">UMC - University Music Center</option>
                            <option value="UT">UT - University Theatre</option>
                            <option value="33.78301158414412, -118.11045075430798">VEC - Vivian Engineering Center</option>
                        </select>
                    </div>
                    <p>{classValuesErrors.building}</p>

                    <div className='form-inputs'>
                        <label>Room</label>
                        <input type="text" name="room" placeholder="Enter Class Room Number Here" value={classValues.room} onChange={handleChange}></input>
                    </div>
                    <p>{classValuesErrors.room}</p>

                    
                    <div className='form-inputs'>
                        <label>Select Class Days</label>
                        <select name="day" value={classValues.day} onChange={handleChange}>
                            <option value="Monday">Monday</option>
                            <option value="Tuesday">Tuesday</option>
                            <option value="Wednesday">Wednesday</option>
                            <option value="Thursday">Thursday</option>
                            <option value="Friday">Friday</option>
                            <option value="Saturday">Saturday</option>
                            <option value="Sunday">Sunday</option>
                        </select>
 
                        <select name="secondday" value={classValues.secondday} onChange={handleChange}>
                            <option value="">None</option>
                            <option value="Monday">Monday</option>
                            <option value="Tuesday">Tuesday</option>
                            <option value="Wednesday">Wednesday</option>
                            <option value="Thursday">Thursday</option>
                            <option value="Friday">Friday</option>
                            <option value="Saturday">Saturday</option>
                            <option value="Sunday">Sunday</option>
                        </select>
                    </div>
                    <p>{classValuesErrors.secondday}</p>
                    
                    <label>Select Time</label>
                    <div className='form-inputs'>
                        <label>Start Time</label>
                        <input type="time" name="starttime" value={classValues.starttime} onChange={handleChange}></input>
                        <p>{classValuesErrors.starttime}</p>
                        
                        <label>End Time</label>
                        <input type="time" name="endtime" value={classValues.endtime} onChange={handleChange}></input>
                        <p>{classValuesErrors.endtime}</p>
                    </div>
                    
                
                <button type="submit">SUBMIT</button>
                </div>
            </form>
        </div>
  );
}

export default ScheduleAddClass