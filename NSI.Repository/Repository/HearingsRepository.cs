using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.HearingsRepository;
using IkarusEntities;
using System.Linq;
using NSI.DC.Exceptions;

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
                var hearingTmp = _dbContext.Hearing.FirstOrDefault(x => x.HearingId == hearingId && x.IsDeleted == false);
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

        public ICollection<HearingDto> GetHearingsByCase(int caseId)
        {
            try
            {
                var Hearings = _dbContext.Hearing.Where(x => x.CaseId == caseId && x.IsDeleted == false);
                if (Hearings != null)
                {
                    ICollection<HearingDto> HearingDtos = new List<HearingDto>();
                    foreach (var item in Hearings)
                    {
                        HearingDtos.Add(Mappers.HearingsRepository.MapToDto(item));
                    }
                    return HearingDtos;
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                throw new Exception("Database error!");
            }
            return null;
        }

        public ICollection<HearingDto> GetHearings()
        {
            try
            {
                var hearings = _dbContext.Hearing.Where(x => x.IsDeleted == false);
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

        public HearingDto GetHearingById(int id)
        {
            try
            {
                var hearing = _dbContext.Hearing.FirstOrDefault(x => x.HearingId == id && x.IsDeleted == false);
                if (hearing != null)
                {
                    return Mappers.HearingsRepository.MapToDto(hearing);
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                throw new NSIException("Database error!");
            }

            return null;
        }

        public void Delete(int hearingId)
        {
            try
            {
                if (hearingId < 0) throw new Exception("id must be positive");
                var hearingTmp = _dbContext.Hearing.FirstOrDefault(x => x.HearingId == hearingId && x.IsDeleted == false);
                if (hearingTmp != null)
                {
                    hearingTmp.IsDeleted = true;
                    hearingTmp.DateModified = DateTime.Now;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                throw new Exception("Database error!");
            }
        }
    }
}
