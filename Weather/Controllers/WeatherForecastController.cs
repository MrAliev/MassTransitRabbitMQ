using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace Weather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWheatherService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IWheatherService service, ILogger<WeatherForecastController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var result = _service.Get();

            return result.ToArray();
        }
    }
}