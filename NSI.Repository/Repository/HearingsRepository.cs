using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.HearingsRepository;
using IkarusEntities;
using System.Linq;
using NSI.DC.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace NSI.Repository.Repository
{
    public partial class HearingsRepository : IHearingsRepository
    {
        private readonly IkarusContext _dbContext;

        public HearingsRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public HearingDto InsertHearing(HearingDto model)
        {
            var entity = Mappers.HearingsRepository.MapToDbEntity(model);
            _dbContext.Hearing.Add(entity);
            if (_dbContext.SaveChanges() > 0)
            {
                return GetHearingById(entity.HearingId);
            }
            throw new NSIException("Erro while inserting new hearing");
        }

        public HearingDto UpdateHearing(int hearingId, HearingDto model)
        {
            var entity = _dbContext.Hearing.FirstOrDefault(x => x.HearingId == hearingId && x.IsDeleted == false);
            if (entity == null) throw new NSIException("Hearing not found");

            //remove all users for this hearing from UserHearing table
            var atendees = _dbContext.UserHearing.Where(x => x.HearingId == hearingId).ToList();
            if (atendees != null)
                _dbContext.UserHearing.RemoveRange(atendees);

            //remove all notes for this hearing from Note table
            var note = _dbContext.Note.FirstOrDefault(x => x.HearingId == hearingId && x.CreatedByUserId == model.Note.ElementAt(0).CreatedByUserId);
            if (note != null)
                _dbContext.Note.Remove(note);

            //update data
            entity.DateModified = DateTime.Now;
            entity.HearingDate = model.HearingDate != null ? model.HearingDate : entity.HearingDate;

            //update users
            foreach (var item in model.UserHearing)
                entity.UserHearing.Add(new UserHearing() { UserId = item.UserId, HearingId = hearingId });

            //update notes
            foreach (var item in model.Note)
                entity.Note.Add(new Note() { CreatedByUserId = item.CreatedByUserId, HearingId = hearingId, Text = item.Text });

            if (_dbContext.SaveChanges() > 0)
            {
                return GetHearingById(entity.HearingId);
            }

            throw new NSIException("Erro while updating hearing");

        }

        public ICollection<HearingDto> GetHearingsByCase(int caseId)
        {
            var hearings = _dbContext.Hearing.Where(x => x.CaseId == caseId && x.IsDeleted == false)
                .Include(hearing => hearing.Note).Include(hearing => hearing.UserHearing)
                .ThenInclude(userHearing => userHearing.User);
            return hearings.Select(x => Mappers.HearingsRepository.MapToDto(x)).ToList();

        }

        public ICollection<HearingDto> GetHearings()
        {
            var hearings = _dbContext.Hearing.Where(x => x.IsDeleted == false).Include(hearing => hearing.Note)
                .Include(hearing => hearing.UserHearing).ThenInclude(userHearing => userHearing.User);
            return hearings.Select(x => Mappers.HearingsRepository.MapToDto(x)).ToList();
        }

        public HearingDto GetHearingById(int id)
        {
            var hearing = _dbContext.Hearing.Where(x => x.HearingId == id && x.IsDeleted == false)
                .Include(x => x.UserHearing)
                .ThenInclude(userHearing => userHearing.User).FirstOrDefault();

            if (hearing == null) throw new NSIException("Hearing not found");

            var correctNote = _dbContext.Entry(hearing).Collection(h => h.Note)
                .Query().Where(n => n.CreatedByUserId == hearing.CreatedByUserId).ToList();

            hearing.Note = correctNote;

            
            return Mappers.HearingsRepository.MapToDto(hearing);
        }

        public void DeleteHearing(int hearingId)
        {

            var hearing = _dbContext.Hearing.FirstOrDefault(x => x.HearingId == hearingId && x.IsDeleted == false);
            if (hearing == null) throw new NSIException("Hearing not found");

            hearing.IsDeleted = true;
            hearing.DateModified = DateTime.Now;
            if (_dbContext.SaveChanges() == 0)
            {
                throw new NSIException("Erro while deleting hearing");
            }

        }
    }
}
