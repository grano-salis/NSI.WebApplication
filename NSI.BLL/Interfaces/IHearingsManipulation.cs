using NSI.DC.HearingsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Interfaces
{
    public interface IHearingsManipulation
    {
        HearingDto CreateHearing(HearingDto model);
        HearingDto UpdateHearing(int hearingId, HearingDto model);
        ICollection<HearingDto> GetHearingsByCase(int caseId);
        ICollection<HearingDto> GetHearings();
        HearingDto GetHearingById(int id);
        void Delete(int hearingId);
    }
}
