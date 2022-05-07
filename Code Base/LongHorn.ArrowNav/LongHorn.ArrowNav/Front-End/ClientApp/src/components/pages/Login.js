import React, { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import "./Login.css"

export const Login = (props) => {
    const [email, setEmail] = useState('');
    const [password, setPassphrase] = useState('');
    const [message, setMessage] = useState('');
    const location = useLocation();
    const navigate = useNavigate();

    function getAccessLevel() {
        var decodedCookies = decodeURIComponent(document.cookie);
        var listOfCookies = decodedCookies.split("; ");
        for (var i = 0; i < listOfCookies.length; i++) {
            let temp = listOfCookies[i].split("=");
            if (temp[0] == process.env.REACT_APP_COOKIE_KEY) {
                let cookieSplit = listOfCookies[i].split('"');
                let accessLevel = cookieSplit[10];
                if (accessLevel.includes("true")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    }



    useEffect(() => {
        if (location.state != undefined) {
            alert(location.state.message);
            setMessage(location.state.message);
            
        }
        

        // Update the document title using the browser API

    },[]);

    const loginHandler = (e) => {
        e.preventDefault();
        
        fetch(process.env.REACT_APP_FETCH + '/login', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                Username: email,
                Password: password,
                IsAuthorized: false,
            }),
        })
            .then(response => response.json())
            .then(data => {
                if (data == "Account is authenticated") {

                    navigate("/account/verification", { state: { email: email } });

                    fetch(process.env.REACT_APP_FETCH + '/login/createcookie', {
                        method: 'POST',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify({
                            Username: email,
                            Password: password,
                            IsAuthorized: false,
                        }),
                    })
                        .then(response => response.json())
                        .then(cookieResponse => {
                            var userLevel = getAccessLevel();
                            if (userLevel === true) {
                                navigate("/account/adminhome");
                            }
                            else {
                                navigate("/account/userhome");
                            }
                           
                        })
                        .catch((error) => {
                            console.error('Error', error);
                        });

                }
                else if (data == "Incorrect Password") {
                    alert(data);
                }
                else if (data == "Account not found.") {
                    alert(data);;
                }
                else {
                    alert(data);
                }
            })
            .catch((error) => {
                console.error('Error', error);
            });
    };

    return (
        <>
            <div id='notification' className='notification-container'>
                {message}

            </div>
            <div className='form-container'>
                <form onSubmit={loginHandler}>
                    <h3>Log in</h3>

                    <div className="form-group">
                        <label>Email</label>
                        <input type="email" required value={email} onChange={(e) => setEmail(e.target.value)}
                            className="form-control" placeholder="Enter email" />
                    </div>

                    <div className="form-group">
                        <label>Password</label>
                        <input type="password" required value={password} onChange={(e) => setPassphrase(e.target.value)}
                            className="form-control" placeholder="Enter password" />
                    </div>

                    <div className="form-group">
                        <div className="custom-control custom-checkbox">
                            <input type="checkbox" className="custom-control-input" id="customCheck1" />
                            <label className="custom-control-label" htmlFor="customCheck1">Remember me</label>
                        </div>
                    </div>

                    <button type="submit" className="btn btn-dark btn-lg btn-block">Sign in</button>
                    <p className="forgot-password text-right">
                        <a href="account/register">Register?</a>
                    </p>
                </form>
            </div>


        </>
    );
}

export default Login;