using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherSummaryController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherSummaryController> _logger;

        public WeatherSummaryController(ILogger<WeatherSummaryController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherSummary")]
        public WeatherSummary Get()
        {
            return new WeatherSummary
            {
                Date = DateOnly.FromDateTime(DateTime.Now.Date),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }

        [HttpPost(Name = "GetWeatherSummaryByCity")]
        public WeatherSummary Post([FromBody] WeatherByCityRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.City))
            {
                //return BadRequest("City is required.");
                throw new ArgumentException("City is required.", nameof(request.City));
            }

            return new WeatherSummary
            {
                Location = request.City,
                Date = DateOnly.FromDateTime(DateTime.Now.Date),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }
    }
}
