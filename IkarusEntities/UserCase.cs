using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class UserCase
    {
        public int UserCaseId { get; set; }
        public bool IsPrimary { get; set; }
        public int CaseId { get; set; }
        public int UserId { get; set; }

        public CaseInfo Case { get; set; }
        public UserInfo User { get; set; }
    }
}
