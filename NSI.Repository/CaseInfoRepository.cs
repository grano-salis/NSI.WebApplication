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

		public CaseInfoDto CreateCaseInfo(CaseInfoDto caseInfoDto)
		{
			try
			{
				var caseInfo = Mappers.CaseInfoRepository.MapToDbEntity(caseInfoDto);
				_dbContext.CaseInfo.Add(caseInfo);
				if (_dbContext.SaveChanges() != 0)
					return Mappers.CaseInfoRepository.MapToDto(caseInfo);
			}
			catch (Exception ex)
			{
				//log ex
				throw new Exception("Database error!");
			}
			return null;
		}

		public CaseInfoDto GetCaseInfoDtoById(int caseId)
		{
			try
			{
				var caseInfo = _dbContext.CaseInfo.FirstOrDefault(x => x.CaseId == caseId);
				if (caseInfo != null)
				{
					return Mappers.CaseInfoRepository.MapToDto(caseInfo);
				}
			}
			catch (Exception ex)
			{
				//log ex
				throw new Exception("Database error!"); throw new Exception();
			}
			return null;
		}

		public ICollection<CaseInfoDto> GetCaseInfos()
		{
			try
			{
				var caseInfo = _dbContext.CaseInfo;
				if (caseInfo != null)
				{
					ICollection<CaseInfoDto> caseInfosDto = new List<CaseInfoDto>();
					foreach (var item in caseInfo)
					{
						caseInfosDto.Add(Mappers.CaseInfoRepository.MapToDto(item));
					}
					return caseInfosDto;
				}
			}
			catch (Exception ex)
			{
				//log ex
				throw new Exception("Database error!");
			}
			return null;
		}
	}
}
