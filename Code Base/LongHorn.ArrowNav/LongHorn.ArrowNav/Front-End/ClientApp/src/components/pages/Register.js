import { data } from 'jquery';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export const Register = () => {
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassphrase] = useState('');
    const navigate = useNavigate();

    const submitHandler = (e) => {
        //prevents the browser from refreshing
        e.preventDefault();
        //will have to change when it gets published so that it will actually communicate with the
        //live website
        fetch('https://localhost:44465/register/createaccount', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                _email: email,
                _passphrase: password,
                _firstName: firstName,
                _lastName: lastName

            }),
        })
            .then(response => response.json())
            .then(data => {
                if (data == "Successful Account Creation") {
                    //use this to register to a new page that says something about the confirmation email 
                    //being sent.
                    navigate("/account", { state: { message: "Check your email for a confirmation link to complete your registration" } });
                }
                else {
                    console.log("Something went wrong");

                }
            })
            .catch((error) => {
                console.error('Error:', error);
            });

    };

    return (
        <div className='form-container'>
            <form onSubmit={submitHandler}>
                <h2>Register</h2>

                <div className="form-group">
                    <label>First name</label>
                    <input type="text" required value={firstName} onChange={(e) => setFirstName(e.target.value)}
                        className="form-control" placeholder="First name" />
                </div>

                <div className="form-group">
                    <label>Last name</label>
                    <input type="text" required value={lastName} onChange={(e) => setLastName(e.target.value)}
                        className="form-control" placeholder="Last name" />
                </div>

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

                <button type="submit" className="btn btn-dark btn-lg btn-block">Register</button>
                <p className="forgot-password text-right">
                    Already registered? <a href="/account">log in</a>
                </p>
            </form>
        </div>
    );
}