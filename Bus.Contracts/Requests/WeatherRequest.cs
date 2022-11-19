using MassTransit;

namespace Bus.Contracts.Requests
{
    public record WeatherRequest : IConsumer
    {
        public int Count { get; init; } = 5 ;
    }
}
