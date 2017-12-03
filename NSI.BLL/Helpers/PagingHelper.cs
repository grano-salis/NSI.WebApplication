using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSI.BLL.Helpers
{
    public static class PagingHelper<T>
    {
        public static ICollection<T> PagedList(ICollection<T> source, int pageNumber, int pageSize)
        {

            return source.Skip(pageSize * (pageNumber - 1))
                            .Take(pageSize)
                            .ToList();
        }
    }
}
