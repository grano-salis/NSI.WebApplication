using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Models
{
    public class DocumentsPagingQueryModel
    {
        public int PageNumber { get; set; }
        public int ResultsPerPage { get; set; }
        public string SearchByTitle { get; set; }
        public int SearchByCaseId { get; set; }
        public DateTime SearchByDateFrom { get; set; }
        public DateTime SearchByDateTo { get; set; }
    }
}
