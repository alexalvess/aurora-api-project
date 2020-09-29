Feature: Worker
	The worker exists to map who work in the company and who have a PPE

@registerWorker
Scenario: Register a worker
	Given the worker's data:
	| Name          | BirthDate  | Nin         | Password |
	| James Willian | 1998-02-15 | 49638618019 | 123456   |
	When the register is done
	And i recover the worker by id
	Then the worker exists, returned, on the database

@updateWorker
Scenario: Update a worker
	Given an worker's data:
	| Name              | BirthDate  | Nin         | Password |
	| Alexander Jeffrey | 2000-06-09 | 38484649008 | 123456   |
	Then i insert this woker on database
	When i update this worker's name to 'Josef Carlos'
	And i update this worker's nin to '08396661014'
	Then i recover this worker
	And verify the worker's name
	And verify the worker's nin