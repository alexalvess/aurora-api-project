using Application.DependencyInjection.Extensions;
using Application.Envelop;
using DataBase.Mongo.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Filters;
using WebApi.HostedServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<ResponseFilter>();
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };

        options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Populate;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });

builder.Services.AddHostedService<VerifyExperiedPpeService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddMongoDbContext()
    .AddMongoDbRepositories()
    .AddMongoDbServices();

builder.Services
    .AddDomainServices();

builder.Services
    .AddNotificationContext();

builder.Services
    .AddScoped<Queryable>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var camelCaseConventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
ConventionRegistry.Register("CamelCase", camelCaseConventionPack, type => true);

app.UseExceptionHandler(appBuilder => appBuilder.Run(async httpContext =>
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();