namespace MinimalApis.Attributes;

public class GetAttribute : BaseRoutingAttribute
{
    public GetAttribute(string route) : base(route)
    {
    }
}