using Application.Envelop;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

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
}