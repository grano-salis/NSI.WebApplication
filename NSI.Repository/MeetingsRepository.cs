using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.MeetingsRepository;
using IkarusEntities;
using System.Linq;

namespace NSI.Repository
{
    public partial class MeetingsRepository : IMeetingsRepository
    {
        private readonly IkarusContext _dbContext;

        public MeetingsRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public MeetingDto Insert(MeetingDto model)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var userIds = new List<int>();
                    var entity_meeting = Mappers.MeetingsRepository.MapToDbEntity(model);
                    foreach (var userMeeting in model.UserMeeting)
                    {
                        entity_meeting.UserMeeting.Add(Mappers.MeetingsRepository.MapToDbEntity(userMeeting.UserId, entity_meeting.MeetingId));
                        userIds.Add(userMeeting.UserId);
                    }
                    _dbContext.Meeting.Add(entity_meeting);
                    if (_dbContext.SaveChanges() != 0)
                    {
                        transaction.Commit();
                        return Mappers.MeetingsRepository.MapToDto(entity_meeting, _dbContext.UserInfo.Where(x => userIds.Contains(x.UserId)));
                    }
                    return null;
                }
                catch(Exception)
                {
                    transaction.Rollback();
                    throw new Exception("Something went wrong with database");
                }
            }
        }
    }
}
