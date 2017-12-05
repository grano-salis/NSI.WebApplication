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
        ICollection<HearingDto> GetHearingsByCase(int caseId);
        ICollection<HearingDto> GetHearings();
        HearingDto GetHearingById(int id);
        void Delete(int hearingId);
    }
}
