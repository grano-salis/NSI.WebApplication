using NSI.DC.MeetingsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Interfaces
{
    public interface IMeetingsManipulation
    {
        void CreateMeeting(MeetingDto model);
        ICollection<MeetingDto> GetMeetings();
        void EditMeeting(int meetingId, MeetingDto model);
        void RemoveMeeting(int meetingId);
        MeetingDto GetMeetingById(int id);
    }
}
