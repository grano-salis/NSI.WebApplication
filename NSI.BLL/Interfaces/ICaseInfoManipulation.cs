using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.CaseRepository;

namespace NSI.BLL.Interfaces
{
    public interface ICaseInfoManipulation
    {
		CaseInfoDto CreateCaseInfo(CaseInfoDto caseInfoDto);
		CaseInfoDto GetCaseInfoById(int caseId);
		ICollection<CaseInfoDto> GetCasesInfo();
	}
}
