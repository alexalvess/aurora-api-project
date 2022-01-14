using Application.Abstractions.Pagination;
using DataBase.Mongo.Abstractions.Repositories.Pagination;
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
    Task<TCollection> FindAsync<TCollection>(
        Expression<Func<TCollection, bool>> predicate, 
        CancellationToken cancellationToken)
        where TCollection : class;

    Task<TResult> FindDynamicallyAsync<TCollection, TResult>(
        Expression<Func<TCollection, bool>> predicate,
        Func<IQueryable<TCollection>, IQueryable<TResult>> select,
        CancellationToken cancellationToken)
        where TCollection : class;

    Task<IPagedResult<TCollection>> GetAllAsync<TCollection>(
        Paging paging,
        CancellationToken cancellationToken)
        where TCollection : class;

    Task<IPagedResult<TResult>> GetAllDynamicallyAsync<TCollection, TResult>(
        Paging paging,
        Expression<Func<TCollection, bool>> predicate,
        Func<IQueryable<TCollection>, IOrderedQueryable<TCollection>> orderBy,
        Func<IQueryable<TCollection>, IQueryable<TResult>> select,
        CancellationToken cancellationToken)
        where TCollection : class;

    Task Upsert<TCollection>(Expression<Func<TCollection, bool>> predicate, TCollection replacementCollection, CancellationToken cancellationToken)
        where TCollection : class;

    Task SaveAsync<TCollection>(TCollection collection, CancellationToken cancellationToken)
        where TCollection : class;

    Task SaveManyAsync<TCollection>(IEnumerable<TCollection> collections, CancellationToken cancellationToken)
        where TCollection : class;
}