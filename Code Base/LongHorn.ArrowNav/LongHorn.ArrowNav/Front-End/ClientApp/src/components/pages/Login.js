import React, { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import "./Login.css"

export const Login = (props) => {
    const [email, setEmail] = useState('');
    const [password, setPassphrase] = useState('');
    const [message, setMessage] = useState('');
    const location = useLocation();
    const navigate = useNavigate();

    useEffect(() => {
        if (location.state != undefined) {
            document.getElementById('notification').style.visibility = 'visible';
            setMessage(location.state.message);
            console.log(message);
            setTimeout(() => {
                document.getElementById('notification').style.visibility = 'hidden';
            }, 7000);
        }

        // Update the document title using the browser API

    });

    /* Nonfunctional loginHandler 
     * The fetch does not work. Would suggest making a model for the login info or research another way.
     * 
     * */


    const loginHandler = (e) => {
        e.preventDefault();
        fetch('https://arrownav.azurewebsites.net/login', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                _Username: email,
                _Password: password
            }),
        })
            .then(response => response.json())
            .then(data => {
                if (data == "Account is authenticated") {
                    fetch('https://arrownav.azurewebsites.net/login/createcookie', {
                        method: 'POST',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify({
                            _Username: email,
                            _Password: password
                        }),
                    })
                        .then(response => response.json())
                        .then(cookieResponse => {
                            console.log(cookieResponse);

                            navigate("/account/userhome");
                           
                        })
                        .catch((error) => {
                            console.error('Error', error);
                        });
                }
                else if (data == "Incorrect Password") {
                    navigate("/account", { state: { message: data } });
                }
                else if (data == "Account not found.") {
                    navigate("/account", { state: { message: data } });
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