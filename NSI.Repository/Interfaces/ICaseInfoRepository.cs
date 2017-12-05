using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.CaseRepository;

namespace NSI.Repository.Interfaces
{
    public interface ICaseInfoRepository
    {
		CaseInfoDto CreateCaseInfo(CaseInfoDto caseInfoDto);
		CaseInfoDto GetCaseInfoDtoById(int caseId);
		ICollection<CaseInfoDto> GetCaseInfos();
	}
}
