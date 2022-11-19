using Bus.Contracts.Requests;
using MassTransit;
using Weather;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMassTransit(x =>
{
    x.AddBus(context => MassTransit.Bus.Factory.CreateUsingRabbitMq(c =>
    {
        c.ReceiveEndpoint(nameof(WebApplication), e =>
        {
            e.ConfigureConsumer(context, typeof(WeatherRequestConsumer));
        });
    }));

    x.AddConsumer<WeatherRequestConsumer>();
    x.AddRequestClient<WeatherRequest>();
});

// Add services to the container.
builder.Services.AddTransient<IWheatherService, WheatherServiceService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
