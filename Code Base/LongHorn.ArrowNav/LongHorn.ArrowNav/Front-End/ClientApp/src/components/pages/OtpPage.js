import React, { useEffect, useState, useRef } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';


export function OTP() {

    const otp = useRef("");
    const location = useLocation();
    const navigate = useNavigate();
    const [resend, setResend] = useState(false);
    const isClosed = useRef(false);

    const toggleResend = () => {
        setResend(!resend);
    }

    function isValid() {
        if ((document.getElementById("user-otp").value === otp.current) && document.getElementById('time').innerHTML != "0:00") {
            fetch(process.env.REACT_APP_FETCH + '/login/updateSuccessfulAttempt?email=' + location.state.email, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
            })
                .then(response => response.json())
                .then(data => {
                    fetch(process.env.REACT_APP_FETCH + '/login/createcookie', {
                        method: 'POST',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify({
                            Email: location.state.email,
                            IsAuthorized: false,
                        }),
                    })
                        .then(response => response.json())
                        .then(cookieResponse => {
                            isClosed.current = true;
                            navigate("/account/userhome");

                        })
                        .catch((error) => {
                            console.error('Error', error);
                        });
                })
                .catch((error) => {
                    console.error('Error', error);
                });


        }
        else {
            fetch(process.env.REACT_APP_FETCH + '/login/updateFailedAttempts?email=' + location.state.email, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },

            })
                .then(response => response.json())
                .then(data => {
                    if (data.includes("disabled")) {
                        alert(data);
                        navigate("/account");
                    }
                    else {
                        alert(data);
                    }

                })
                .catch((error) => {
                    console.error('Error', error);
                });

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
        const interval = setInterval(function () {
            var breakout = isClosed.current;
            minutes = parseInt(timer / 60, 10);
            seconds = parseInt(timer % 60, 10);

            minutes = minutes < 10 ? "0" + minutes : minutes;
            seconds = seconds < 10 ? "0" + seconds : seconds;

            display.textContent = minutes + ":" + seconds;

            if (breakout) {
                clearInterval(interval);
            }
            if (--timer < 0) {
                timer = duration;
            }
            if (minutes == 0 && seconds == 0) {
                clearInterval(interval);
                if (window.confirm("Time Ran Out. Resend OTP? Else get redirected to the login page.")) {
                    toggleResend();
                }
                else {
                    navigate("/account");
                }
            }


        }, 1000);
    }


    useEffect(() => {
        getOTP();
        startTimer(60 * 2, document.querySelector('#time'));
        const validateButton = document.getElementById("validate-btn");
        validateButton.addEventListener('click', () => {
            isValid();
        });
        const cancelBtn = document.getElementById("cancel-btn");
        cancelBtn.addEventListener('click', () => {
            if (window.confirm("Are you sure you want to go back?")) {
                isClosed.current = true;
                navigate("/account");
            }
           
        })
    }, [resend]);


    return (

        <>
            <div>
                <div>OTP expires in <span id="time">02:00</span> minutes!</div>
                <input className="otp-bar" id="user-otp" placeholder="Enter the OTP" />
                <button type="button" id="validate-btn" > validate</button>
                <button type="button" id="cancel-btn" > cancel</button>
            </div>

        </>
    );
}
export default OTP;