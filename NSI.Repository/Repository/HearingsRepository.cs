using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.HearingsRepository;
using IkarusEntities;

namespace NSI.Repository.Repository
{
    public partial class HearingsRepository : IHearingsRepository
    {
        private readonly IkarusContext _dbContext;

        public HearingsRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Insert(HearingDto model)
        {
            try
            {
                var entity_hearing = Mappers.HearingsRepository.MapToDbEntity(model);
                _dbContext.Hearing.Add(entity_hearing);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // log exception
                throw new Exception("Something went wrong with database");
            }
        }
    }
}
