using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Pagination
{
    public record Paging
    {
        private const int DefaultLimit = 10;
        private int _limit;

        public int Limit
        {
            get => _limit > 0 ? _limit : DefaultLimit;
            set => _limit = value;
        }

        public int Offset { get; init; }
    }
}
