using NSI.DC.MeetingsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface IMeetingsRepository
    {
        MeetingDto InsertMeeting(MeetingDto model);
        MeetingDto UpdateMeeting(int meetingId, MeetingDto model);
        ICollection<MeetingDto> GetMeetings();
        void DeleteMeeting(int meetingId);
        MeetingDto GetMeetingById(int id);
    }
}
