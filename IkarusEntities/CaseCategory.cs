using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class CaseCategory
    {
        public CaseCategory()
        {
            CaseInfo = new HashSet<CaseInfo>();
        }

        public long CaseCategoryId { get; set; }
        public string CaseCategoryName { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<CaseInfo> CaseInfo { get; set; }
    }
}
