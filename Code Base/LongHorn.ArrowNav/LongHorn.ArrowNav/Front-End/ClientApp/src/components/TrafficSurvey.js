import React, { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import "./TrafficSurvey.css"

const RadioInput = ({ label, value, checked, setter }) => {
	return (
		<label>
			<input type="radio" checked={checked == value}
				onChange={() => setter(value)} />
			<span>{label}</span>
		</label>
	);
};


export const TrafficSurvey = (props) => {
	const [zone1, setZone1] = React.useState();
	const [zone2, setZone2] = React.useState();
	const [zone3, setZone3] = React.useState();

	//function openForm() {
	//	console.log(document.getElementById('box'))
	//}
	const closeForm = () => {
		document.getElementById("box").style.visiblity = "hidden";
	}
	//window.onload = openForm();

	const handleSubmit = e => {
		e.preventDefault();
		const data = { zone1, zone2, zone3 };
		const json = JSON.stringify(data);
		console.clear();
		console.log(json);
	};

	const surveyHandler = (e) => {
		e.preventDefault();
		console.log(parseInt(zone1));
		fetch('https://arrownav.azurewebsites.net/trafficsurvey', {
			method: 'POST',
			headers: {
				'Accept': 'application/json',
				'Content-Type': 'application/json',
			},
			body: JSON.stringify([
				{
					_WeekdayName: 'string',
					_TimeSlot: 'string',
					_ZoneName: 'Zone1',
					_TotalValue: zone1
				},
				{
					_WeekdayName: 'string',
					_TimeSlot: 'string',
					_ZoneName: 'Zone2',
					_TotalValue: zone2
				},
				{
					_WeekdayName: 'string',
					_TimeSlot: 'string',
					_ZoneName: 'Zone3',
					_TotalValue: zone3
				}
			]),
		})
			.then(response => response.json())
			.then(data => {
				console.log(data);
			})
			.catch((error) => {
				console.error('Error', error);
			});
	};

	return (
		<div className="surveyBox">
			<div id="box" className="box">
				<span className="close-icon" onClick={() => { document.getElementById('box').style.visibility='hidden' }} >x</span>
				<form onSubmit={surveyHandler}>
					<h3>Traffic Survey</h3>
					<p>This optional survey is to improve user experience and more
						accurately model the traffic of campus</p>
					<p>Please rate the following the traffic of locations on campus
						from a scale of 1-5, based on the following criteria...</p>
					<div>
						<label>Staircase next to USU:</label>
						<RadioInput label="1" value="1" checked={zone1} setter={setZone1} />
						<RadioInput label="2" value="2" checked={zone1} setter={setZone1} />
						<RadioInput label="3" value="3" checked={zone1} setter={setZone1} />
						<RadioInput label="4" value="4" checked={zone1} setter={setZone1} />
						<RadioInput label="5" value="5" checked={zone1} setter={setZone1} />
					</div>
					<div>
						<label>Walkway in front of Bookstore:</label>
						<RadioInput label="1" value="1" checked={zone2} setter={setZone2} />
						<RadioInput label="2" value="2" checked={zone2} setter={setZone2} />
						<RadioInput label="3" value="3" checked={zone2} setter={setZone2} />
						<RadioInput label="4" value="4" checked={zone2} setter={setZone2} />
						<RadioInput label="5" value="5" checked={zone2} setter={setZone2} />
					</div>
					<div>
						<label>Walkway in front of SRWC:</label>
						<RadioInput label="1" value="1" checked={zone3} setter={setZone3} />
						<RadioInput label="2" value="2" checked={zone3} setter={setZone3} />
						<RadioInput label="3" value="3" checked={zone3} setter={setZone3} />
						<RadioInput label="4" value="4" checked={zone3} setter={setZone3} />
						<RadioInput label="5" value="5" checked={zone3} setter={setZone3} />
					</div>
					<button type="submit" >Submit</button>
				</form>
			</div>
		</div>
	);
};

export default TrafficSurvey;