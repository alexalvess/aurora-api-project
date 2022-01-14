using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Pagination
{
    public interface IPageInfo
    {
        long TotalPages { get; }

        bool HasPrevious { get; }

        bool HasNext { get; }
    }
}
