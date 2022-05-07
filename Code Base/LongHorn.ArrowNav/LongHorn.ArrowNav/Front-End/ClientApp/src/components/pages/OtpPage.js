import React, { useEffect, useState,useRef } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';


export function OTP() {

    const otp = useRef("");
    const userInput = useRef("");
    const location = useLocation();
    const navigate = useNavigate();

    function isValid() {
        if ((document.getElementById("user-otp").value === otp.current) && document.getElementById('time').innerHTML != "0:00") {
            fetch(process.env.REACT_APP_FETCH + '/login/createcookie', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    _Username: location.state.email,
                    _Password: "",
                }),
            })
                .then(response => response.json())
                .then(cookieResponse => {

                    navigate("/account/userhome");

                })
                .catch((error) => {
                    console.error('Error', error);
                });
        }
        else {
            alert(userInput.current + " :" + otp.current);
        }
    }
    
    async function getOTP() {

        var url = process.env.REACT_APP_FETCH + "/login/getOTP?email=" + location.state.email;
        fetch(url, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
        })
            .then(response => response.json())
            .then(data => {
                otp.current = data;
            })
            .catch((error) => {
                console.error('Error', error);
            });
        
    }
    function startTimer(duration, display) {
        var timer = duration, minutes, seconds;
        setInterval(function () {
            minutes = parseInt(timer / 60, 10);
            seconds = parseInt(timer % 60, 10);

            minutes = minutes < 10 ? "0" + minutes : minutes;
            seconds = seconds < 10 ? "0" + seconds : seconds;

            display.textContent = minutes + ":" + seconds;

            if (--timer < 0) {
                timer = duration;
            }
        }, 1000);
    }


    useEffect(() => {
        getOTP();
        startTimer(60 * 2,document.querySelector('#time'))
        const validateButton = document.getElementById("validate-btn");
        validateButton.addEventListener('click', () => {
            isValid();
        });
    }, []);


    return (

        <>
            <div>
                <div>Registration closes in <span id="time">02:00</span> minutes!</div>
                <input className="otp-bar" id = "user-otp" placeholder="Enter the One time Passphrase" />
                <button type="button" id="validate-btn" > validate</button>
                </div>

        </>
    );
}
export default OTP;