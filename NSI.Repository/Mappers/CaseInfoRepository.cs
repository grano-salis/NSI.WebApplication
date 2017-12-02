using System;
using System.Collections.Generic;
using System.Text;
using IkarusEntities;
using NSI.DC.CaseRepository;

namespace NSI.Repository.Mappers
{
	public class CaseInfoRepository
	{
		public static CaseInfo MapToDbEntity(CaseInfoDto caseInfoDto)
		{
			return new CaseInfo
			{
				CaseCategory=caseInfoDto.CaseCategory,
				CaseId=caseInfoDto.CaseId,
				CaseNumber=caseInfoDto.CaseNumber
			};
		}

		public static CaseInfoDto MapToDto(CaseInfo caseInfo)
		{
			return new CaseInfoDto()
			{
				CaseCategory=caseInfo.CaseCategory,
				CaseId=caseInfo.CaseId,
				CaseNumber=caseInfo.CaseNumber
			};
		}
	}
}
