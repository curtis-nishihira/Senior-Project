CREATE TABLE Logging (
	logs varchar(500),
	UtcTimeStamp datetimeoffset(0),
	logLevel varchar(100),
	userPerformingOperator varchar(500),
	category varchar(250)
	);
