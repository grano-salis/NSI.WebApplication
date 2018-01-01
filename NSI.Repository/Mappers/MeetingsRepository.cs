using IkarusEntities;
using NSI.DC.MeetingsRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NSI.Repository.Mappers
{
    public partial class MeetingsRepository
    {
        public static Meeting MapToDbEntity(MeetingDto model)
        {
            return new Meeting()
            {
                MeetingId = model.MeetingId,
                Title = model.Title,
                MeetingPlace = model.MeetingPlace,
                From = model.From,
                To = model.To,
                CreatedByUserId = 1,
                DateCreated = DateTimeOffset.UtcNow,
                UserMeeting = model.UserMeeting.Select(x => new UserMeeting() { UserId = x.UserId }).ToList()
            };
        }

        public static MeetingDto MapToDto(Meeting entity)
        {
            return new MeetingDto()
            {
                MeetingId = entity.MeetingId,
                Title = entity.Title,
                MeetingPlace = entity.MeetingPlace,
                From = entity.From.AddHours(-1),
                To = entity.To.GetValueOrDefault().AddHours(-1),
                UserMeeting = entity.UserMeeting.Select(x => new UserMeetingDto()
                {
                    UserId = x.UserId,
                    UserName = x.User.Username
                }).ToList()
            };
        }
    }
}
