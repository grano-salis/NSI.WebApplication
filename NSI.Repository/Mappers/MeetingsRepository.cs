using IkarusEntities;
using NSI.DC.MeetingsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Mappers
{
    public partial class MeetingsRepository
    {
        public static Meeting MapToDbEntity(MeetingDto model)
        {
            return new Meeting()
            {
                MeetingId = model.MeetingId,
                From = model.From,
                To = model.To,
                CreatedByUser = null,
                CreatedByUserId = 1,
                UserMeeting = null // should call MapToEntity for every user
            };
        }

        public static MeetingDto MapToDto(Meeting entity)
        {
            return new MeetingDto(){
                MeetingId = entity.MeetingId,
                From = entity.From,
                To = entity.To
            };
        }
    }
}
