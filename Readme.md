# Weather API Test (DataTorque Technical Assessment)

## Overview
This solution was built as part of the **DataTorque Technical Assessment**.  
It consists of three parts:

1. **WeatherApi** – The main .NET 8 Web API that fetches real-time weather data from **OpenWeatherMap**.  
2. **WeatherApiTests** – Unit tests for validating the API responses and failure scenarios.  
3. **WeatherApi-Client** – A simple React UI (bonus) that consumes the Weather API and displays the forecast and recommendations.

---

## Features
- **Endpoint:** `/weather?lat={latitude}&lon={longitude}`
- **Response includes:**
  - Temperature (°C)
  - Wind speed (km/h)
  - Weather condition: *Sunny, Windy, Rainy, or Snowing*
  - Clothing recommendation based on rules:
    - Sunny → "Don't forget to bring a hat"
    - Temperature > 25°C → "It’s a great day for a swim"
    - Temperature < 15°C and raining/snowing → "Don't forget to bring a coat"
    - Raining → "Don’t forget the umbrella"
- Every **fifth request** returns a `503 Service Unavailable` (to simulate API failure).
- Unit tests cover both **success** and **error** responses.

---

## Technologies Used
- **.NET 8 (C#)**
- **ASP.NET Core Web API**
- **xUnit** for testing
- **OpenWeatherMap API**
- **React** (for WeatherApi-Client)
- **Visual Studio 2022**

---

## Example
**Request:**
http://localhost:5002/weather?lat=-41.2865&lon=174.7762


**Response:**
```json
{
  "temperature": 19.2,
  "windSpeed": 14.3,
  "condition": "Windy",
  "recommendation": "Don't forget the umbrella"
}

Improvements I would make with more time:

Better error handling:

Right now, if the external weather API fails, the app may throw an error. I would add proper try-catch blocks, logging, and retry mechanisms to make it more resilient.

Input validation & security:

I would validate latitude and longitude inputs to avoid invalid requests and add basic security measures like API key checks or rate limiting.

Performance improvements:

To reduce repeated API calls for the same city, I would implement caching so that frequent requests are faster.

More useful recommendations:

Currently, recommendations are basic. I would improve them by considering factors like wind, humidity, or user preferences for clothing.

Testing & maintainability:

I would add more unit and integration tests to cover edge cases. Also, refactor code to separate responsibilities more clearly for easier maintenance.

API documentation:

I would enhance Swagger/OpenAPI docs to include example responses, error codes, and instructions, making it easier for other developers to use.