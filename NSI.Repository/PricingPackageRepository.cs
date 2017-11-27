using System;
using System.Collections.Generic;
using IkarusEntities;
using NSI.DC.PricingPackageRepository;
using NSI.Repository.Interfaces;
using System.Linq;

namespace NSI.Repository
{
    public partial class PricingPackageRepository:IPricingPackageRepository
    {
        private readonly IkarusContext _dbContext;

        public PricingPackageRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;

        }

        PricingPackageDto IPricingPackageRepository.GetPricingPackage(int pricingPackageId)
        {
            PricingPackage t = _dbContext.PricingPackage.FirstOrDefault(x => x.PricingPackageId == pricingPackageId);
            return t != null ? PricingPackageRepository.MapToDto(t) : null;

        }

        IEnumerable<PricingPackageDto> IPricingPackageRepository.GetAllPricingPackages()
        {
            return _dbContext.PricingPackage.ToList().Select(x => PricingPackageRepository.MapToDto(x));
        }

        PricingPackageDto IPricingPackageRepository.SavePricingPackage(PricingPackageDto pricingPackage)
        {
            var newPricingPackage = MapToDbEntity(pricingPackage);
            _dbContext.PricingPackage.Add(newPricingPackage);
            if (_dbContext.SaveChanges() != 0) return MapToDto(newPricingPackage);
            return null;
        }

        bool IPricingPackageRepository.DeletePricingPackage(int pricingPackageId)
        {
            var pricingPackage = _dbContext.PricingPackage.FirstOrDefault(x => x.PricingPackageId == pricingPackageId);
            pricingPackage.IsDeleted = false;
            pricingPackage.IsActive = false;
            if (_dbContext.SaveChanges() != 0) return true;
            return false;
        }
    }
}
