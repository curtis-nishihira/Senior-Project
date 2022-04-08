import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import "./UserHome.css";


export function UserHome() {
    const userName = getCookies();
    const navigate = useNavigate();
    //function fillList() {
    //    fetch('https://arrownav.azurewebsites.net', {

    //    })
    //}

    function getCookies() {
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
    //unsure of its functionality
    function logout() {
        fetch('https://localhost:44465/login/removecookie')
            .then(response => response.json())
            .then(data => {
                console.log(data);
                if (data == "cookie removed")
                {
                    navigate("https://localhost:44465/account");
                }
            })
            .catch((error) => {
                console.error('Error', error);
            });
    }

    return (
        <>
            <div> User : {userName}</div>
            <button type="button" onClick={logout}> Log out </button>
            <table className= "schedule-table">
                <tr>
                    <th>Company</th>
                    <th>Contact</th>
                    <th>Country</th>
                </tr>
            </table>
        </>
    );
}