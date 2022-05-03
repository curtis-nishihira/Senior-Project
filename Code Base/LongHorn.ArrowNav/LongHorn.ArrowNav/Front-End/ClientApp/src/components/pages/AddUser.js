import React, { useState, useEffect } from "react"

export const AddUser = (props) => {
    const [isSubmit, setIsSubmit] = useState(false);
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassphrase] = useState('');


    const submitHandler = (e) => {
        //prevents the browser from refreshing
        e.preventDefault();
        //will have to change when it gets published so that it will actually communicate with the
        //live website
        fetch(process.env.REACT_APP_FETCH + '/register/createaccount', {
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
                    props.handleCloser();
                    setIsSubmit(true);
                }
                else {
                    alert(data);

                }
            })
            .catch((error) => {
                console.error('Error:', error);
            });

    };


    return (
        <div className="addclass-container">
            <form onSubmit={submitHandler}>
                <h1>Enter User Information</h1>
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
                    <input type="text" required value={password} onChange={(e) => setPassphrase(e.target.value)}
                        className="form-control" placeholder="Enter password" />
                </div>
                <div>
                    <button type="submit">SUBMIT</button>
                </div>
            </form>
        </div>
    );
}

export default AddUser