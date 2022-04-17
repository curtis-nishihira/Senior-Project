import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
const RadioInput = ({ label, value, checked, setter }) => {
	return (
		<label>
			<input type="radio" checked={checked == value}
				onChange={() => setter(value)} />
			<span>{label}</span>
		</label>
	);
};

export const CapacitySurvey = (props) => {
	const [lib, setLib] = React.useState();
	const [usu, setUsu] = React.useState();
	const [srwc, setSrwc] = React.useState();
	const navigate = useNavigate();
	const handleSubmit = e => {
		e.preventDefault();
		const data = { lib, usu, srwc };
		const json = JSON.stringify(data);
		console.clear();
		console.log(json);
	};

	const surveyHandler = (e) => {
		e.preventDefault();
		fetch(process.env.REACT_APP_FETCH + '/capacity/updateCapacity', {
			method: 'POST',
			headers: {
				'Accept': 'application/json',
				'Content-Type': 'application/json',
			},
			body: JSON.stringify([
				{
					_WeekdayName: 'string',
					_TimeSlot: 'string',
					_Building: 'LIB',
					_AddValue: lib
				},
				{
					_WeekdayName: 'string',
					_TimeSlot: 'string',
					_Building: 'USU',
					_AddValue: usu
				},
				{
					_WeekdayName: 'string',
					_TimeSlot: 'string',
					_Building: 'SRWC',
					_AddValue: srwc
				}
			]),
		})
			.then(response => response.json())
			.then(data => {
				console.log(data);
				navigate("/account", { state: { message: "Survey Completed" } });
			})
			.catch((error) => {
				console.error('Error', error);
			});
	};

	return (
		<form onSubmit={surveyHandler}>
			<h3>Capacity</h3>
			<p>This optional survey is to improve user experience and more
				accurately model the traffic of campus</p>
			<p>Please rate the following the traffic of locations on campus
				from a scale of 1-5, based on the following criteria...</p>
			<div>
				<label>Library:</label>
				<RadioInput label="1" value="1" checked={lib} setter={setLib} />
				<RadioInput label="2" value="2" checked={lib} setter={setLib} />
				<RadioInput label="3" value="3" checked={lib} setter={setLib} />
				<RadioInput label="4" value="4" checked={lib} setter={setLib} />
				<RadioInput label="5" value="5" checked={lib} setter={setLib} />
			</div>
			<div>
				<label>University Student Union:</label>
				<RadioInput label="1" value="1" checked={usu} setter={setUsu} />
				<RadioInput label="2" value="2" checked={usu} setter={setUsu} />
				<RadioInput label="3" value="3" checked={usu} setter={setUsu} />
				<RadioInput label="4" value="4" checked={usu} setter={setUsu} />
				<RadioInput label="5" value="5" checked={usu} setter={setUsu} />
			</div>
			<div>
				<label>Student Recreation & Wellness Center:</label>
				<RadioInput label="1" value="1" checked={srwc} setter={setSrwc} />
				<RadioInput label="2" value="2" checked={srwc} setter={setSrwc} />
				<RadioInput label="3" value="3" checked={srwc} setter={setSrwc} />
				<RadioInput label="4" value="4" checked={srwc} setter={setSrwc} />
				<RadioInput label="5" value="5" checked={srwc} setter={setSrwc} />
			</div>
			<button type="submit">Submit</button>
		</form>
	);
};

export default CapacitySurvey;