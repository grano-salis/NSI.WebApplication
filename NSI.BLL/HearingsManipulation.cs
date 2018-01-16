using NSI.BLL.Helpers;
using NSI.BLL.Interfaces;
using NSI.DC.HearingsRepository;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL
{
    public class HearingsManipulation : IHearingsManipulation
    {
        private readonly IHearingsRepository _hearingsRepository;

        public HearingsManipulation(IHearingsRepository hearingsRepository)
        {
            _hearingsRepository = hearingsRepository;
        }

        public HearingDto CreateHearing(HearingDto model)
        {
            return _hearingsRepository.InsertHearing(model);
        }

        public HearingDto UpdateHearing(int hearingId, HearingDto model)
        {
            ValidationHelper.IntegerGreaterThanZero(hearingId, name: "Hearing id");
            return _hearingsRepository.UpdateHearing(hearingId, model);
        }

        public ICollection<HearingDto> GetHearingsByCase(int caseId)
        {
            ValidationHelper.IntegerGreaterThanZero(caseId, name: "Case id");
            return _hearingsRepository.GetHearingsByCase(caseId);
        }

        public ICollection<HearingDto> GetHearings()
        {
            return _hearingsRepository.GetHearings();
        }

        public HearingDto GetHearingById(int id)
        {
            ValidationHelper.IntegerGreaterThanZero(id, name: "Hearing id");
            return _hearingsRepository.GetHearingById(id);
        }

        public void Delete(int hearingId)
        {
            ValidationHelper.IntegerGreaterThanZero(hearingId, name: "Hearing id");
            _hearingsRepository.DeleteHearing(hearingId);
        }
    }
}
