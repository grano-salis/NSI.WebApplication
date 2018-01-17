using NSI.DC.Auth;
using NSI.DC.HearingsRepository;
using NSI.DC.MeetingsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface IUsersRepository
    {
        ICollection<UserMeetingDto> GetForMeetings(string username);
        ICollection<UserHearingDto> GetForHearings(string username);
        UserInfoDto GetUserInfoByUsername(string username);
    }
}
