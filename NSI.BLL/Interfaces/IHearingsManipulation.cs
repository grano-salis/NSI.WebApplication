using NSI.DC.HearingsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Interfaces
{
    public interface IHearingsManipulation
    {
        void Create(HearingDto model);
        void Update(int hearingId, HearingDto model);
    }
}
