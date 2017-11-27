using System;
using System.Collections.Generic;
using NSI.DC.PricingPackageRepository;

namespace NSI.BLL.Interfaces
{
    public interface IPricingPackageManipulation
    {
        PricingPackageDto GetPricingPackage(int pricingPackageId);
        IEnumerable<PricingPackageDto> GetPricingPackages();
        PricingPackageDto SavePricingPackage(PricingPackageDto pricingPackage);
        bool DeletePricingPackageById(int pricingPackageId);
    }
}
