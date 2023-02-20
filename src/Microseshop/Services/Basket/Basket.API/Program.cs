using Basket.API.GrpcService;
using Basket.API.Repositories;
using Discount.GRPC.Protos;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
ConfigurationManager configuration = builder.Configuration;


builder.Services.AddSingleton<IConnectionMultiplexer>(c => 
    ConnectionMultiplexer.Connect(configuration.GetSection("CacheSettings:ConnectionString").Value));

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
    (o => o.Address = new Uri(configuration.GetSection("GrpcSettings:DiscountUrl").Value));
builder.Services.AddScoped<DiscountGrpcService>();



builder.Services.AddControllers();
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

