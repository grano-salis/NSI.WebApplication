using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class UserCase
    {
        public long UserCaseId { get; set; }
        public bool IsPrimary { get; set; }
        public long CaseId { get; set; }
        public long UserId { get; set; }

        public CaseInfo Case { get; set; }
        public UserInfo User { get; set; }
    }
}
