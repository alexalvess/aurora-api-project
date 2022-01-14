using Application.Abstractions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Mongo.Abstractions.Repositories.Pagination
{
    public class PageInfo : IPageInfo
    {
        public long TotalPages { get; init; }

        public bool HasPrevious { get; init; }

        public bool HasNext { get; init; }
    }
}
