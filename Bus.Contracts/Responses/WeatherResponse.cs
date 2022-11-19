using SharedModels;


namespace Bus.Contracts.Responses
{
    public record WeatherResponse
    {
        public IEnumerable<WeatherForecast> WeatherForecasts { get; init; } = new List<WeatherForecast>();
    }
}
