using NSI.DC.MeetingsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Interfaces
{
    public interface IMeetingsManipulation
    {
        MeetingDto Create(MeetingDto model);
    }
}
