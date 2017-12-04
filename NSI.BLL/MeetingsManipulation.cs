using NSI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.MeetingsRepository;
using NSI.Repository.Interfaces;

namespace NSI.BLL
{
    public class MeetingsManipulation : IMeetingsManipulation
    {
        private readonly IMeetingsRepository _meetingsRepository;

        public MeetingsManipulation(IMeetingsRepository meetingsRepository)
        {
            _meetingsRepository = meetingsRepository;
        }

        public void Create(MeetingDto model)
        {
            _meetingsRepository.Insert(model);
        }

        public ICollection<MeetingDto> GetMeetings()
        {
            return _meetingsRepository.GetMeetings();
        }

        public void Update(int meetingId, MeetingDto model)
        {
            _meetingsRepository.Update(meetingId, model);
        }
        public void Delete(int meetingId)
        {
            _meetingsRepository.Delete(meetingId);
        }

        public MeetingDto GetMeetingById(int id)
        {
            return _meetingsRepository.GetMeetingById(id);
        }
    }
}
