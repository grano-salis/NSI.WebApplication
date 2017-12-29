using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSI.DC.MeetingsRepository;

namespace NSI.REST.Models
{
    public class MeetingsSearchModel
    {
        public int MeetingId { get; set; }

        public string Title { get; set; }

        public DateTime? To { get; set; }

        public DateTime From { get; set; }

        public IEnumerable<UserMeetingDto> UserMeeting { get; set; }
    }
}
