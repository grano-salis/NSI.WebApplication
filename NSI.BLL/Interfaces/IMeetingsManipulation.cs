using NSI.DC.MeetingsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Interfaces
{
    public interface IMeetingsManipulation
    {
        MeetingDto CreateMeeting(MeetingDto model);
        ICollection<MeetingDto> GetMeetings();
        MeetingDto EditMeeting(int meetingId, MeetingDto model);
        void RemoveMeeting(int meetingId);
        MeetingDto GetMeetingById(int id);
    }
}
