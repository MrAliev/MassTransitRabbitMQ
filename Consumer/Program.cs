using MassTransit;
using Newtonsoft.Json;
using SharedModels;

var busControl = MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.ReceiveEndpoint("order-created-event", e =>
    {
        e.Consumer<OrderCreatedConsumer>();
    });

});

await busControl.StartAsync();

try
{
    Console.WriteLine("Press enter to exit");

    await Task.Run(Console.ReadLine);
}
finally
{
    await busControl.StopAsync();
}

internal class OrderCreatedConsumer : IConsumer<OrderCreated>
{
    public Task Consume(ConsumeContext<OrderCreated> context)
    {
        var jsonMessage = JsonConvert.SerializeObject(context.Message);
        Console.WriteLine($"OrderCreated message: {jsonMessage}");
        return Task.CompletedTask;
    }
}
