import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import "./UserHome.css";
import Popup from './Popup';
import AddUser from './AddUser';
import EditUser from './EditUser';


export function AdminHome() {
    const userEmail = getEmailFromCookies();
    const [userFirstName, setFirstName] = useState("");
    const [userLastName, setLastName] = useState("");
    const [selectedEmail, setEmail] = useState("");
    const [selectedStatus, setStatus] = useState("");
    const [selectedType, setType] = useState("");
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
    const toggleIsSelected = () => {
        setIsSelected(!isSelected);
    }

    function fillTable() {
        if (isAddOpen == false && isEditOpen == false) {
            // change to the getter for admin info
            console.log("enter");
            fetch(process.env.REACT_APP_FETCH + '/login/getAllUsers', {
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
                        var course = document.createTextNode(classes[i].email);
                        td1.appendChild(course);
                        document.getElementById(trString).appendChild(td1);
                        var td2 = document.createElement('td');
                        var courseType = document.createTextNode(classes[i].accountStatus);
                        td2.appendChild(courseType);
                        document.getElementById(trString).appendChild(td2);
                        var td3 = document.createElement('td');
                        var building = document.createTextNode(classes[i].accessLevel);
                        td3.appendChild(building);
                        document.getElementById(trString).appendChild(td3);
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
            setEmail(table.rows[event.target.value].cells[1].innerHTML);
            setStatus(table.rows[event.target.value].cells[2].innerHTML);
            setType(table.rows[event.target.value].cells[3].innerHTML);

        })
    }, [])

    function setToDefaultValues() {
        setEmail("");
        setStatus("");
        setType("");
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

    function deleteUser() {
        if (selectedEmail != '') {
            fetch(process.env.REACT_APP_FETCH + '/login/deleteUser?email=' + selectedEmail, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
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


    function deleteAccount() {
        if (window.confirm("Are you sure you want to delete your account?")) {
            fetch(process.env.REACT_APP_FETCH + "/login/deleteAccount?email=" + userEmail, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
            })
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                })
                .catch((error) => {
                    console.error('Error', error);
                });
        }

        
    }

    function validate() {
        if (selectedEmail != '') {
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
                        <th>Email</th>
                        <th>Account Status</th>
                        <th>Account Type</th>
                    </tr>
                </thead>
                <tbody id="schedule-body">
                </tbody>
            </table>

            <div>
                <input id="button"
                    type="button"
                    value="ADD USER"
                    onClick={toggleAddPopup}
                />
                {isAddOpen && <Popup
                    content={<>
                        <AddUser handleCloser={toggleAddPopup} />
                    </>}
                    handleClose={toggleAddPopup}
                />}

                <input id="button"
                    type="button"
                    value="DELETE USER"
                    onClick={deleteUser}
                />
                <input id="button"
                    type="button"
                    value="EDIT USER"
                    onClick={validate}
                />
                {isEditOpen && <Popup
                    content={<>
                        <EditUser
                            email={selectedEmail}
                            status={selectedStatus}
                            type={selectedType}
                            handleCloser={toggleEditPopup} />
                    </>}
                    handleClose={toggleEditPopup}
                />}
            </div>
        </>
    );
}