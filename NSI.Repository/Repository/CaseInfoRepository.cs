using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IkarusEntities;
using NSI.DC.CaseRepository;
using NSI.Repository.Interfaces;

namespace NSI.Repository
{
	public partial class CaseInfoRepository : ICaseInfoRepository
	{
		private readonly IkarusContext _dbContext;

		public CaseInfoRepository(IkarusContext dbContext)
		{
			_dbContext = dbContext;
		}

		public CaseInfo CreateCaseInfo(CaseInfoDto caseInfoDto)
		{
			try
			{
                //var caseInfo = Mappers.CaseInfoRepository.MapToDbEntity(caseInfoDto);
                // CaseInfo caseinfo = new CaseInfo();
                CaseInfo caseInfo = new CaseInfo()
                {
                    CaseNumber = caseInfoDto.CaseNumber,
                    CourtNumber = caseInfoDto.CourtNumber,
                    Value = caseInfoDto.Value,
                    Judge = caseInfoDto.Judge,
                    Court = caseInfoDto.Court,
                    CounterParty=caseInfoDto.CounterParty,
                    Note = caseInfoDto.Note,
                    CaseCategory=caseInfoDto.CaseCategory,
                    CustomerId=caseInfoDto.CustomerId,
                    ClientId=caseInfoDto.ClientId,
                    CreatedByUserId=caseInfoDto.CreatedByUserId
                };


                _dbContext.CaseInfo.Add(caseInfo);
                if (_dbContext.SaveChanges() != 0)
                    return caseInfo;//Mappers.CaseInfoRepository.MapToDto(caseInfo);
			}
			catch (Exception ex)
			{
				//log ex
				throw new Exception("Database error!");
			}
			return null;
		}

        public CaseInfo GetCaseInfoDtoById(int caseId)
		{
			try
			{
				var caseInfo = _dbContext.CaseInfo.FirstOrDefault(x => x.CaseId == caseId);
                return caseInfo;
				//if (caseInfo != null)
				//{
				//	return Mappers.CaseInfoRepository.MapToDto(caseInfo);
				//}
			}
			catch (Exception ex)
			{
				//log ex
				throw new Exception("Database error!"); throw new Exception();
			}
			// return null;
		}

        public IEnumerable<CaseInfo> GetCaseInfos()
		{
			try
			{
				var caseInfo = _dbContext.CaseInfo;
                return caseInfo;
				//if (caseInfo != null)
				//{
				//	ICollection<CaseInfo> caseInfos = new List<CaseInfo>();
				//	foreach (var item in caseInfo)
				//	{
				//		caseInfosDto.Add(Mappers.CaseInfoRepository.MapToDto(item));
				//	}
				//	return caseInfosDto;
				//}
			}
			catch (Exception ex)
			{
				//log ex
				throw new Exception("Database error!");
			}
			// return null;
		}
	}
}
