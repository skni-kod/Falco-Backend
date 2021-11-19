using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.Helpers
{
    public class PaginationModel<T>
    {
            const int MaxPageSize = 50;
            private int _pageSize;
            public int PageSize
            {
                get => _pageSize;
                set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }

            public int CurrentPage { get; set; }
            public int TotalItems { get; set; }
            public int TotalPages { get; set; }
            public IList<T> Items { get; set; }

            public PaginationModel()
            {
                Items = new List<T>();
            }  
    }
}
