using Application.Ports.MongoServices;
using DataBase.Mongo.Context;
using DataBase.Mongo.Repositories.OperatorRepository;
using DataBase.Mongo.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DataBase.Mongo.DependencyInjection.Extensions;
 
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDbContext(this IServiceCollection services)
        => services.AddSingleton<IMongoContext, MongoContext>();

    public static IServiceCollection AddMongoDbRepositories(this IServiceCollection services)
        => services
            .AddTransient<IOperatorRepository, OperatorRepository>();

    public static IServiceCollection AddMongoDbServices(this IServiceCollection services)
        => services
            .AddScoped<IOperatorService, OperatorService>();
}