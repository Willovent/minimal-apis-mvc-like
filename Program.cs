using MinimalApis.Endpoints;
using MinimalApis.Helpers;
using MinimalApis.Services;

var appBuider = WebApplication.CreateBuilder(args);
appBuider.Services.AddScoped<Service>();

var app = appBuider.Build();

app.ConfigureEndpoints(typeof(TotoEndpoint));

app.Run();

public record GreetingModel(string Name);
