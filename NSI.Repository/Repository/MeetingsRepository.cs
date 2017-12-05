using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.MeetingsRepository;
using IkarusEntities;
using System.Linq;
using NSI.DC.Exceptions;

namespace NSI.Repository
{
    public partial class MeetingsRepository : IMeetingsRepository
    {
        private readonly IkarusContext _dbContext;

        public MeetingsRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Insert(MeetingDto model)
        {
            try
            {
                var entity_meeting = Mappers.MeetingsRepository.MapToDbEntity(model);
                _dbContext.Meeting.Add(entity_meeting);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                throw new Exception("Something went wrong with database");
            }
        }
        public ICollection<MeetingDto> GetMeetings()
        {
            try
            {
                var meetings = _dbContext.Meeting.Where(x => x.IsDeleted == false);
                if (meetings != null)
                {
                    ICollection<MeetingDto> meetingDto = new List<MeetingDto>();
                    foreach (var item in meetings)
                    {
                        meetingDto.Add(Mappers.MeetingsRepository.MapToDto(item));
                    }
                    return meetingDto;
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                throw new Exception("Database error!");
            }
            return null;
        }
        public void Update(int meetingId, MeetingDto model)
        {
            try
            {
                var meetingTmp = _dbContext.Meeting.FirstOrDefault(x => x.MeetingId == meetingId && x.IsDeleted == false);
                if (meetingTmp != null)
                {
                    //remove all users for this meeting from UserMeeting table
                    var atendees = _dbContext.UserMeeting.Where(x => x.MeetingId == meetingId).ToList();
                    if (atendees != null)
                        _dbContext.UserMeeting.RemoveRange(atendees);

                    //update data
                    meetingTmp.Title = model.Title != null ? model.Title : meetingTmp.Title;
                    meetingTmp.DateModified = DateTime.Now;
                    meetingTmp.From = model.From != null ? model.From : meetingTmp.From;
                    meetingTmp.To = model.To != null ? model.To : meetingTmp.To;

                    //update users
                    foreach (var item in model.UserMeeting)
                        meetingTmp.UserMeeting.Add(new UserMeeting() { UserId = item.UserId, MeetingId = meetingId });

                    _dbContext.SaveChanges();

                }
            }
            catch (Exception e)
            {
                Logger.Logger.LogError(e.Message);
                throw new Exception("Database error!");

            }
        }

        public void Delete(int meetingId)
        {
            try
            {
                if (meetingId < 0) throw new Exception("id must be positive");
                var meetingTmp = _dbContext.Meeting.FirstOrDefault(x => x.MeetingId == meetingId);
                if (meetingTmp != null)
                {
                    meetingTmp.IsDeleted = true;
                    meetingTmp.DateModified = DateTime.Now;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                throw new Exception("Database error!");
            }
        }

        public MeetingDto GetMeetingById(int id)
        {
            try
            {
                var meeting = _dbContext.Meeting.FirstOrDefault(x => x.MeetingId == id && x.IsDeleted == false);
                if (meeting != null)
                {
                    return Mappers.MeetingsRepository.MapToDto(meeting);
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                throw new NSIException("Database error!");
            }
            return null;
        }
    }
    }
