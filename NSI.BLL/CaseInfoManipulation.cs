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

		public CaseInfo GetCaseInfoDtoById(int caseId)
		{
			return _caseInfoRepository.GetCaseInfoDtoById(caseId);
		}

        public IEnumerable<CaseInfo> GetCaseInfos()
		{
			return _caseInfoRepository.GetCaseInfos();
		}
	}
}
