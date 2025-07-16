# WeatherApi (.NET 8)

A simple ASP.NET Core Web API that provides weather forecast data. This project targets .NET 8 and demonstrates basic API setup, controller usage, and Swagger integration.

## Features

- RESTful endpoint for retrieving weather forecasts
- CORS enabled for frontend integration (default: `http://localhost:4200`)
- Swagger/OpenAPI documentation for easy API exploration
- HTTPS redirection and basic authorization middleware

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- (Optional) [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

### Running the API

1. Clone the repository.
2. Navigate to the project directory.
3. Run the API:

dotnet run --project WeatherApi


4. Access Swagger UI at [https://localhost:5001/swagger](https://localhost:5001/swagger) (port may vary).

### API Usage

#### GET /WeatherForecast

Returns a list of 5 weather forecasts with random temperature and summary.

**Example Response:**

[ { "date": "2025-07-17", "temperatureC": 23, "summary": "Mild" }, ... ]


## Configuration

- CORS origin can be changed in `Program.cs` (`WithOrigins` method).
- Swagger is enabled in development mode.

## Project Structure

- `Controllers/WeatherForecastController.cs`: Main API endpoint.
- `Program.cs`: Application startup and middleware configuration.


## TODO

- Proper validation and error handling
- Integration tests