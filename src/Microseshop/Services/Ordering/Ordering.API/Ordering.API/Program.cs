using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


builder.Services.AddControllers();
builder.Services.AddAppServices();
builder.Services.AddInfrastructureServices(configuration);



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


