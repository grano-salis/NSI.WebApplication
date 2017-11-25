using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.MeetingsRepository
{
    [DataContract]
    public class MeetingDto
    {
        [DataMember]
        public int MeetingId { get; set; }

        [DataMember]
        public DateTime From { get; set; }

        [DataMember]
        public DateTime? To { get; set; }

        [DataMember]
        public IEnumerable<UserMeetingDto> UserMeeting { get; set; }
    }
}
