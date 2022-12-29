using Application.DependencyInjection.Extensions;
using Application.Envelop;
using DataBase.Mongo.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization.Conventions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using WebApi.DependencyInjection.Extensions;
using WebApi.Filters;
using WebApi.HostedServices;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((context, provider) =>
{
    provider.ValidateScopes =
        provider.ValidateOnBuild =
            context.HostingEnvironment.IsDevelopment();
});

builder.Host.ConfigureServices((context, services) =>
{
    services
        .AddControllers(options => options.Filters.Add<ResponseFilter>())
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Populate;
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
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

    var camelCaseConventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
    ConventionRegistry.Register("CamelCase", camelCaseConventionPack, type => true);

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