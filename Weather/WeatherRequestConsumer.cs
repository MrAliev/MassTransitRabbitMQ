using Bus.Contracts.Requests;
using Bus.Contracts.Responses;
using MassTransit;

namespace Weather
{
    public class WeatherRequestConsumer : IConsumer<WeatherRequest>
    {
        private readonly IWheatherService _service;
        private readonly ILogger<WeatherRequestConsumer> _logger;

        public WeatherRequestConsumer(IWheatherService service, ILogger<WeatherRequestConsumer> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<WeatherRequest> context)
        {
            _logger.LogDebug("Consume of {name} started. RequestId: {id}", nameof(WeatherRequest), context.RequestId);

            try
            {
                var result = _service.Get(context.Message.Count);

                await context.RespondAsync(new WeatherResponse
                {
                    WeatherForecasts = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Consume of {name} ended unsuccessfully. ", nameof(WeatherRequest));

                await context.RespondAsync(ex);
            }
        }
    }
}
