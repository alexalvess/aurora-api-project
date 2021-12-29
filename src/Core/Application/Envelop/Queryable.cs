using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Envelop
{
    public record Queryable
    {
        public bool Wrap { get; set; }

        public List<string> Fields { get; set; }

        public IDictionary<string, string> Filter { get; set; }

        public IList<string> Sort { get; set; }

        public IList<string> Search { get; set; }

        public Page Page { get; set; }

        public IDictionary<int, string> GetSortFields()
        {
            return Sort.Select(filed =>
            {
                var order = filed.StartsWith("-") ? -1 : 1;
                filed = filed[1..];

                return new KeyValuePair<int, string>(order, filed);
            }).ToDictionary(key => key.Key, value => value.Value);
        }
    }

    public record Page(long Offset, long Limit);
}
