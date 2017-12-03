using NSI.DC.MeetingsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface IUsersRepository
    {
        ICollection<UserMeetingDto> GetForMeetings(string username);
    }
}
