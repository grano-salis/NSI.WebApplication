using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class DocumentHistory
    {
        public int DocumentHistoryId { get; set; }
        public int ModifiedByUserId { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int DocumentId { get; set; }

        public Document Document { get; set; }
        public UserInfo ModifiedByUser { get; set; }
    }
}
