namespace WeatherApi.Entities
{
    public class WeatherSummary
    {
        public string Location { get; set; } = string.Empty;
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
