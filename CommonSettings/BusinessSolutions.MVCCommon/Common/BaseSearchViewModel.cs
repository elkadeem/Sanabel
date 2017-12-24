using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.MVCCommon.Common
{
    public class BaseSearchViewModel<T> where T : class
    {
        public StaticPagedList<T> Items { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; } = 10;
    }
}
