using System;
using IkarusEntities;
using NSI.DC.PricingPackageRepository;
using NSI.Repository.Interfaces;

namespace NSI.Repository
{
    public partial class PricingPackageRepository//:IPricingPackageRepository
    {
        private readonly IkarusContext _dbContext;
        public PricingPackageRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }
        //PricingPackageDto get


    }
}
