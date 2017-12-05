using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Models
{
    public class DocumentsPagingQueryModel
    {
        public string Search { get; set; }
        public int PageNumber { get; set; }
        public int ResultsPerPage { get; set; }
        public string FilterBy { get; set; }
    }
}
