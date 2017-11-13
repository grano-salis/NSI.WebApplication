using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class UserMeeting
    {
        public long UserMeetingId { get; set; }
        public long MeetingId { get; set; }
        public long UserId { get; set; }

        public Meeting Meeting { get; set; }
        public UserInfo User { get; set; }
    }
}
