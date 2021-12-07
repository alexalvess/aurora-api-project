using Application.DomainServices;
using Application.Ports.DomainServices;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
        => services
            .AddScoped<IOperatorDomainService, OperatorDomainService>();
}
