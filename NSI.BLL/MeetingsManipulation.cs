﻿using NSI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.MeetingsRepository;
using NSI.Repository.Interfaces;
using NSI.DC.Exceptions;
using NSI.BLL.Helpers;

namespace NSI.BLL
{
    public class MeetingsManipulation : IMeetingsManipulation
    {
        private readonly IMeetingsRepository _meetingsRepository;

        public MeetingsManipulation(IMeetingsRepository meetingsRepository)
        {
            _meetingsRepository = meetingsRepository;
        }

        public MeetingDto CreateMeeting(MeetingDto model)
        {
            return _meetingsRepository.InsertMeeting(model);
        }

        public ICollection<MeetingDto> GetMeetings()
        {
            return _meetingsRepository.GetMeetings();
        }

        public MeetingDto EditMeeting(int meetingId, MeetingDto model)
        {
            ValidationHelper.IntegerGreaterThanZero(meetingId, name: "Meeting id");
            return _meetingsRepository.UpdateMeeting(meetingId, model);
        }
        public void RemoveMeeting(int meetingId)
        {
            ValidationHelper.IntegerGreaterThanZero(meetingId, name: "Meeting id");
            _meetingsRepository.DeleteMeeting(meetingId);
        }

        public MeetingDto GetMeetingById(int id)
        {
            ValidationHelper.IntegerGreaterThanZero(id, name: "Meeting id");
            return _meetingsRepository.GetMeetingById(id);
        }

        public ICollection<MeetingDto> GetMeetings(int? page = null, int? pageSize = null)
        {
            var meetings = _meetingsRepository.GetMeetings();
            if (page != null && pageSize != null)
            {
                return PagingHelper<MeetingDto>.PagedList(meetings, (int)page, (int)pageSize);
            }
            return meetings;
        }

        public ICollection<MeetingDto> SearchMeetings(MeetingDto searchCriteria, int pageNumber, int pageSize)
        {
            return PagingHelper<MeetingDto>.PagedList(_meetingsRepository.SearchMeetings(searchCriteria), pageNumber, pageSize);
        }

        public ICollection<MeetingDto> GetMeetingsByUser(int userId)
        {
            ValidationHelper.IntegerGreaterThanZero(userId, name: "User id");
            return _meetingsRepository.GetMeetingsByUser(userId);
        }

        public ICollection<MeetingTimeDto> GetMeetingTimes(ICollection<int> userIds, DateTime from, DateTime to, int meetingDuration, int currentMeetingId)
        {
            foreach (int userId in userIds)
                ValidationHelper.IntegerGreaterThanZero(userId, name: "User id");
            ValidationHelper.IntegerGreaterThanZero(currentMeetingId, name: "Meeting id");
            return _meetingsRepository.GetMeetingTimes(userIds, from, to, meetingDuration, currentMeetingId);
        }

        public ICollection<MeetingDto> CheckUsersAvailability(ICollection<int> userIds, DateTime from, DateTime to, int currentMeetingId)
        {
            foreach (int userId in userIds)
                ValidationHelper.IntegerGreaterThanZero(userId, name: "User id");
            ValidationHelper.IntegerGreaterThanZero(currentMeetingId, name: "Meeting id");

            return _meetingsRepository.CheckUsersAvailability(userIds, from, to, currentMeetingId);
        }
    }
}
