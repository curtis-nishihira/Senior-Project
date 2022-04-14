import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';



export const Rewards = () => {
    const initialValues = { answer: "" };
    const [formValues, setFormValues] = useState(initialValues);
    const [formErrors, setFormErrors] = useState({});
    const [isSubmit, setIsSubmit] = useState(false);
    const [isCredit, setCredit] = useState(0);
    const [isCount, setCount] = useState(0);

    const handleChange = (e) => {
        //console.log(e.target);
        const { name, value } = e.target;
        setFormValues({ ...formValues, [name]: value });
        //console.log(formValues);
    };
    
    const handleSubmit = (e) => {
        e.preventDefault();
        setFormErrors(validate(formValues));
        setIsSubmit(true);
    };

    useEffect(() => {
        console.log(formErrors);
        if (Object.keys(formErrors).length === 0 && isSubmit) {
            console.log(formValues);
        }
    }, [formErrors]);

    const validate = (values) => {
        const errors = {}
        var regex = "THE PYRAMID"
        
        if (!values.answer) {
            errors.answer = "Answer is required!";
        } else if (regex != values.answer.toUpperCase()) {
            errors.answer = "Not the correct answer!";
        }
        return errors;
    };

    function buttonPressed() {
        setCount(isCount + 1);
        setCredit(isCredit + 50);
    }

    //display
    return (
        <div className="container">
            <form onSubmit={handleSubmit}>
                <h1>Rewards</h1>
                <div className = "ui divider"></div>
                <div className="ui form"></div>
                <p>Credits: {isCredit}</p>

                    <div className="field">
                        <label>Clue:</label>
                        <label> Where the pharaohs sleep under cerulean skies... </label>
                        <input type="text" name="answer" placeholder="Answer" value={formValues.answer} onChange = {handleChange}/>
                    </div>

                <p>{formErrors.answer}</p>

                {Object.keys(formErrors).length === 0 && isSubmit ?
                    (<div>
                        <p>Correct Answer!</p>
                        <button
                            onClick={buttonPressed}
                            disabled = { isCount == 1}>Claim Credits</button>
                            
                    </div>) : (<div className="ui message success"></div>)}
                
                <button className = "fluid ui button blue">Submit</button>
            </form>

            <p></p>
            
            <label>Coupons:</label>
            <select>
                <option value="coupon1">10% off @ Sbarro</option>
                <option value="coupon2">10% off @ Outpost</option>
            </select>

        </div>
        
        );
}

export default Rewards;