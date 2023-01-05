using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DataBase.Mongo.Context;

public class MongoContext : IMongoContext
{
    public MongoContext(IConfiguration configuration)
    {
        var mongoUrl = new MongoUrl(configuration.GetConnectionString("MongoDb"));
        Database = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
    }

    public IMongoDatabase Database { get; }

    public IMongoCollection<TCollection> GetCollection<TCollection>()
        => Database.GetCollection<TCollection>(typeof(TCollection).Name);
}