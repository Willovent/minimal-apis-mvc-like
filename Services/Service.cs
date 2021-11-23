namespace MinimalApis.Services;

public class Service
{
    private static int CallNumber = 0;
    private int instanceCallNumber = 0;
    public string GetHelloWorld() => $"Hello World [globalCalls : {++CallNumber}, instanceCalls: {++instanceCallNumber}]";
    public Task<string> GreetAsync(string name) => Task.FromResult($"Hello {name}");
}