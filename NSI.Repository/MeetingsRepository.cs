using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.MeetingsRepository;
using IkarusEntities;

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
            var entity = Mappers.MeetingsRepository.MapToDbEntity(model);
            _dbContext.Meeting.Add(entity);
            if(_dbContext.SaveChanges() != 0) return Mappers.MeetingsRepository.MapToDto(entity);
            return null;
        }
    }
}
