using NSI.DC.HearingsRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Repository.Interfaces
{
    public interface IHearingsRepository
    {
        HearingDto InsertHearing(HearingDto Model);
        HearingDto UpdateHearing(int hearingId, HearingDto model);
        ICollection<HearingDto> GetHearingsByCase(int caseId);
        ICollection<HearingDto> GetHearings();
        HearingDto GetHearingById(int id);
        void DeleteHearing(int hearingId);
    }
}
