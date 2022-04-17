import React, { useEffect, useState } from 'react';

export const CapacitySurvey = (props) => {
	const defaultValues = { usu: "", lib: "", srwc: "", usuTime: "", libTime: "", srwcTime: "" };
	const [buildingValues, setBuildingValues] = useState(defaultValues);


	const handleChange = (e) => {
		const { name, value } = e.target;
		setBuildingValues({ ...buildingValues, [name]: value })
	}



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
					_TimeSlot: buildingValues.libTime,
					_Building: 'LIB',
					_AddValue: buildingValues.lib
				},
				{
					_WeekdayName: 'Monday',
					_TimeSlot: buildingValues.usuTime,
					_Building: 'USU',
					_AddValue: buildingValues.usu
				},
				{
					_WeekdayName: 'Monday',
					_TimeSlot: buildingValues.srwcTime,
					_Building: 'SRWC',
					_AddValue: buildingValues.srwc
				}
			]),
		})
			.then(response => response.json())
			.then(data => {
				console.log(data);
				props.handleCloser();
			})
			.catch((error) => {
				console.error('Error', error);
			});
	};

	return (
		<form onSubmit={surveyHandler}>
			<h3>Capacity Survey</h3>
			<p>This optional survey is to improve user experience and more
				accurately model the traffic of campus</p>
			<p>Please rate the following the traffic of locations on campus
				from a scale of 1-5, based on the following criteria</p>
			<ul>
				<li>Ability to obtain a table, seat, or exercise equipment</li>
				<li>Ability to walk about without having to maneuvre around people</li>
				<li>Access to and helpfulness of building employees</li>
				<li>Any lines or services waited on or in</li>
			</ul>
			<div className='form-inputs'>
				<label>Library</label>
				<select name="lib" required value={buildingValues.lib} onChange={handleChange}>
					<option value="0">n/a</option>
					<option value="1">1</option>
					<option value="2">2</option>
					<option value="3">3</option>
					<option value="4">4</option>
					<option value="5">5</option>
				</select>
			</div>
			<div className='form-inputs'>
				<label>Time Arrived at Building</label>
				<input type="time" name="libTime" required value={buildingValues.libTime} onChange={handleChange} ></input>
			</div>
			<div className='form-inputs'>
				<label>University Student Union</label>
				<select name="usu" required value={buildingValues.usu} onChange={handleChange}>
					<option value="0">n/a</option>
					<option value="1">1</option>
					<option value="2">2</option>
					<option value="3">3</option>
					<option value="4">4</option>
					<option value="5">5</option>
				</select>
			</div>
			<div className='form-inputs'>
				<label>Time Arrived at Building</label>
				<input type="time" name="usuTime" required value={buildingValues.usuTime} onChange={handleChange} ></input>
			</div>
			<div className='form-inputs'>
				<label>Student Recreation & Wellness Center</label>
				<select name="srwc" required value={buildingValues.srwc} onChange={handleChange}>
					<option value="0">n/a</option>
					<option value="1">1</option>
					<option value="2">2</option>
					<option value="3">3</option>
					<option value="4">4</option>
					<option value="5">5</option>
				</select>
			</div>
			<div className='form-inputs'>
				<label>Time Arrived at Building</label>
				<input type="time" name="srwcTime" required value={buildingValues.srwcTime} onChange={handleChange}></input>
			</div>
			<button type="submit">Submit</button>
		</form>
	);
};

export default CapacitySurvey;