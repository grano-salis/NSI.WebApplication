using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class UserHearing
    {
        public int UserHearingId { get; set; }
        public int HearingId { get; set; }
        public int UserId { get; set; }

        public Hearing Hearing { get; set; }
        public UserInfo User { get; set; }
    }
}
