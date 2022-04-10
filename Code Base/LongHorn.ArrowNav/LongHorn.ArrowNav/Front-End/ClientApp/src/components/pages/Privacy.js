import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';


export const Privacy = (props) => {
	const navigate = useNavigate();

	const surveyHandler = (e) => {
		navigate("/account");
	};

	return (
		<form onSubmit={surveyHandler}>
			<h3>Privacy Agreement</h3>
			<p>Insert Policy</p>
			<button type="submit">Accept</button>
			<button type="submit">Don't Allow</button>
		</form>
	);
};

export default Privacy;