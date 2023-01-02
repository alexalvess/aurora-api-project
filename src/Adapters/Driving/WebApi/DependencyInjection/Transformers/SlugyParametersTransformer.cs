using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace WebApi.DependencyInjection.Transformers;

public class SlugyParametersTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object value)
        => Regex.Replace(value.ToString() ?? string.Empty, "([a-z])([A-Z])", "$1-$2").ToLower();
}
