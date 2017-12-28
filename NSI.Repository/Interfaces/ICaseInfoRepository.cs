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
		CaseInfo GetCaseInfoById(int caseId);
        ICollection<CaseInfoDto> GetCaseInfos();
        ICollection<CaseInfoDto> GetLatestCaseInfos();
      //  bool DeleteCaseInfoById(int caseId);
        bool EditCaseInfoById(int caseId, CaseInfoDto caseInfoDto);
        void DeleteCase(int caseId);
    }
}
