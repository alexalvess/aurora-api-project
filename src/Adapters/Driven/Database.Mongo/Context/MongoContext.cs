using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Mongo.Context;

public class MongoContext : IMongoContext
{
    private readonly IMongoDatabase _mongoDatabase;

    public MongoContext(IConfiguration configuration)
    {
        var mongoUrl = new MongoUrl(configuration.GetConnectionString("MongoDb"));
        _mongoDatabase = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
    }

    public IMongoCollection<TCollection> GetCollection<TCollection>()
        => _mongoDatabase.GetCollection<TCollection>(typeof(TCollection).Name);
}