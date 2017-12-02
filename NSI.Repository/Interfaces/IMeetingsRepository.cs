using NSI.DC.MeetingsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface IMeetingsRepository
    {
        void Insert(MeetingDto model);
        ICollection<MeetingDto> GetMeetings();
    }
}
