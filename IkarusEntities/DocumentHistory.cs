using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class DocumentHistory
    {
        public int DocumentHistoryId { get; set; }
        public int ModifiedByUserId { get; set; }
        public int DocumentId { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string DocumentPath { get; set; }
        public string DocumentTitle { get; set; }
        public string CaseNumber { get; set; }
        public string DocumentCategoryName { get; set; }
        public string DocumentDescription { get; set; }

        public Document Document { get; set; }
        public UserInfo ModifiedByUser { get; set; }
    }
}
