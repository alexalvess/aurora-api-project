using Application.DependencyInjection.Extensions;
using Application.Envelop;
using DataBase.Mongo.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization.Conventions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;
using WebApi.DependencyInjection.Extensions;
using WebApi.DependencyInjection.Transformers;
using WebApi.Filters;
using WebApi.HostedServices;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseDefaultServiceProvider((context, provider) =>
    {
        provider.ValidateScopes =
            provider.ValidateOnBuild =
                context.HostingEnvironment.IsDevelopment();
    })
    .ConfigureAppConfiguration((context, configurationBuilder) =>
    {
        configurationBuilder
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddRouting(options => options.LowercaseUrls = false);

        services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new SlugyParametersTransformer()));
            options.SuppressAsyncSuffixInActionNames = true;
        });

        services.AddHostedService<VerifyExperiedPpeService>();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services
            .AddMongoDbContext()
            .AddMongoDbRepositories()
            .AddMongoDbServices();

        services
            .AddDomainServices();

        services
            .AddNotificationContext();

        services
            .AddScoped<Queryable>();
    });

using var app = builder.Build();

try
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ConfigureExceptionHandler();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    await app.RunAsync();
}
catch (Exception ex)
{
    await app.StopAsync();
}
finally
{
    await app.DisposeAsync();
}