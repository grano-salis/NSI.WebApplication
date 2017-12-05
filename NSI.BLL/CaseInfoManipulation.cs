using System;
using System.Collections.Generic;
using System.Text;
using NSI.BLL.Interfaces;
using NSI.DC.CaseRepository;
using NSI.Repository.Interfaces;

namespace NSI.BLL
{
    public class CaseInfoManipulation: ICaseInfoManipulation
    {
		ICaseInfoRepository _caseInfoRepository;

		public CaseInfoManipulation(ICaseInfoRepository caseInfoRepository)
		{
			_caseInfoRepository = caseInfoRepository;
		}

		public CaseInfoDto CreateCaseInfo(CaseInfoDto caseInfoDto)
		{
			return _caseInfoRepository.CreateCaseInfo(caseInfoDto);
		}

		public CaseInfoDto GetCaseInfoById(int caseId)
		{
			return _caseInfoRepository.GetCaseInfoDtoById(caseId);
		}

		public ICollection<CaseInfoDto> GetCasesInfo()
		{
			return _caseInfoRepository.GetCaseInfos();
		}
	}
}
