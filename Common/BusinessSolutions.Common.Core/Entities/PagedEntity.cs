using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core
{
    public class PagedEntity<TEnttiy> where TEnttiy : class
    {
        public int TotalCount { get; private set; }

        public IReadOnlyList<TEnttiy> Items { get; private set; }

        public PagedEntity(IReadOnlyList<TEnttiy> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }

        public PagedEntity(IEnumerable<TEnttiy> items, int totalCount)
        {

            Items = items == null ? null : items.ToList().AsReadOnly();
            TotalCount = totalCount;
        }

    }
}
