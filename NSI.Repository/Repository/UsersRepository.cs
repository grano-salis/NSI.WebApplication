using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.MeetingsRepository;
using IkarusEntities;
using System.Linq;
using NSI.DC.HearingsRepository;
using NSI.DC.Exceptions;
using NSI.DC.Exceptions.Enums;
using NSI.DC.Auth;

namespace NSI.Repository.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IkarusContext _dbContext;

        public UsersRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<UserMeetingDto> GetForMeetings(string username)
        {
            return _dbContext.UserInfo.Where(x => x.Username.Contains(username))
                                        .Select(x => new UserMeetingDto()
                                        {
                                            UserId = x.UserId,
                                            UserName = x.Username
                                        })
                                        .ToList();
        }

        public ICollection<UserHearingDto> GetForHearings(string username)
        {
            return _dbContext.UserInfo.Where(x => x.Username.Contains(username))
                                        .Select(x => new UserHearingDto()
                                        {
                                            UserId = x.UserId,
                                            UserName = x.Username
                                        })
                                        .ToList();
        }

        public UserInfoDto GetUserInfoByUsername(string username)
        {
            if (username == null)
                throw new NSIException("Username is null", Level.Error, ErrorType.InvalidParameter);
            var userInfo = _dbContext.UserInfo.FirstOrDefault(x => x.Username == username);
            if (userInfo == null)
                throw new NSIException("No user for username = " + username, Level.Info, ErrorType.MissingData);
            return Mappers.UserInfoRepository.MapToDto(userInfo);
        }

    }
}
