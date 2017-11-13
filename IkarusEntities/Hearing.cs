using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Hearing
    {
        public long HearingId { get; set; }
        public DateTime HearingDate { get; set; }
        public string Note { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
        public long CaseId { get; set; }
        public long CreatedByUserId { get; set; }

        public CaseInfo Case { get; set; }
        public UserInfo CreatedByUser { get; set; }
    }
}
