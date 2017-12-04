using NSI.BLL.Interfaces;
using NSI.DC.HearingsRepository;
using NSI.DC.MeetingsRepository;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL
{
    public class UsersManipulation : IUsersManipulation
    {
        private readonly IUsersRepository _usersRepository;

        public UsersManipulation(IUsersRepository userRepository)
        {
            _usersRepository = userRepository;
        }

        public ICollection<UserMeetingDto> GetForMeetings(string username)
        {
            return _usersRepository.GetForMeetings(username);
        }

        public ICollection<UserHearingDto> GetForHearings(string username)
        {
            return _usersRepository.GetForHearings(username);
        }
    }
}
