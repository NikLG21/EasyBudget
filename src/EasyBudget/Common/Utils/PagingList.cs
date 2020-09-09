using System;
using System.Collections.Generic;
using System.Text;

namespace EasyBudget.Common.Utils
{
    public class PagingList<T>
    {
        public List<T> Data { get; set; }
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
