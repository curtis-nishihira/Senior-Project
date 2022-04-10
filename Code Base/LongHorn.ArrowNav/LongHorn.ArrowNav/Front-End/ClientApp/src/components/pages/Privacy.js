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
				By creating an ArrowNav account, personal information relating to the account, such as
				username and password, will be kept confidential.

				The only data that will be analysed and collected when using ArrowNav will be
				from submitting non-mandatory surveys. This data will be used to update how crowded
				locations on campus are and update travel times when generating routes on the application.

				Should a user wish to delete their account, all information pertaining to that account,
				such as username and password, will be deleted from ArrowNav databases.

				User account information is never shared or distributed to third parties.

				Cookies are used for this application. When agreeing to the privacy policy, you as a user agree
				to allow cookies.

				By agreeing to this privacy policy, you are agreeing to all that has been mentioned above
				and agree to abide by said policy.
				
			</p>
			<button type="submit">Accept</button>
			<button type="submit">Don't Allow</button>
		</form>
	);
};

export default Privacy;