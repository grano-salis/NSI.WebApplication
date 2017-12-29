using System;

namespace NSI.DC.DocumentRepository
{
    public class DocumentsPagingQueryModel
    {
        public int PageNumber { get; set; }
        public int ResultsPerPage { get; set; }
        public string SearchByTitle { get; set; }
        public int? SearchByCaseId { get; set; }
        public int? SearchByCategoryId { get; set; }
        public string SearchByDescription { get; set; }

        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
        public DateTime? ModifiedDateFrom { get; set; }
        public DateTime? ModifiedDateTo { get; set; }

    }
}
