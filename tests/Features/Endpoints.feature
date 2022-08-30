Feature: Backend Endpoints 

The backend for getting information about transactions: smoke tests

Background: Logged into the backend rest API
	Given I'm logged in the backend rest api


Scenario: The one where I succesfully create a transaction
	When I create a positive transaction with the following values
	| field       | value                    |
	| title       | cupcake                  |
	| description | tasty cupcake            |
	| cost        | 2.5                     |
	| userId      | 630bca76f43104d1a1143efb |
	Then I should get a 200 response


Scenario: The one where I succesfully update a transaction
	Given I've created a transaction with the following values
	| field       | value          |
	| title       | burrito        |
	| description | delicious burrito  |
	| cost        | 5          |
	| userId      | 630bca76f43104d1a1143efb |
	When I change the description to tasty burrito
	Then I should get a 200 response 


Scenario: The one where I succesfully delete a transaction
	Given I'm logged in the backend rest api
	And I've created a transaction with the following values
	| field       | value          |
	| title       | Big Car      |
	| description | really expensive car  |
	| cost        | 50000          |
	| userId      | 630bca76f43104d1a1143efb |
	When I delete that transaction
	Then I should get a 200 response 


