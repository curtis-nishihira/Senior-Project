import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';


export const Privacy = (props) => {

	const surveyHandler = (e) => {
		props.handleCloser();
	};

	return (
		<form onSubmit={surveyHandler}>
			<h3>Privacy Agreement</h3>
			<p>
				By creating an ArrowNav account, personal information relating to the account, such as<br></br>
				username and password, will be kept confidential.<br></br>
				<br></br>
				The only data that will be analysed and collected when using ArrowNav will be<br></br>
				from submitting non-mandatory surveys. This data will be used to update how crowded<br></br>
				locations on campus are and update travel times when generating routes on the application.<br></br>
				<br></br>
				Should a user wish to delete their account, all information pertaining to that account,<br></br>
				such as username and password, will be deleted from ArrowNav databases.<br></br>
				<br></br>
				User account information is never shared or distributed to third parties.<br></br>
				<br></br>
				Cookies are used for this application. When agreeing to the privacy policy, you as a user agree<br></br>
				to allow cookies.<br></br>
				<br></br>
				By agreeing to this privacy policy, you are agreeing to all that has been mentioned above<br></br>
				and agree to abide by said policy.<br></br>
				
			</p>
			<button id = "accept-btn" type="submit">Accept</button>
			<button id = "decline-btn" type="submit">Don't Allow</button>
		</form>
	);
};

export default Privacy;