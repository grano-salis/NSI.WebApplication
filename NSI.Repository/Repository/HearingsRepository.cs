using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.HearingsRepository;
using IkarusEntities;
using System.Linq;

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
                Logger.Logger.LogError(ex.Message);
                throw new Exception("Something went wrong with database");
            }
        }

        public void Update(int hearingId, HearingDto model)
        {
            try
            {
                var hearingTmp = _dbContext.Hearing.FirstOrDefault(x => x.HearingId == hearingId);
                if(hearingTmp != null)
                {
                    //remove all users for this hearing from UserHearing table
                    var atendees = _dbContext.UserHearing.Where(x => x.HearingId == hearingId).ToList();
                    if (atendees != null)
                        _dbContext.UserHearing.RemoveRange(atendees);

                    //remove all notes for this hearing from Note table
                    var notes = _dbContext.Note.Where(x => x.HearingId == hearingId).ToList();
                    if (notes != null)
                        _dbContext.Note.RemoveRange(notes);

                    //update data
                    hearingTmp.DateModified = DateTime.Now;
                    hearingTmp.HearingDate = model.HearingDate != null ? model.HearingDate : hearingTmp.HearingDate;

                    //update users
                    foreach (var item in model.UserHearing)
                        hearingTmp.UserHearing.Add(new UserHearing() { UserId = item.UserId, HearingId = hearingId });

                    //update notes
                    foreach (var item in model.Note)
                        hearingTmp.Note.Add(new Note() { CreatedByUserId = item.CreatedByUserId, HearingId = hearingId, Text = item.Text });

                    _dbContext.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                throw new Exception("Database error!");
            }
        }

        public ICollection<HearingDto> GetHearings()
        {
            try
            {
                var hearings = _dbContext.Hearing;
                if (hearings != null)
                {
                    ICollection<HearingDto> hearingDto = new List<HearingDto>();
                    foreach (var item in hearings)
                    {
                        hearingDto.Add(Mappers.HearingsRepository.MapToDto(item));
                    }
                    return hearingDto;
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                throw new Exception("Database error!");
            }
            return null;
        }
    }
}
