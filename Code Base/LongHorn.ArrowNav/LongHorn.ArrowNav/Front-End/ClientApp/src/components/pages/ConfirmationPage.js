import React from 'react';
import { useNavigate } from 'react-router-dom';
import "./ConfirmationPage.css";

export function ConfirmationPage() {

    const navigate = useNavigate();

    setTimeout(() => {
        navigate("/account");
    }, 10000);


    return (
        <>
            <div className="container">
                <h1>Thank You</h1>

                <p>Your email has been confirmed and your account has now been activated</p>

                <p> You will be redirected back to the web app after 10 seconds</p>

            </div>
        </>

    );
}

export default ConfirmationPage