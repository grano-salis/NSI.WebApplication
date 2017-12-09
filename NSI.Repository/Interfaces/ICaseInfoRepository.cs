using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.CaseRepository;
using IkarusEntities;

namespace NSI.Repository.Interfaces
{
    public interface ICaseInfoRepository
    {
        CaseInfo CreateCaseInfo(CaseInfoDto caseInfoDto);
		CaseInfo GetCaseInfoDtoById(int caseId);
        IEnumerable<CaseInfo> GetCaseInfos();
	}
}
