using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataBase.Mongo.Abstractions.Repositories;

public interface IMongoRepository
{
    Task<TCollection> FindAsync<TCollection>(Expression<Func<TCollection, bool>> predicate, CancellationToken cancellationToken)
        where TCollection : class;

    Task<IEnumerable<TCollection>> GetAllAsync<TCollection>(List<string> fields, CancellationToken cancellationToken)
        where TCollection : class;

    Task Upsert<TCollection>(Expression<Func<TCollection, bool>> predicate, TCollection replacementCollection, CancellationToken cancellationToken)
        where TCollection : class;

    Task SaveAsync<TCollection>(TCollection collection, CancellationToken cancellationToken)
        where TCollection : class;

    Task SaveManyAsync<TCollection>(IEnumerable<TCollection> collections, CancellationToken cancellationToken)
        where TCollection : class;
}