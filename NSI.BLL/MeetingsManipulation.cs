using NSI.BLL.Interfaces;
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

        public void CreateMeeting(MeetingDto model)
        {
            _meetingsRepository.InsertMeeting(model);
        }

        public ICollection<MeetingDto> GetMeetings()
        {
            return _meetingsRepository.GetMeetings();
        }

        public void EditMeeting(int meetingId, MeetingDto model)
        {
            ValidationHelper.IntegerGreaterThanZero(meetingId, name: "Meeting id");
            _meetingsRepository.UpdateMeeting(meetingId, model);
        }
        public void RemoveMeeting(int meetingId)
        {
            ValidationHelper.IntegerGreaterThanZero(meetingId, name: "Meeting id");
            _meetingsRepository.DeleteMeeting(meetingId);
        }

        public MeetingDto GetMeetingById(int meetingId)
        {
            ValidationHelper.IntegerGreaterThanZero(meetingId, name: "Meeting id");
            return _meetingsRepository.GetMeetingById(meetingId);
        }
    }
}
