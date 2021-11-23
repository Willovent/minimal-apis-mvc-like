using MinimalApis.Attributes;
using MinimalApis.Services;

namespace MinimalApis.Endpoints;

[Endpoint]
public static class TotoEndpoint
{

    [Get("home/{name}")]
    [Get("Toto/{name}")]
    public static async Task<IResult> GetGreetings(string name, Service service)
    {
        return Results.Ok(new { greeting = service.GetHelloWorld() + $" {name}" });
    }

    [Post("home")]
    public static async Task<IResult> PostGreetings(GreetingModel model, Service service)
    {
        return Results.Ok(await service.GreetAsync(model.Name));
    }
}
