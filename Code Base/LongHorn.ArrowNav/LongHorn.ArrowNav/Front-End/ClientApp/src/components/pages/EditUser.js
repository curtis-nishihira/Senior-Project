import React, { useState, useEffect } from "react"

export const EditUser = (props) => {
    const [isSubmit, setIsSubmit] = useState(false);
    const [status, setStatus] = useState(props.status);
    const [accessLevel, setAccessLevel] = useState(props.type);
    const [email, setEmail] = useState(props.email);


    const submitHandler = (e) => {
        //prevents the browser from refreshing
        e.preventDefault();
        //will have to change when it gets published so that it will actually communicate with the
        //live website
        fetch(process.env.REACT_APP_FETCH + '/login/updateUser', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                email: email,
                accessLevel: accessLevel,
                accountStatus: status,
                emailConfirmed: ''

            }),
        })
            .then(response => response.json())
            .then(data => {
                if (data == "Account Updated") {
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
                <h1>Edit User Information</h1>
                <div className="form-group">
                    <label>Email</label>
                    <input type="email" disabled="disabled" required value={email} onChange={(e) => setEmail(e.target.value)}
                        className="form-control" placeholder="Enter email" />
                </div>
                <div className="form-group">
                    <label>Account Status</label>
                    <input type="text" required value={status} onChange={(e) => setStatus(e.target.value)}
                        className="form-control" placeholder="First name" />
                </div>

                <div className="form-group">
                    <label>Access Level</label>
                    <input type="text" required value={accessLevel} onChange={(e) => setAccessLevel(e.target.value)}
                        className="form-control" placeholder="Last name" />
                </div>
                <div>
                    <button type="submit">SUBMIT</button>
                </div>
            </form>
        </div>
    );
}

export default EditUser