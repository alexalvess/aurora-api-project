using Application.Abstractions.Pagination;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataBase.Mongo.Abstractions.Repositories.Pagination
{
    public class PagedResult<T> : IPagedResult<T>
    {
        private readonly IEnumerable<T> _items;
        private readonly long _totalItems;
        private readonly int _limit;
        private readonly int _offset;

        private PagedResult(IEnumerable<T> items, long totalItems, int limit, int offset)
        {
            _items = items;
            _limit = limit;
            _offset = offset;
            _totalItems = totalItems;
        }

        public IEnumerable<T> Items
            => _items.Take(_limit);

        public IPageInfo PageInfo
            => new PageInfo
            {
                HasNext = _items.Count() > _limit,
                HasPrevious = _offset > 0,
                TotalPages = _totalItems > 0 
                    ? (long)Math.Ceiling(_totalItems/(double)_limit)
                    : 0
            };

        public static async Task<IPagedResult<T>> CreateAsync(Paging paging, IQueryable<T> source, CancellationToken cancellationToken)
        {
            paging ??= new Paging();
            var items = await ApplyPagination(paging, source).ToListAsync(cancellationToken);
            return new PagedResult<T>(items, source.Count(), paging.Limit, paging.Offset);
        }

        private static IMongoQueryable<T> ApplyPagination(Paging paging, IQueryable<T> source)
            => source.Skip(paging.Limit * paging.Offset).Take(paging.Limit + 1) as IMongoQueryable<T>;
    }
}
