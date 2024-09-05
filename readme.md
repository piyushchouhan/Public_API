# Public_API

## Overview

**Public_API** is a robust ASP.NET Core Web API designed to provide current weather information for specified cities. Leveraging external weather services and Entity Framework Core for data management, this API ensures efficient data retrieval and storage. The application follows best practices in software architecture, including the use of dependency injection, error handling, and comprehensive unit testing with xUnit and Moq.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
- [Usage](#usage)
  - [Running the Application](#running-the-application)
  - [API Endpoints](#api-endpoints)
- [Database Migrations](#database-migrations)
- [Testing](#testing)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## Features

- **Weather Data Retrieval:** Fetches current weather data from an external API based on city names.
- **Data Persistence:** Stores weather data, including location and current conditions, in a SQL Server database using Entity Framework Core.
- **Error Handling:** Implements comprehensive error handling to manage various failure scenarios gracefully.
- **Dependency Injection:** Utilizes dependency injection for better modularity and testability.
- **Unit Testing:** Includes unit tests with xUnit and Moq to ensure reliability and correctness of the API endpoints.
- **API Documentation:** Provides Swagger/OpenAPI documentation for easy exploration and testing of API endpoints.

## Technologies Used

- **.NET 6:** Framework for building the Web API.
- **ASP.NET Core:** For creating RESTful API endpoints.
- **Entity Framework Core:** ORM for database interactions.
- **SQL Server:** Database for storing weather data.
- **HttpClient:** For making HTTP requests to external weather APIs.
- **Newtonsoft.Json:** For JSON serialization and deserialization.
- **xUnit:** Testing framework for unit tests.
- **Moq:** Mocking library for creating mock objects in tests.
- **Swagger/OpenAPI:** For API documentation and testing.

## Project Structure

```
Public_API/
│
├── Public_API/
|   |──Controllers/
│   |    └── WeatherController.cs
│   ├── Program.cs
|   |── appsettings.json
|
├── Data/
│   ├── ApplicationDbContext.cs
│   └── Models/
│       ├── Location.cs
│       ├── Current.cs
│       └── Condition.cs
│
├── Services/
│   ├── IWeatherService.cs
│   └── WeatherService.cs
│
├── Controller.test/
│   └── ControllerTests.cs
│
└── README.md
```

## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed on your machine:

- **.NET SDK** (version 6.0 or higher)
- **Visual Studio 2022** or **Visual Studio Code**
- **SQL Server** (Express or full version) or **LocalDB**
- **Postman** (optional, for testing API endpoints)

### Installation

1. **Clone the Repository**

   ```bash
   git clone https://github.com/piyushchouhan/Public_API.git
   cd Public_API
   ```

2. **Restore NuGet Packages**

   Navigate to the project directory and restore the required packages:

   ```bash
   dotnet restore
   ```

3. **Build the Solution**

   ```bash
   dotnet build
   ```

### Configuration

1. **Configure Connection String**

   Open `appsettings.json` and update the `ConnectionStrings` section with your SQL Server details:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=YOUR_DATABASE_NAME;Integrated Security=True;"
     },
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*"
   }
   ```

2. **Set Weather API Key**

   Ensure that the `WeatherService` is using a valid API key. You can store the API key in `appsettings.json` for better security:

   ```json
   {
     "WeatherApi": {
       "Key": "YOUR_WEATHER_API_KEY"
     },
     // ... other settings
   }
   ```

   Update `WeatherService.cs` to read the API key from configuration:

   ```csharp
   public WeatherService(HttpClient httpClient, ApplicationDbContext context, IConfiguration configuration)
   {
       _httpClient = httpClient;
       _context = context;
       _apiKey = configuration["WeatherApi:Key"];
   }
   
   // Use _apiKey in the API request
   var response = await _httpClient.GetAsync($"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={city}&aqi=no");
   ```

## Usage

### Running the Application

1. **Create Database Migrations**

   First create database migration scipt associated with schema:

   ```bash
   add-migration weatherDb
   ```
2. **Apply Database Migrations**

   Ensure the database is up-to-date with the latest schema:

   ```bash
   update-database --verbose
   ```

2. **Run the Application**

   ```bash
   dotnet run
   ```

   The API will start, and Swagger UI will be available at `https://localhost:{PORT}/swagger` for exploration and testing.

### API Endpoints

#### Get Current Weather

- **Endpoint:** `GET /api/weather`
- **Description:** Retrieves current weather information for a specified city.
- **Parameters:**
  - `city` (query string) - Name of the city.
- **Response:**
  - `200 OK` with weather data.
  - `400 Bad Request` if the city name is invalid.
  - `503 Service Unavailable` if the weather service is unreachable.
  - `500 Internal Server Error` for unexpected errors.

**Example Request:**

```http
GET https://localhost:5001/api/weather?city=London
```

**Example Response:**

```json
{
  "location": {
    "name": "London",
    "region": "City of London, Greater London",
    // ... other location details
  },
  "current": {
    "temp_c": 17.2,
    "condition": {
      "text": "Overcast",
      "icon": "//cdn.weatherapi.com/weather/64x64/night/122.png",
      "code": 1009
    },
    // ... other current weather details
  }
}
```

## Database Migrations

The project uses Entity Framework Core for database operations. Follow these steps to manage migrations:

1. **Add a New Migration**

   Whenever you make changes to the `ApplicationDbContext` or your models, add a new migration:

   ```bash
   dotnet ef migrations add YourMigrationName --verbose
   ```

2. **Update the Database**

   Apply the latest migrations to the database:

   ```bash
   dotnet ef database update --verbose
   ```

3. **List Pending Migrations**

   To check for any pending migrations:

   ```bash
   dotnet ef migrations list
   ```

## Testing

The project includes unit tests for the `WeatherController` using xUnit and Moq. The tests cover various scenarios, including successful data retrieval and handling of different exceptions.

### Running Tests

1. **Navigate to the Test Project**

   ```bash
   cd Tests
   ```

2. **Run Tests**

   ```bash
   dotnet test
   ```

### Test Coverage

- **Success Scenario:** Ensures that the `GetWeather` endpoint returns `200 OK` with correct data when the service successfully retrieves weather information.
- **Service Unavailable:** Validates that a `503 Service Unavailable` response is returned when an `HttpRequestException` occurs.
- **Bad Request:** Checks that a `400 Bad Request` response is returned when an `ArgumentException` is thrown due to invalid input.
- **Internal Server Error:** Confirms that a `500 Internal Server Error` is returned for unexpected exceptions.

## Contributing

Contributions are welcome! Follow these steps to contribute to the project:

1. **Fork the Repository**

   Click the "Fork" button on the repository page to create your own copy.

2. **Clone Your Fork**

   ```bash
   git clone https://github.com/piyushchouhan/Public_API.git
   cd Public_API
   ```

3. **Create a New Branch**

   ```bash
   git checkout -b feature/YourFeatureName
   ```

4. **Make Changes**

   Implement your feature or bug fix.

5. **Commit Your Changes**

   ```bash
   git commit -m "Add feature: YourFeatureName"
   ```

6. **Push to Your Fork**

   ```bash
   git push origin feature/YourFeatureName
   ```

7. **Create a Pull Request**

   Navigate to the original repository and create a pull request from your fork.

## License

This project is licensed under the [MIT License](LICENSE).

## Contact

For any questions or support, please contact:

- **Name:** Piyush Chauhan
- **Email:** piyushnchouhan@gmail.com
- **GitHub:** [piyushchouhan](https://github.com/piyushchouhan)

---

*This README was generated to provide comprehensive guidance on setting up, using, and contributing to the Public_API project. For further assistance, please refer to the official documentation or contact the maintainer.*