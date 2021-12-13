using Application.DomainServices;
using Application.NotificationPattern;
using Application.Ports.DomainServices;
using Application.Ports.NotificationServices;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
        => services
            .AddScoped<IOperatorDomainService, OperatorDomainService>();

    public static IServiceCollection AddNotificationContext(this IServiceCollection services)
        => services
            .AddScoped<INotificationContext, NotificationContext>();
}
