using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, configBuilder) =>
{
    configBuilder.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);

});

builder.Services.AddOcelot()
    .AddCacheManager(settings => settings.WithDictionaryHandle());


builder.Host.ConfigureLogging((hostingContext, logBuilder) =>
{
    logBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    logBuilder.AddConsole();
    logBuilder.AddDebug();

});

builder.Services.AddOcelot();

var app = builder.Build();
await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
