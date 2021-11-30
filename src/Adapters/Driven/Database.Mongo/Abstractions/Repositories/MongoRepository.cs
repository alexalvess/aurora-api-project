﻿using DataBase.Mongo.Context;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataBase.Mongo.Abstractions.Repositories;

public abstract class MongoRepository : IMongoRepository
{
    private readonly IMongoContext _mongoContext;

    public MongoRepository(IMongoContext mongoContext)
        => _mongoContext = mongoContext;

    public Task<TCollection> FindAsync<TCollection>(Expression<Func<TCollection, bool>> predicate, CancellationToken cancellationToken)
        where TCollection : class
        => _mongoContext.GetCollection<TCollection>().AsQueryable().Where(predicate).FirstOrDefaultAsync(cancellationToken);

    public Task<List<TCollection>> GetAllAsync<TCollection>(CancellationToken cancellationToken)
        where TCollection : class
        => _mongoContext.GetCollection<TCollection>().AsQueryable().ToListAsync(cancellationToken);

    public Task Upsert<TCollection>(Expression<Func<TCollection, bool>> predicate, TCollection replacementCollection, CancellationToken cancellationToken)
        where TCollection : class
        => _mongoContext
            .GetCollection<TCollection>()
            .ReplaceOneAsync(
                filter: predicate,
                replacement: replacementCollection,
                options: new ReplaceOptions { IsUpsert = true },
                cancellationToken: cancellationToken);

    public Task SaveAsync<TCollection>(TCollection collection, CancellationToken cancellationToken)
        where TCollection : class
        => _mongoContext.GetCollection<TCollection>().InsertOneAsync(collection, cancellationToken: cancellationToken);

    public Task SaveManyAsync<TCollection>(IEnumerable<TCollection> collections, CancellationToken cancellationToken)
        where TCollection : class
        => _mongoContext.GetCollection<TCollection>().InsertManyAsync(collections, cancellationToken: cancellationToken);
}