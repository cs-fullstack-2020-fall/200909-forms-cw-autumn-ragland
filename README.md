# 20 09 09 Forms Classwork

## Set Up
- clone repo
- open in vscode
- accept extension suggestions
- restore packages
- view in browser

## Models
User
- id : unique integer for identifying a user
- first name : required field
- last name : required filed
- age : integer greater than 18
- user name : required field no more than 50 characters

## Endpoints
- View All Users 
	- pass all users in the database to a view
- View a User by ID
	- find a user by ID and pass to a view
	- if a user isn't found by ID return a view displaying an appropriate message 
- Display New User Form
	- render a view with a new user form
- Create a User
	- add a user to the database from an instance of the User model
	- if model is valid redirect to the view displaying all users in the database
	- if the model throws an error build a string from the model error and pass to the view displaying the New User Form
- Display Update User Form
	- find a user by ID and pass to a view that renders an update user form
	- if a user isn't found by ID return a view displaying an appropriate message
- Update a User
	- find a user by ID and update the found user in the database
	- if model is valid redirect to the view displaying all users in the database
	- if the model throws an error build a string from the model error and pass to the view displaying the New User Form
	- if the user is not found return a view displaying an appropriate message 
- Delete a User
	- find a user by ID and delete from database
	- if a user isn't found by ID return a view displaying an appropriate message

## Views
- View All Users
	- display a table of each users' full name and userName
- View User Detail
	- display one users full name, age, userName
	- display a button to edit a user
	- display a button to delete a user
- New User Form
	- display a form using HTML helper tags to create a new user
	- include all model properties except ID
	- pass form to the Create a User on submit
	- if form produces errors on submit, display errors above form
- Update User Form
	- display a form using HTML helper tags to update an existing user
	- include all model properties
	- user ID and user age should be read only form fields
	- pass form to the Update a User on submit
	- if form produces errors on submit, display errors above form
- Error
	- display simple errors, such as User Not Found, using View Data