# BoyumIT.TodoApi# BoyumIT.TodoApi

BoyumIT.TodoApi is a RESTful Web API developed using .NET 8, designed to manage a simple Todo list. It supports operations such as 
creating a new Todo item, retrieving all Todo items, getting the details of a specific Todo item,
and updating a specific Todo item. The API adheres to RESTful design principles, ensuring clean and readable code.

## Features

- Create a new Todo item
- Get a List of All Todo Items
- Get the details of a specific Todo item by its ID
- Update a specific Todo item

## Requirements

- .NET 8 SDK 
- Visual Studio 2022 or any compatible IDE that supports .NET 8

## Getting Started

To run the BoyumIT.TodoApi on your local machine, follow these steps:

1. **Clone the Repository:** "git clone https://github.com/Doram13/BoyumIT.TodoApi"

2. **Open the project in Visual Studio 2022 or any compatible IDE.**

3. **Build the project:** "dotnet build"

4. **Run the project:** "dotnet run"

The API will start on `https://localhost:5001` and `http://localhost:5000` by default.

## Testing the API

The solution includes a unit test project (`UnitTests`) to ensure the functionality of the API. To run these tests, follow these steps:

1. **Navigate to the Test Project Directory:** "cd UnitTests"

2. **Run the Tests:** "dotnet test"

3. **View the Test Results**
   The test results will be displayed in the console.

## API Endpoints

The API exposes the following endpoints:

- `GET /api/todoItems` - Get all Todo items
- `GET /api/todoItems/{id}` - Get a specific Todo item by its ID
- `POST /api/todoItems` - Create a new Todo item
- `PUT /api/todoItems/{id}` - Updates a specific todo item.
- `DELETE /api/todoItems/{id}` - Deletes a specific todo item.
- `PUT /api/todoItems/{id}/Title` - Updates the title of a specific todo item.
- `PUT /api/todoItems/{id}/Description` - Updates the description of a specific todo item.
- `PUT /api/todoItems/{id}/Status` - Updates the status of a specific todo item.
## Contact

For any questions or feedback, feel free to reach out to me at 1991doram@gmail.com

## Acknowledgements

- [Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-8.0)
- [RESTful API Design](https://restfulapi.net/)

## Architecture

The API follows a clean architecture pattern, separating the application into layers based on their responsibilities.
The layers are currently represented as the following folders: Controllers for "Presentation", Services for "Business Logic",
and Models for 'Domain'. If a different DB (Sql or NoSQL for example) was to be used then a new Data Access Layer could 
be created too.

## Improvements

- Add new features as: Due Date with email notification, priority, tag system and relations between tasks, as blocking, or preceeds.
- Implement a Data Access Layer to interact with a database
- Add Authentication and Authorization
- Implement Caching for better performance
- Add more Logging for better monitoring
- Implement a CI/CD pipeline for automated testing and deployment
- Add Functional/Integration Tests to test the API end-to-end, and add Performance Tests.
- Implement a front-end application to consume the API
- Add more features such as sorting, filtering, and pagination
- Implement a global exception handler to handle errors
- Implement a versioning strategy for the API
- Implement a rate-limiting mechanism to prevent abuse
- Implement a health check endpoint
- Add more server-side validation for input data.