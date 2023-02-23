using EventBus.Messages.Common;
using MassTransit;
using Ordering.API.EventBusConsumer;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


builder.Services.AddControllers();
builder.Services.AddAppServices();
builder.Services.AddInfrastructureServices(configuration);

builder.Services.AddMassTransit(config => {

    config.AddConsumer<CartCheckoutConsumer>();

    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(configuration.GetSection("EventBusSettings:HostAddress").Value);

        cfg.ReceiveEndpoint(EventBusConstants.CART_CHECKOUT_QUEUE_NAME, c =>
        {
            c.ConfigureConsumer<CartCheckoutConsumer>(ctx);
        });
    });
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<CartCheckoutConsumer>();



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using OrderContext ctx = app.Services.CreateScope().ServiceProvider.GetService<OrderContext>();
    OrderContextSeed.SeedData(ctx);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();


app.Run();


