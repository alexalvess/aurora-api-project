using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Pagination
{
    public interface IPagedResult<out T>
    {
        IEnumerable<T> Items { get; }

        IPageInfo PageInfo { get; }
    }
}
