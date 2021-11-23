namespace MinimalApis.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class BaseRoutingAttribute : Attribute
{
    public string Route { get; set; }
    public BaseRoutingAttribute(string route)
    {
        this.Route = route;
    }
}