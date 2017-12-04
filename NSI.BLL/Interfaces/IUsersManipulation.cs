using NSI.DC.HearingsRepository;
using NSI.DC.MeetingsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Interfaces
{
    public interface IUsersManipulation
    {
        ICollection<UserMeetingDto> GetForMeetings(string username);
        ICollection<UserHearingDto> GetForHearings(string username);
    }
}
