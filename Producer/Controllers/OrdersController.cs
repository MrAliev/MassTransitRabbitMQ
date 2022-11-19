using Bus.Contracts.Requests;
using Bus.Contracts.Responses;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRequestClient<WeatherRequest> _requestClient;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IRequestClient<WeatherRequest> requestClient, IPublishEndpoint publishEndpoint, ILogger<OrdersController> logger)
        {
            _requestClient = requestClient;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            await _publishEndpoint.Publish<OrderCreated>(new
            {
                Id = 1,
                orderDto.ProductName,
                orderDto.Quantity,
                orderDto.Price
            });

            try
            {
                using var request = _requestClient.Create(new WeatherRequest {Count = 7});

                var timeStart = DateTime.Now;

                var response = await request.GetResponse<WeatherResponse>();

                var timeRequest = DateTime.Now - timeStart;

                _logger.LogWarning("Total Request time = {time}", timeRequest);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
    }
}
