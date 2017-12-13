using NSI.Repository.Interfaces;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IkarusEntities;
using NSI.DC.CaseRepository;
using NSI.Logger;
using Microsoft.Extensions.Logging;

namespace NSI.Repository
{
	public partial class CaseInfoRepository : ICaseInfoRepository
	{
		private readonly IkarusContext _dbContext;
        private readonly ILogger<CaseInfoRepository> _logger;

        public CaseInfoRepository(IkarusContext dbContext, ILogger<CaseInfoRepository> logger)
		{
			_dbContext = dbContext;
            _logger = logger;
		}

		public CaseInfo CreateCaseInfo(CaseInfoDto caseInfoDto)
		{
			try
			{
                CaseInfo caseInfo = Mappers.CaseInfoRepository.MapToDbEntity(caseInfoDto);
                _dbContext.CaseInfo.Add(caseInfo);
                if (_dbContext.SaveChanges() != 0)
                    return caseInfo;
			}
			catch (Exception ex)
			{
                throw new Exception(ex.InnerException.Message);
			}
			return null;
		}

        public CaseInfo GetCaseInfoById(int caseId)
		{
			try
			{
				var caseInfo = _dbContext.CaseInfo.FirstOrDefault(x => x.CaseId == caseId);
                return caseInfo;
			}
			catch (Exception ex)
			{
				throw new Exception("Database error!");
			}
		}

        public ICollection<CaseInfoDto> GetCaseInfos()
		{
			try
			{
                var caseInfo = _dbContext.CaseInfo;
                if (caseInfo != null)
                {
                    ICollection<CaseInfoDto> caseInfoDto = new List<CaseInfoDto>();
                    foreach (var item in caseInfo)
                    {
                        caseInfoDto.Add(Mappers.CaseInfoRepository.MapToDto(item));
                    }
                    return caseInfoDto;
                }
				//var caseInfo = _dbContext.CaseInfo;
                //return new List<CaseInfo>(caseInfo);
			}
			catch (Exception ex)
			{
                throw new Exception(ex.InnerException.Message);
			}
            return null;
		}

        public bool DeleteCaseInfoById(int caseId) {
            try
            {
                var caseInfo = _dbContext.CaseInfo.FirstOrDefault(x => x.CaseId == caseId);
                if (caseInfo != null)
                {
                    if (_dbContext.CaseInfo.Remove(caseInfo) != null)
                    {
                        _dbContext.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public bool EditCaseInfoById(int caseId, CaseInfoDto caseInfoDto) {
            
            if(caseInfoDto == null)
            {
                throw new ArgumentNullException(nameof(caseInfoDto), "Address argument is not provided!");
            }

            try
            {
                var CaseInfoTmp = _dbContext.CaseInfo.FirstOrDefault(x => x.CaseId == caseId);
                if (CaseInfoTmp != null)
                {
                    _logger.LogError(CaseInfoTmp.ToString());
                    CaseInfoTmp = Mappers.CaseInfoRepository.MapToDbEntityEdit(CaseInfoTmp, caseInfoDto);
                    _dbContext.CaseInfo.Update(CaseInfoTmp);
                    _dbContext.SaveChanges();
                    return true;
                } else {
                    throw new Exception("DatabaseError");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

	}
}
