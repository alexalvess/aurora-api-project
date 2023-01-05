using Application.Envelop;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebApi.DependencyInjection.Transformers;
using System.Linq;
using System.Text.Json;

namespace WebApi.DependencyInjection.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseExceptionHandler(appBuilder => appBuilder.Run(async httpContext =>
        {
            const string ERROR_MESSAGE = "Ocorreu um erro! Estamos trabalhando para solucionar a indisponibilidade. Por favor, tente novamente mais tarde.";

            var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            httpContext.Response.StatusCode = exception switch
            {
                OperationCanceledException or TaskCanceledException => 499,
                _ => (int)HttpStatusCode.InternalServerError
            };

            await httpContext.Response.WriteAsJsonAsync(new Response(ERROR_MESSAGE));
        }));

    public static void ConfigureHealthChecks(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapHealthChecks("health/live", new HealthCheckOptions
        {
            Predicate = _ => true
        });

        routeBuilder.MapHealthChecks("health/ready", new HealthCheckOptions
        {
            Predicate = healthCheck => healthCheck.Tags.Contains("ready"),
            ResponseWriter = WriteResponse
        });
    }

    private static Task WriteResponse(HttpContext httpContext, HealthReport healthReport)
    {
        var slugify = new SlugyParametersTransformer();

        var body = new
        {
            status = healthReport.Status.ToString(),
            results = healthReport.Entries.Select(entry => new
            {
                status = entry.Value.Status.ToString(),
                description = slugify.TransformOutbound(entry.Key.Replace("HealthCheck", string.Empty))
            })
        };

        httpContext.Response.ContentType = "application/json";

        return httpContext.Response.WriteAsync(JsonSerializer.Serialize(body, options: new JsonSerializerOptions
        {
            WriteIndented = true
        }).ToString());
    }
}