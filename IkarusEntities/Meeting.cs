using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Meeting
    {
        public Meeting()
        {
            UserMeeting = new HashSet<UserMeeting>();
        }

        public int MeetingId { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
        public int CreatedByUserId { get; set; }

        public UserInfo CreatedByUser { get; set; }
        public ICollection<UserMeeting> UserMeeting { get; set; }
    }
}
