using System;
using IkarusEntities;
using NSI.DC.PricingPackageRepository;


namespace NSI.Repository
{
    public partial class PricingPackageRepository
    {
        public static PricingPackage MapToDbEntity(PricingPackageDto pricingPackage)
        {
            return new PricingPackage()
            {
                PricingPackageId = pricingPackage.PricingPackageId,
                PricingPackageName = pricingPackage.PricingPackageName,
                Price = pricingPackage.Price,
                IsActive = pricingPackage.IsActive,
                IsDeleted = pricingPackage.IsDeleted,
                DateCreated = pricingPackage.DateCreated,
                DateModified = pricingPackage.DateModified
            };
        }

        public static PricingPackageDto MapToDto(PricingPackage pricingPackage)
        {
            return new PricingPackageDto()
            {
                PricingPackageId = pricingPackage.PricingPackageId,
                PricingPackageName = pricingPackage.PricingPackageName,
                Price = pricingPackage.Price,
                IsActive = pricingPackage.IsActive,
                IsDeleted = pricingPackage.IsDeleted,
                DateCreated = pricingPackage.DateCreated,
                DateModified = pricingPackage.DateModified

            };
        }
    }
}
