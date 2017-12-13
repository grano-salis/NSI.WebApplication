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
            return new CaseInfo()
            {
                CaseNumber = caseInfoDto.CaseNumber,
                CourtNumber = caseInfoDto.CourtNumber,
                Value = caseInfoDto.Value,
                Judge = caseInfoDto.Judge,
                Court = caseInfoDto.Court,
                CounterParty = caseInfoDto.CounterParty,
                Note = caseInfoDto.Note,
                CaseCategory = caseInfoDto.CaseCategory,
                CustomerId = caseInfoDto.CustomerId,
                ClientId = caseInfoDto.ClientId,
                CreatedByUserId = caseInfoDto.CreatedByUserId
            };
		}

        public static CaseInfo MapToDbEntityEdit(CaseInfo caseInfoOriginal, CaseInfoDto caseInfoEdit) {
            caseInfoOriginal.CaseNumber = caseInfoEdit.CaseNumber ?? caseInfoOriginal.CaseNumber;
            caseInfoOriginal.CourtNumber = caseInfoEdit.CourtNumber ?? caseInfoOriginal.CourtNumber;
            caseInfoOriginal.Value = caseInfoEdit.Value ?? caseInfoOriginal.Value;
            caseInfoOriginal.Judge = caseInfoEdit.Judge ?? caseInfoOriginal.Judge;
            caseInfoOriginal.Court = caseInfoEdit.Court ?? caseInfoOriginal.Court;
            caseInfoOriginal.CounterParty = caseInfoEdit.CounterParty ?? caseInfoOriginal.CounterParty;
            caseInfoOriginal.Note = caseInfoEdit.Note ?? caseInfoOriginal.Note;
            caseInfoOriginal.DateModified = DateTime.Now;
            caseInfoOriginal.CaseCategory = caseInfoEdit.CaseCategory;
            caseInfoOriginal.CustomerId = caseInfoEdit.CustomerId;
            caseInfoOriginal.ClientId = caseInfoEdit.ClientId;
            caseInfoOriginal.CreatedByUserId = caseInfoEdit.CreatedByUserId;
            return caseInfoOriginal;
        }

		public static CaseInfoDto MapToDto(CaseInfo caseInfo)
		{
			return new CaseInfoDto()
			{
                DateCreated = caseInfo.DateCreated,
                DateModified = caseInfo.DateModified,
                CaseNumber = caseInfo.CaseNumber,
                CourtNumber = caseInfo.CourtNumber,
                Value = caseInfo.Value,
                Judge = caseInfo.Judge,
                Court = caseInfo.Court,
                CounterParty = caseInfo.CounterParty,
                Note = caseInfo.Note,
                CaseCategory = caseInfo.CaseCategory,
                CustomerId = caseInfo.CustomerId,
                ClientId = caseInfo.ClientId,
                CreatedByUserId = caseInfo.CreatedByUserId,
                CaseId = caseInfo.CaseId
			};
		}

	}
}
