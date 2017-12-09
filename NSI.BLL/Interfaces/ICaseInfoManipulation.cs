using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.CaseRepository;
using IkarusEntities;

namespace NSI.BLL.Interfaces
{
    public interface ICaseInfoManipulation
    {
        CaseInfo CreateCaseInfo(CaseInfoDto caseInfoDto);
        CaseInfo GetCaseInfoDtoById(int caseId);
        IEnumerable<CaseInfo> GetCaseInfos();
	}
}
