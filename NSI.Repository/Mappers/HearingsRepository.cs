using IkarusEntities;
using NSI.DC.HearingsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSI.Repository.Mappers
{
    public partial class HearingsRepository
    {
        public static Hearing MapToDbEntity(HearingDto model)
        {
            return new Hearing()
            {
                HearingId = model.HearingId,
                HearingDate = model.HearingDate,
                CreatedByUserId = model.CreatedByUserId,
                CaseId = model.CaseId,
                UserHearing = model.UserHearing.Select(x => new UserHearing() { UserId = x.UserId }).ToList(),
                Note = model.Note.Select(x => new Note()
                {
                    Text = x.Text,
                    CreatedByUserId = x.CreatedByUserId,
                    HearingId = model.HearingId
                }).ToList()
            };
        }

        public static HearingDto MapToDto(Hearing entity)
        {
            return new HearingDto()
            {
                HearingId = entity.HearingId,
                HearingDate = entity.HearingDate,
                CreatedByUserId = entity.CreatedByUserId,
                CaseId = entity.CaseId,
                UserHearing = entity.UserHearing.Select(x => new UserHearingDto()
                {
                    UserId = x.UserId,
                    UserName = x.User.Username
                }),
                Note = entity.Note.Select(x => new NoteDto()
                {
                    Text = x.Text,
                    CreatedByUserId = x.CreatedByUserId,
                    HearingId = x.HearingId
                })
            };
        }
    }
}
