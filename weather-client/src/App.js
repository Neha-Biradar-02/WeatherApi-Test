import React, { useState } from "react";
import "./App.css";

function App() {
  const [lat, setLat] = useState("-41.2865");
  const [lon, setLon] = useState("174.7762");
  const [weather, setWeather] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const handleGetWeather = async () => {
    if (!lat || !lon) {
      setError("Please enter both latitude and longitude");
      return;
    }

    setLoading(true);
    setError("");
    setWeather(null);

    try {
      const response = await fetch(`http://localhost:5002/weather?lat=${lat}&lon=${lon}`);
      if (!response.ok) {
        throw new Error(`Server error: ${response.status}`);
      }
      const data = await response.json();
      setWeather(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  // Choose a dynamic background image based on condition
  const getBackgroundImage = () => {
    if (!weather) return "default-bg";
    switch (weather.condition.toLowerCase()) {
      case "sunny":
        return "sunny-bg";
      case "rainy":
        return "rainy-bg";
      case "snowing":
        return "snowy-bg";
      case "windy":
        return "windy-bg";
      default:
        return "default-bg";
    }
  };

  return (
    <div className={`app-container ${getBackgroundImage()}`}>
      <div className="weather-box">
        <h1>🌦️ Weather Insight</h1>

        <div className="input-container">
          <input
            type="number"
            placeholder="Latitude"
            value={lat}
            onChange={(e) => setLat(e.target.value)}
          />
          <input
            type="number"
            placeholder="Longitude"
            value={lon}
            onChange={(e) => setLon(e.target.value)}
          />
          <button onClick={handleGetWeather}>Get Weather</button>
        </div>

        {loading && <div className="loader"></div>}
        {error && <p className="error">{error}</p>}

        {weather && (
  <div className="weather-card">
    <h2>📍 Weather Details</h2>
    <p><strong>Temperature:</strong> {weather.temperature}°C</p>
    <p><strong>Wind Speed:</strong> {weather.windSpeed} km/h</p>  {/* <-- Added */}
    <p><strong>Condition:</strong> {weather.condition}</p>
    <p><strong>Recommendation:</strong> 🧭 {weather.recommendation}</p>
  </div>
)}
      </div>
    </div>
  );
}

export default App;
