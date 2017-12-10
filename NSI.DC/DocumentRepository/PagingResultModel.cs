using NSI.DC.DocumentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Models
{
    public class PagingResultModel<T> where T : class
    {
        public PagingResultModel()
        {
            Results = new List<T>();
        }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public IList<T> Results { get; set; }
    }
}
