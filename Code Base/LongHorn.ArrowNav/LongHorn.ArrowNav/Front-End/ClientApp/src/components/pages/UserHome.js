import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import "./UserHome.css";
import Popup from './Popup';
import ScheduleAddClass from './ScheduleAddClass';
import ScheduleEditClass from './ScheduleEditClass';
import CapacitySurvey from './CapacitySurvey';


export function UserHome() {
    const userEmail = getEmailFromCookies();
    const [userFirstName, setFirstName] = useState("");
    const [userLastName, setLastName] = useState("");
    const [selectedCourse, setCourse] = useState("");
    const [selectedCourseType, setCourseType] = useState("");
    const [selectedBuilding, setBuilding] = useState("");
    const [selectedRoom, setRoom] = useState("");
    const [selectedDays, setDays] = useState("");
    const [selectedStartTime, setStartTime] = useState("");
    const [selectedEndTime, setEndTime] = useState("");
    const navigate = useNavigate();
    const [isAddOpen, setIsAddOpen] = useState(false);
    const [isEditOpen, setIsEditOpen] = useState(false);
    const [isCapacityOpen, setCapacityOpen] = useState(false);
    const [isSelected, setIsSelected] = useState(false);

    const toggleAddPopup = () => {
        setIsAddOpen(!isAddOpen);
    }
    const toggleEditPopup = () => {
        setIsEditOpen(!isEditOpen);
    }
    const toggleCapacityPopup = () => {
        setCapacityOpen(!isCapacityOpen);
    }
    const toggleIsSelected = () => {
        setIsSelected(!isSelected);
    }

    function fillTable() {
        if (isAddOpen == false && isEditOpen == false) {
            fetch(process.env.REACT_APP_FETCH + '/schedule/getschedule?email=' + userEmail, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
            })
                .then(response => response.json())
                .then(data => {
                    var classes = data;
                    var tbody = document.getElementById("schedule-body");
                    for (var i = 0; i < classes.length; i++) {
                        var trString = "myTr" + i.toString();
                        var tr = document.createElement('tr');
                        tr.setAttribute("id", trString);
                        tbody.appendChild(tr);
                        var td = document.createElement('td');
                        var input = document.createElement('input');
                        var buttonIdString = "btn" + i.toString();
                        input.setAttribute("id", buttonIdString)
                        input.setAttribute("type", "radio");
                        input.setAttribute("name", "rowSelected");
                        input.setAttribute("value", i + 1);
                        td.appendChild(input);
                        document.getElementById(trString).appendChild(td);
                        var td1 = document.createElement('td');
                        var course = document.createTextNode(classes[i]._course);
                        td1.appendChild(course);
                        document.getElementById(trString).appendChild(td1);
                        var td2 = document.createElement('td');
                        var courseType = document.createTextNode(classes[i]._coursetype);
                        td2.appendChild(courseType);
                        document.getElementById(trString).appendChild(td2);
                        var td3 = document.createElement('td');
                        var building = document.createTextNode(classes[i]._building);
                        td3.appendChild(building);
                        document.getElementById(trString).appendChild(td3);
                        var td4 = document.createElement('td');
                        var room = document.createTextNode(classes[i]._room);
                        td4.appendChild(room);
                        document.getElementById(trString).appendChild(td4);
                        var td5 = document.createElement('td');
                        var days = document.createTextNode(classes[i]._days);
                        td5.appendChild(days);
                        document.getElementById(trString).appendChild(td5);
                        var td6 = document.createElement('td');
                        var startTime = document.createTextNode(classes[i]._startTime);
                        td6.appendChild(startTime);
                        document.getElementById(trString).appendChild(td6);
                        var td7 = document.createElement('td');
                        var endTime = document.createTextNode(classes[i]._endTime);
                        td7.appendChild(endTime);
                        document.getElementById(trString).appendChild(td7);
                    }
                })
                .catch((error) => {
                    console.log('Error', error);
                })
        }
    }

    function getProfile() {
        fetch(process.env.REACT_APP_FETCH + "/login/getProfile?email=" + userEmail, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
        })
            .then(response => response.json())
            .then(data => {
                setFirstName(data._firstName);
                setLastName(data._lastName);
            })
            .catch((error) => {
                console.error('Error', error);
            });
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

    useEffect(() => {
        getProfile();
        const wrapper = document.getElementById('schedule-table');

        wrapper.addEventListener('click', (event) => {
            const isButton = event.target.nodeName === 'INPUT';
            if (!isButton) {
                return;
            }
            var table = document.getElementById("schedule-table");
            setCourse(table.rows[event.target.value].cells[1].innerHTML);
            setCourseType(table.rows[event.target.value].cells[2].innerHTML);
            setBuilding(table.rows[event.target.value].cells[3].innerHTML);
            setRoom(table.rows[event.target.value].cells[4].innerHTML);
            setDays(table.rows[event.target.value].cells[5].innerHTML);
            setStartTime(table.rows[event.target.value].cells[6].innerHTML);
            setEndTime(table.rows[event.target.value].cells[7].innerHTML);
        })
    }, [])

    function setToDefaultValues() {
        setCourse("");
        setCourseType("");
        setBuilding("");
        setRoom("");
        setDays("");
        setStartTime("");
        setEndTime("");
    }

    useEffect(() => {
        fillTable();
        return function cleanup() {
            setToDefaultValues();
            var body = document.getElementById('schedule-body');
            if (body != null) {
                body.innerHTML = '';
            }
        }
    }, [isAddOpen, isEditOpen, isSelected])

    function logout() {
        fetch(process.env.REACT_APP_FETCH + '/login/removecookie', {
            method: "GET",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
        })
            .then(response => response.json())
            .then(data => {
                console.log(data);
                if (data == "cookie removed") {
                    //should go one page backwards
                    navigate("/account");
                }
            })
            .catch((error) => {
                alert(error);
            });
    }

    function deleteCourse() {
        if (selectedCourse != '') {
            fetch(process.env.REACT_APP_FETCH +'/schedule/scheduledelete', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    _Username: userEmail,
                    _course: selectedCourse,
                    _coursetype: selectedCourseType,
                    _building: selectedBuilding,
                    _room: selectedRoom,
                    _days: selectedDays,
                    _starttime: selectedStartTime,
                    _endtime: selectedEndTime
                }),
            })
                .then(response => response.json())
                .then(data => {
                })
                .catch((error) => {
                    console.error('Error', error);
                });
            toggleIsSelected();
        }
        else {
            alert("You didn't choose an option");
        }
    }

    function findOnMap() {
        if (selectedCourse != '') {
            fetch(process.env.REACT_APP_FETCH + '/building/getBuildingbyAcronym?acronym=' + selectedBuilding, {
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
        else {
            alert("You didn't choose an option");
        }
    }

    function deleteAccount() {
        console.log(userEmail);
        fetch(process.env.REACT_APP_FETCH +"/login/deleteAccount?email=" + userEmail, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
        })
            .then(response => response.json())
            .then(data => {
                if (data == "account deleted") {
                    navigate("/account");
                }
            })
            .catch((error) => {
                console.error('Error', error);
            });
    }

    function validate() {
        if (selectedCourse != '') {
            toggleEditPopup();
        }
        else {
            alert("You didn't choose an option");
        }
    }
    return (
        <>
            <div>Names: {userFirstName} {userLastName}</div>
            <div> Email : {userEmail} </div>
            <button type="button" onClick={logout}> Log out </button>
            <button type="button" onClick={deleteAccount}> delete account</button>
            <table id="schedule-table" className="schedule-table">
                <thead >
                    <tr>
                        <th></th>
                        <th>Course</th>
                        <th>Course Type</th>
                        <th>Building</th>
                        <th>Room Number</th>
                        <th>Days</th>
                        <th>Start Time</th>
                        <th>End Time</th>
                    </tr>
                </thead>
                <tbody id="schedule-body">
                </tbody>
            </table>

            <div>
                <input id="button"
                    type="button"
                    value="ADD CLASS"
                    onClick={toggleAddPopup}
                />
                {isAddOpen && <Popup
                    content={<>
                        <ScheduleAddClass username={userEmail} handleCloser={toggleAddPopup} />
                    </>}
                    handleClose={toggleAddPopup}
                />}

                <input id="button"
                    type="button"
                    value="DELETE CLASS"
                    onClick={deleteCourse}
                />
                <input id="button"
                    type="button"
                    value="EDIT CLASS"
                    onClick={validate}
                />
                {isEditOpen && <Popup
                    content={<>
                        <ScheduleEditClass
                            username={userEmail}
                            course={selectedCourse}
                            courseType={selectedCourseType}
                            building={selectedBuilding}
                            room={selectedRoom}
                            days={selectedDays}
                            startTime={selectedStartTime}
                            endTime={selectedEndTime}
                            handleCloser={toggleEditPopup} />
                    </>}
                    handleClose={toggleEditPopup}
                />}
                <input id="button"
                    type="button"
                    value="FIND CLASS"
                    onClick={findOnMap}
                />
            </div>
            <div>
                <input id="button"
                    type="button"
                    value="Optional Building Survey"
                    onClick={toggleCapacityPopup}
                />
                {isCapacityOpen && <Popup
                    content={<>
                        <CapacitySurvey handleCloser={toggleCapacityPopup} />
                    </>}
                    handleClose={toggleCapacityPopup}
                />}
            </div>
        </>
    );
}