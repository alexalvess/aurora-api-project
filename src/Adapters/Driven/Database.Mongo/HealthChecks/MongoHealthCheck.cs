using DataBase.Mongo.Context;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace DataBase.Mongo.HealthChecks;

public class MongoHealthCheck : IHealthCheck
{
    private readonly IMongoContext _mongoContext;

    public MongoHealthCheck(IMongoContext mongoContext)
    {
        _mongoContext = mongoContext;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var isHealthy = await CheckConnectionAsync();

        if (isHealthy)
            return HealthCheckResult.Healthy("MongoDb healthy");
        return HealthCheckResult.Unhealthy("MongoDb Unhealthy");
    }

    private async Task<bool> CheckConnectionAsync()
    {
        try
        {
            await _mongoContext.Database.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
            return true;
        }
        catch
        {
            return false;
        }
    }
}
