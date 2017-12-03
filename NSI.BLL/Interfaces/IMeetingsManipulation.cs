using NSI.DC.MeetingsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Interfaces
{
    public interface IMeetingsManipulation
    {
        void Create(MeetingDto model);
        ICollection<MeetingDto> GetMeetings();
        void Update(int meetingId, MeetingDto model);
        void Delete(int meetingId);
    }
}
