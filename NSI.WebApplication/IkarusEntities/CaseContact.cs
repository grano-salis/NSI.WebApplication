using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class CaseContact
    {
        public long CaseContactId { get; set; }
        public int CaseId { get; set; }
        public int Contact { get; set; }

        public CaseInfo Case { get; set; }
        public Contact ContactNavigation { get; set; }
    }
}
