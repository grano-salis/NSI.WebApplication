using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class CaseContact
    {
        public long CaseContactId { get; set; }
        public long CaseId { get; set; }
        public long Contact { get; set; }

        public CaseInfo Case { get; set; }
        public Contact ContactNavigation { get; set; }
    }
}
