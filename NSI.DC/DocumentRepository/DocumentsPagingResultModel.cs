using NSI.DC.DocumentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Models
{
    public class DocumentsPagingResultModel
    {
        public DocumentsPagingResultModel()
        {
            Results = new List<DocumentDto>();
        }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public IList<DocumentDto> Results { get; set; }
    }
}
