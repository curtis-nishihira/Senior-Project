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
                Password: password
            }),
        })
            .then(response => response.json())
            .then(data => {
                if (data.message == "Account is authenticated" && data.isAuthorized == true) {
                    fetch(process.env.REACT_APP_FETCH + '/login/createcookie', {
                        method: 'POST',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify({
                            Email: email,
                            IsAuthorized: data.isAuthorized
                        }),
                    })
                        .then(response => response.json())
                        .then(cookieResponse => {
                            navigate("/account/adminhome");
                        })
                        .catch((error) => {
                            console.error('Error', error);
                        });

                }
                else if (data.message == "Account is authenticated" && data.isAuthorized == false)
                {
                    navigate("/account/verification", { state: { email: email } });
                }
                else if (data.message == "Incorrect Password") {
                    alert(data.message);
                }
                else if (data.message == "Account not found.") {
                    alert(data.message);;
                }
                else {
                    alert(data.message);
                }
            })
            .catch((error) => {
                console.error('Error', error);
            });
    };

    return (
        <>
           
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