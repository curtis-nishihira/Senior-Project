import React, { useEffect, useState } from 'react';



export function UserHome() {
    const userName = getCookies();

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

    return (
        <>
            <div> {userName}</div>
            <div>
                <datalist id="suggestions">
                </datalist>
                <input autoComplete="on" list="suggestions" />
            </div>
        </>
    );
}