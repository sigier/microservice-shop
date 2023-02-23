using Purchase.Aggregator.Models;
using Purchase.Aggregator.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurationManager configuration = builder.Configuration;


builder.Services.AddHttpClient<ICatalogueService, CatalogueService>(c =>
    c.BaseAddress = new Uri(configuration.GetSection("ApiSettings:CatalogUrl").Value));

builder.Services.AddHttpClient<ICartService, CartService>(c =>
    c.BaseAddress = new Uri(configuration.GetSection("ApiSettings:BasketUrl").Value));

builder.Services.AddHttpClient<IOrderService, OrderService>(c =>
    c.BaseAddress = new Uri(configuration.GetSection("ApiSettings:OrderingUrl").Value));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();


app.Run();
