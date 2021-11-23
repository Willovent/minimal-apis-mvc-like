using System.Linq.Expressions;
using MinimalApis.Attributes;

namespace MinimalApis.Helpers
{
    public static class EndpointHelper
    {
        public static void ConfigureEndpoints(this WebApplication app, Type pointer)
        {
            var endpoints = pointer.Assembly.GetTypes().Where(x => x.GetCustomAttributes(false).Any(y => y.GetType() == typeof(EndpointAttribute)));
            var allowedAttributes = new[] { typeof(GetAttribute), typeof(PostAttribute) };
            var methodWithAttributes = endpoints
                .SelectMany(endpoint => endpoint.GetMethods()
                .Select(methodInfo => new
                {
                    EndpointAttribut = (EndpointAttribute)endpoint.GetCustomAttributes(false).First(x => x.GetType() == typeof(EndpointAttribute)),
                    Method = methodInfo,
                    Attributes = methodInfo.GetCustomAttributes(false).Where(attribute => allowedAttributes.Contains(attribute.GetType())).Cast<BaseRoutingAttribute>()
                })
                .Where(x => x.Attributes.Any()));

            foreach (var methodWithAttribute in methodWithAttributes)
            {
                var types = methodWithAttribute.Method.GetParameters().Select(p => p.ParameterType);
                types = types.Concat(new[] { methodWithAttribute.Method.ReturnType });
                foreach (var attribute in methodWithAttribute.Attributes)
                {
                    switch (attribute)
                    {
                        case GetAttribute get:
                            app.MapGet(get.Route, Delegate.CreateDelegate(Expression.GetFuncType(types.ToArray()), methodWithAttribute.Method));
                            break;
                        case PostAttribute post:
                            app.MapPost(post.Route, Delegate.CreateDelegate(Expression.GetFuncType(types.ToArray()), methodWithAttribute.Method));
                            break;
                    }
                }
            }
        }
    }
}
