using Ordering.API.Extensions;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IHost host = Host.CreateDefaultBuilder(args).Build();
host.MigrateDatabase<OrderContext>((ctx, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed
        .SeedAsync(ctx, logger)
        .Wait();

});

builder.Services.AddControllers();
builder.Services.AddAppServices();
builder.Services.AddInfrastructureServices(configuration);



// Add services to the container.
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

app.MapControllers();


app.Run();


