using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.MeetingsRepository;
using IkarusEntities;
using System.Linq;
using NSI.DC.Exceptions;
using Microsoft.EntityFrameworkCore;
using NSI.DC.Exceptions.Enums;

namespace NSI.Repository
{
    public partial class MeetingsRepository : IMeetingsRepository
    {
        private readonly IkarusContext _dbContext;

        public MeetingsRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<MeetingDto> GetMeetings()
        {
            var meetings = _dbContext.Meeting.Where(x => x.IsDeleted == false)
                .Include(x => x.UserMeeting).ThenInclude(userMeeting => userMeeting.User);
            return meetings.Select(x => Mappers.MeetingsRepository.MapToDto(x)).ToList();
        }

        public MeetingDto GetMeetingById(int id)
        {
            var meeting = _dbContext.Meeting.Where(x => x.MeetingId == id && x.IsDeleted == false)
                .Include(x => x.UserMeeting).ThenInclude(userMeeting => userMeeting.User)
                .FirstOrDefault();
            if (meeting == null) throw new NSIException("Meeting not found");
            return Mappers.MeetingsRepository.MapToDto(meeting);
        }

        public MeetingDto InsertMeeting(MeetingDto model)
        {
            var entity_meeting = Mappers.MeetingsRepository.MapToDbEntity(model);
            _dbContext.Meeting.Add(entity_meeting);
            if (_dbContext.SaveChanges() > 0)
            {
                return GetMeetingById(entity_meeting.MeetingId);
            }
            throw new NSIException("Error while inserting new meeting");
            
        }

        public MeetingDto UpdateMeeting(int meetingId, MeetingDto model)
        {
            var meeting = _dbContext.Meeting.FirstOrDefault(x => x.MeetingId == meetingId && x.IsDeleted == false);
            if (meeting == null) throw new NSIException("Meeting not found");
            //remove all users for this meeting from UserMeeting table
            var atendees = _dbContext.UserMeeting.Where(x => x.MeetingId == meetingId).ToList();
            if (atendees != null)
                _dbContext.UserMeeting.RemoveRange(atendees);

            //update data
            meeting.Title = model.Title ?? meeting.Title;
            meeting.MeetingPlace = model.MeetingPlace ?? meeting.MeetingPlace;
            meeting.DateModified = DateTimeOffset.UtcNow;
            meeting.From = model.From != null ? model.From : meeting.From;
            meeting.To = model.To != null ? model.To : meeting.To;

            //update users
            foreach (var item in model.UserMeeting)
                meeting.UserMeeting.Add(new UserMeeting() { UserId = item.UserId, MeetingId = meetingId });


            if (_dbContext.SaveChanges() > 0)
            {
                return GetMeetingById(meeting.MeetingId);
            }
            throw new NSIException("Error while updating new meeting");

        }

        public void DeleteMeeting(int meetingId)
        {
            var meeting = _dbContext.Meeting.FirstOrDefault(x => x.MeetingId == meetingId);
            if (meeting == null) throw new NSIException("Meeting not found");

            meeting.IsDeleted = true;
            meeting.DateModified = DateTime.Now;
            if(_dbContext.SaveChanges() == 0)
            {
                throw new NSIException("Error while deleting meeting");
            }
        }

        public ICollection<MeetingDto> SearchMeetings(MeetingDto searchCriteria)
        {
            if (searchCriteria == null)
            {
                Logger.Logger.LogError("Meetings searchCriteria is null!");
                throw new NSIException("Parameter searchCriteria is null!", Level.Error, ErrorType.InvalidParameter);
            }
            var meetings = from meeting in _dbContext.Meeting select meeting;

            if (searchCriteria.MeetingId != 0)
                meetings = meetings.Where(x => x.MeetingId == searchCriteria.MeetingId);

            if (!string.IsNullOrEmpty(searchCriteria.Title))
                meetings = meetings.Where(x => x.Title.Contains(searchCriteria.Title));

            if (searchCriteria.To != null)
                meetings = meetings.Where(x => x.To.Value.Date == searchCriteria.To.Value.Date);

            ICollection<MeetingDto> meetingsDto = new List<MeetingDto>();
            foreach (var item in meetings)
            {
                meetingsDto.Add(Mappers.MeetingsRepository.MapToDto(item));
            }
            return meetingsDto;
        }

    }
}
