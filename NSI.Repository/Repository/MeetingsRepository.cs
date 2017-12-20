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

        public ICollection<MeetingDto> GetMeetingsByUser(int userId)
        {
            var ids = _dbContext.UserMeeting.Where(x => x.UserId == userId)
                .Select(um => um.MeetingId).ToList();

            if(ids == null)
            {
                throw new NSIException("User id not found");
            }

            var meetings = _dbContext.Meeting.Where(x => x.IsDeleted == false && ids.Contains(x.MeetingId))
                .Include(x => x.UserMeeting).ThenInclude(userMeeting => userMeeting.User);

            if(meetings == null)
            {
                throw new NSIException("Meetings for given user id not found");
            }

            return meetings.Select(x => Mappers.MeetingsRepository.MapToDto(x)).ToList();
        }

        //returns intervals of available time for appointing a meeting for given users and time range 
        public ICollection<MeetingTimeDto> GetMeetingTimes(ICollection<int> userIds, DateTime from, DateTime to, int meetingDuration)
        {
            //gathering the meetings of all the given users
            List<List<MeetingDto>> userMeetings = new List<List<MeetingDto>>();
            for(int i = 0; i < userIds.Count; i++)
            {
                var meetings = GetMeetingsByUser(userIds.ElementAt(i)).ToList();
                userMeetings.Add(meetings);
            }

            List<MeetingTimeDto> listOfAvailableTimes = new List<MeetingTimeDto>();

            if(userMeetings.Count == 0)
            {
                listOfAvailableTimes.Add(new MeetingTimeDto { From = from, To = to });

                return listOfAvailableTimes;
            }


            //extracting all the unavailable intervals based on existing meetings and given time range
            //in order to find all the available intervals
            List<MeetingTimeDto> listOfUnavailableTimes = new List<MeetingTimeDto>();

            for(int i = 0; i < userMeetings.Count; i++)
            {
                for(int j = 0; j < userMeetings[i].Count; j++)
                {
                    if (userMeetings[i][j].From > from && to > userMeetings[i][j].To)
                    {
                        listOfUnavailableTimes.Add(new MeetingTimeDto
                        { From = userMeetings[i][j].From, To = userMeetings[i][j].To });
                    }
                    else if (userMeetings[i][j].From <= from && to > userMeetings[i][j].To && userMeetings[i][j].To > from)
                    {
                        listOfUnavailableTimes.Add(new MeetingTimeDto
                        { From = from, To = userMeetings[i][j].To });
                    }
                    else if (userMeetings[i][j].From > from && to <= userMeetings[i][j].To && userMeetings[i][j].From < to)
                    {
                        listOfUnavailableTimes.Add(new MeetingTimeDto
                        { From = userMeetings[i][j].From, To = to });
                    }
                    else if(userMeetings[i][j].From <= from && to <= userMeetings[i][j].To)
                    {
                        //if there are no free intervals, returning an empty list
                        return listOfAvailableTimes;
                    }
                }
            }

            if (listOfUnavailableTimes.Count == 0)
            {
                listOfAvailableTimes.Add(new MeetingTimeDto { From = from, To = to });

                return listOfAvailableTimes;
            }

            var sortedUnavailable = listOfUnavailableTimes.OrderBy(t => t.From).ToList();

            //merging the sorted unavailable intervals in order to avoid the possible overlappings
            //into a non-overlapping list of unavailable intervals
            var mergedUnavailable = new List<MeetingTimeDto>();

            mergedUnavailable.Add(sortedUnavailable[0]);

            if(sortedUnavailable.Count > 1)
            {
                for (int i = 1; i < sortedUnavailable.Count; i++)
                {
                    if(sortedUnavailable[i].From <= mergedUnavailable[mergedUnavailable.Count - 1].To &&
                       sortedUnavailable[i].To > mergedUnavailable[mergedUnavailable.Count - 1].To)
                    {
                        mergedUnavailable[mergedUnavailable.Count - 1].To = sortedUnavailable[i].To;
                    }
                    else if(sortedUnavailable[i].From > mergedUnavailable[mergedUnavailable.Count - 1].To)
                    {
                        mergedUnavailable.Add(sortedUnavailable[i]);
                    }
                }
            }
            

            //extracting free intervals from the given time range by finding the gaps between
            //unavailable intervals
            DateTime FreeIntervalPivot = from;

            for(int i = 0; i < mergedUnavailable.Count; i++)
            {
                if(FreeIntervalPivot < mergedUnavailable[i].From)
                {
                    listOfAvailableTimes.Add(new MeetingTimeDto { From = FreeIntervalPivot, To = mergedUnavailable[i].From });
                }

                if (mergedUnavailable[i].To != null)
                {
                    FreeIntervalPivot = (DateTime)mergedUnavailable[i].To;
                }
            }

            if(to > FreeIntervalPivot)
            {
                listOfAvailableTimes.Add(new MeetingTimeDto { From = FreeIntervalPivot, To = to});
            }

            return listOfAvailableTimes;
        }
    }
}
