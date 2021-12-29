using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Envelop
{
    public record Paging
    {
        public string First { get; init; }

        public string Last { get; init; }

        public string Next { get; init; }

        public string Previous { get; set; }
    }
}
