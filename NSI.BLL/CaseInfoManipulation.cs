using System;
using System.Collections.Generic;
using System.Text;
using NSI.BLL.Interfaces;
using NSI.DC.CaseRepository;
using NSI.Repository.Interfaces;
using IkarusEntities;

namespace NSI.BLL
{
    public class CaseInfoManipulation: ICaseInfoManipulation
    {
		ICaseInfoRepository _caseInfoRepository;

		public CaseInfoManipulation(ICaseInfoRepository caseInfoRepository)
		{
			_caseInfoRepository = caseInfoRepository;
		}

        public CaseInfo CreateCaseInfo(CaseInfoDto caseInfoDto)
		{
			return _caseInfoRepository.CreateCaseInfo(caseInfoDto);
		}

		public CaseInfo GetCaseInfoById(int caseId)
		{
			return _caseInfoRepository.GetCaseInfoById(caseId);
		}

        public ICollection<CaseInfoDto> GetCaseInfos()
		{
			return _caseInfoRepository.GetCaseInfos();
		}

        public ICollection<CaseInfoDto> GetLatestCaseInfos()
        {
            return _caseInfoRepository.GetLatestCaseInfos();
        }

        //public bool  DeleteCaseInfoById(int caseId) {
        //    return _caseInfoRepository.DeleteCaseInfoById(caseId);
        //}
        public bool EditCaseInfoById(int caseId, CaseInfoDto caseInfoDto) {
            return _caseInfoRepository.EditCaseInfoById(caseId, caseInfoDto);
        }
        public void Delete(int caseId)
        {
            
            _caseInfoRepository.Delete(caseId);
        }

    }
}
