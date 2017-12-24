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
        ICollection<MeetingDto> SearchMeetings(MeetingDto searchCriteria, int pageNumber, int pageSize);
        ICollection<MeetingDto> GetMeetings(int? page, int? pageSize);
        ICollection<MeetingDto> GetMeetingsByUser(int userId);
        ICollection<MeetingTimeDto> GetMeetingTimes(ICollection<int> userIds, DateTime from, DateTime to, int meetingDuration);
        ICollection<MeetingDto> CheckUsersAvailability(ICollection<int> userIds, DateTime from, DateTime to);
    }
}
