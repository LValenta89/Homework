Feature: Sensor Data Aggregator
	Should be able to count number of messages received from sensors. Data come in an array for all sensors combined.
	Component must be able to count data for unknow number of sensors and new data from new sensors might arrive
	at any time.

Scenario: Receive one message from one sensor
	Given that data sensor aggregator (DSA) is initialized
	When DSA receives 1 message(s) for Sensor1
	Then the result should be 1 for Sensor1
	And total messages received should be 1
	
Scenario: Receive 30 message from one sensor
	Given that data sensor aggregator (DSA) is initialized
	When DSA receives 30 message(s) for Sensor1
	Then the result should be 30 for Sensor1
	And total messages received should be 30
	
Scenario: Receive 15 messages from 3 sensors
	Given that data sensor aggregator (DSA) is initialized
	When DSA receives 5 message(s) for Sensor1
	And DSA receives 5 message(s) for Sensor2
	And DSA receives 5 message(s) for Sensor3
	Then the result should be 5 for Sensor1
	And the result should be 5 for Sensor2
	And the result should be 5 for Sensor3
	And total messages received should be 15