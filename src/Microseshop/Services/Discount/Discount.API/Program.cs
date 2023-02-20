using Discount.API.Repositories;
using Discount.API.Extensions;
using Discount.GRPC.Protos;



var builder = WebApplication.CreateBuilder(args);

IHost host = Host.CreateDefaultBuilder(args).Build();
host.MigrateData<Program>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
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

