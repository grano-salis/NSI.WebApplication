using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class UserMeeting
    {
        public int UserMeetingId { get; set; }
        public int MeetingId { get; set; }
        public int UserId { get; set; }

        public Meeting Meeting { get; set; }
        public UserInfo User { get; set; }
    }
}
