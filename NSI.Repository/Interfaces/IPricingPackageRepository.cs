using System;
using NSI.DC.PricingPackageRepository;

namespace NSI.Repository.Interfaces
{
    public interface IPricingPackageRepository
    {
        PricingPackageDto GetPricingPackage(int pricingPackageId);
        //IEnumerable<TransactionDto> SearchTransactions(DocumentSearchCriteriaDto searchCriteria);
        long SaveTransaction(PricingPackageDto pricingPackage);
    }
}
