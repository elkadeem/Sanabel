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
        private int _pageSize;        
        public BaseSearchViewModel() : this(10)
        {

        }

        public BaseSearchViewModel(int pageSize)
        {
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", "PageSize must be greate than Zero.");

            _pageSize = pageSize;
        }


        public StaticPagedList<T> Items { get; set; }

        public int PageIndex { get; set; }

        public int PageSize => _pageSize;
    }
}
