using System.Text.Json;
using WeatherApi.Entities;

namespace WeatherApi.Services
{
    public class OpenWeatherClient
    {
        private readonly string _apiKey;
        private readonly string _defaultState;
        private readonly string _defaultCountry;
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;
        private readonly string _defaultUnits;

        private const string GEO_PATH = "geo/1.0/direct";

        public OpenWeatherClient(IConfiguration configuration, HttpClient httpClient)
        {
            _apiKey = configuration["OpenWeather:ApiKey"] ?? throw new ArgumentNullException("OpenWeather:ApiKey");
            _defaultState = configuration["OpenWeather:DefaultState"] ?? throw new ArgumentNullException("OpenWeather:DefaultState");
            _defaultCountry = configuration["OpenWeather:DefaultCountry"] ?? throw new ArgumentNullException("OpenWeather:DefaultCountry");
            
            _defaultUnits = configuration["OpenWeather:DefaultUnits"] ?? "metric";

            _baseUrl = configuration["OpenWeather:BaseUrl"] ?? throw new ArgumentNullException("OpenWeather:BaseUrl");
            _httpClient = httpClient;
        }

        public async Task<Coordinates?> GetCoordinatesByLocation(string cityName)
        {
            var url = $"{_baseUrl}/{GEO_PATH}?q={cityName},{_defaultState},{_defaultCountry}&appid={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var results = JsonSerializer.Deserialize<List<OpenWeatherGeoResult>>(json);

            if (results != null && results.Count > 0)
            {
                var first = results[0];
                return new Coordinates { Latitude = first.lat, Longitude = first.lon };
            }

            return null;
        }

        public async Task<WeatherSummary?> GetWeatherByCoordinates(Coordinates coordinates)
        {
            var url = $"{_baseUrl}/data/2.5/weather?lat={coordinates.Latitude}&lon={coordinates.Longitude}&units={_defaultUnits}&appid={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var weatherResult = JsonSerializer.Deserialize<OpenWeatherResult>(json);

            if (weatherResult != null && weatherResult.main != null && weatherResult.weather != null && weatherResult.weather.Length > 0)
            {
                return new WeatherSummary
                {
                    TemperatureC = (int)weatherResult.main.temp,
                    Summary = weatherResult.weather[0].main
                };
            }

            return null;
        }

        private class OpenWeatherGeoResult
        {
            public double lat { get; set; }
            public double lon { get; set; }
        }

        private class OpenWeatherResult
        {
            public MainInfo main { get; set; }
            public WeatherInfo[] weather { get; set; }
        }

        private class MainInfo
        {
            public double temp { get; set; }
        }

        private class WeatherInfo
        {
            public string main { get; set; }
        }
    }
}
