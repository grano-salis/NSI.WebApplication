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
                CreatedByUserId = 1,
                DateCreated = DateTime.UtcNow
            };
        }

        public static UserMeeting MapToDbEntity(int user_id, int meeting_id)
        {
            return new UserMeeting()
            {
                UserId = user_id,
                MeetingId = meeting_id
            };
        }

        public static MeetingDto MapToDto(Meeting entity, IEnumerable<UserInfo> users)
        {
            List<UserMeetingDto> userMeetings = new List<UserMeetingDto>();
            foreach (var item in users)
            {
                userMeetings.Add(new UserMeetingDto()
                {
                    UserId = item.UserId,
                    UserName = item.Username
                });
            }
            return new MeetingDto()
            {
                MeetingId = entity.MeetingId,
                From = entity.From,
                To = entity.To,
                UserMeeting = userMeetings
            };
        }
    }
}
