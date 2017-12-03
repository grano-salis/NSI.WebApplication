using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.MeetingsRepository;
using IkarusEntities;
using System.Linq;

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
    }
}
