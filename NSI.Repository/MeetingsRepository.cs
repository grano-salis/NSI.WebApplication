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

        public void Insert(MeetingDto model)
        {
            try
            {
                var entity_meeting = Mappers.MeetingsRepository.MapToDbEntity(model);
                _dbContext.Meeting.Add(entity_meeting);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                // log exception
                throw new Exception("Something went wrong with database");
            }
        }
    }
}
