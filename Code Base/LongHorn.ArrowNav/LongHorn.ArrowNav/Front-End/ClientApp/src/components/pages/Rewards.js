import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';



export const Rewards = () => {
    //credit and counter states
    const initialValues = { answer: "" };
    const [formValues, setFormValues] = useState(initialValues);
    const [formErrors, setFormErrors] = useState({});
    const [isSubmit, setIsSubmit] = useState(false);
    const [isCredit, setCredit] = useState(0);
    const [isCount, setCount] = useState(0);
    const email = getEmailFromCookies();

    const [isClicked, setClick] = useState(false);

    //coupon states
    const initVal = { coupon: "" };
    const [formVal, setFormVal] = useState(initVal);
    const [formErr, setFormErr] = useState({});
    const [isSub, setIsSub] = useState(false);

    //getting cookies
    function getEmailFromCookies() {
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

    //fetchData for GET and POST
    async function fetchData(url, methodType, bodyData) {
        if (methodType === "GET") {
            const response = await fetch(url);
            const data = await response.json();
            return data;
        }
        else if (methodType === "POST") {
            const response = await fetch(url, { method: methodType })
            const data = await response.json();
            return data;
        }

    }

    //toggle the value for "Claim Credits" is clicked
    const toggleClickListener = () => {
        setClick(!isClicked);
    }

    //Credit and Counter
    useEffect( () => {
        getCredits();
        getCounter();
    }, [isClicked])


    async function getCredits() {
        var url = process.env.REACT_APP_FETCH + "/rewards/GetCredits?email=" + email;
        var credits = await fetchData(url, "GET", []);
        setCredit(credits);
    }

    async function getCounter() {
        var url = process.env.REACT_APP_FETCH +"/rewards/GetCounter?email=" + email;
        var counter = await fetchData(url, "GET", []);
        setCount(counter);
    }

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormValues({ ...formValues, [name]: value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        setFormErrors(validate(formValues));
        setIsSubmit(true);
    };

    const validate = (values) => {
        const errors = {}
        var regex = "ARROWNAV"

        if (!values.answer) {
            errors.answer = "Answer is required!";
        } else if (regex != values.answer.toUpperCase()) {
            errors.answer = "Not the correct answer!";
        }
        return errors;
    };

    async function buttonPressed() {
        fetch(process.env.REACT_APP_FETCH +"/rewards/SetCredits", {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                _Email: email,
                _Credits: (isCredit + 50),
                _Counter: 0
            }),

        })
            .then(response => response.json())
            .then(data2 => {
                console.log(data2)
            })
            .catch((error) => {
                console.error('Error', error);
            });

        fetch(process.env.REACT_APP_FETCH + "/rewards/SetCounter", {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                _Email: email,
                _Credits: 0,
                _Counter: (isCount + 1)
            })
        })
            .then(response => response.json())
            .then(data => {
                console.log(data)
            })
            .catch((error) => {
                console.error('Error', error);
            });
        toggleClickListener();
    }

    //Coupon
    const couponChange = (e) => {
        const { name, value } = e.target;
        setFormVal({ ...formVal, [name]: value });
    };

    const couponSubmit = (e) => {
        e.preventDefault();
        setFormErr(validation(formVal));
        setIsSub(true);
    };

    const validation = (values) => {
        const err = {}
        var regex = "SBARRO"

        if (!values.coupon) {
            err.coupon = "Answer is required!";
        } else if (regex != values.coupon.toUpperCase()) {
            err.coupon = "Not a valid coupon entry";
        }
        return err;
    };

    async function buttonPressed2() {
        alert("ArrowNav Coupon: 10% off Sbarro order!")
        fetch(process.env.REACT_APP_FETCH +"/rewards/SetCredits", {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                _Email: email,
                _Credits: (isCredit - 50),
                _Counter: 0
            }),

        })
            .then(response => response.json())
            .then(data2 => {
                console.log(data2)
            })
            .catch((error) => {
                console.error('Error', error);
            });
        toggleClickListener();
    }


    //DISPLAY
    return (
        <div className="container">
            <form onSubmit={handleSubmit}>
                <h1>Rewards</h1>
                <div className="ui divider"></div>
                <div className="ui form"></div>
                <p>Credits: {isCredit}</p>

                <div className="field">
                    <label>Clue:</label>
                    <label> Where the pharaohs sleep under cerulean skies... </label>
                    <input type="text" name="answer" placeholder="Answer" value={formValues.answer} onChange={handleChange} />
                </div>

                <p>{formErrors.answer}</p>

                {Object.keys(formErrors).length === 0 && isSubmit ?
                    (<div>
                        <p>Correct Answer!</p>
                        <button
                            onClick={buttonPressed}
                            disabled={isCount == 1}>Claim Credits
                        </button>

                    </div>) : (<div className="ui message success"></div>)}
                <button className="fluid ui button blue">Submit</button>
            </form>

            <p></p>

            <form onSubmit={couponSubmit}>
                <div className="field">
                    <label>Coupons (cost 50 credits):</label>
                    <label>Type the following for coupon:</label>
                    <label>"Sbarro" for 10% off Sbarro order</label>
                    <input type="text" name="coupon" placeholder="Answer" value={formVal.coupon} onChange={couponChange} />
                </div>

                <p>{formErr.coupon}</p>

                {Object.keys(formErr).length === 0 && isSub && isCredit >= 50 ?
                    (<div>
                        <p>Warning! This coupon, when claimed, is for one-time use at time of purchasing order</p>
                        <button onClick={buttonPressed2}>Claim Coupon

                        </button>
                    </div>) : (<p></p>)
                }
            </form>
        </div>

    );
}

export default Rewards;