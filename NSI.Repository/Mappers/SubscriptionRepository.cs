using System;
using IkarusEntities;
using NSI.DC.SubscriptionRepository;

namespace NSI.Repository
{
    public partial class SubscriptionRepository
    {
        public static Subscription MapToDbEntity(SubscriptionDto subscription)
        {
            return new Subscription()
            {
                SubscriptionId = subscription.SubscriptionId,
                PricingPackageId = subscription.PricingPackageId,
                CustomerId = subscription.CustomerId,
                SubscriptionStartDate = subscription.SubscriptionStartDate,
                SubscriptionExpirationDate = subscription.SubscriptionExpirationDate,
                IsActive = subscription.IsActive,
                RecurringPayment = subscription.RecurringPayment,
                Customer = subscription.Customer,
                PricingPackage = subscription.pricingPackage
            };
        }

        public static SubscriptionDto MapToDto(Subscription subscription)
        {
            return new SubscriptionDto()
            {
                SubscriptionId = subscription.SubscriptionId,
                PricingPackageId = subscription.PricingPackageId,
                CustomerId = subscription.CustomerId,
                SubscriptionStartDate = subscription.SubscriptionStartDate,
                SubscriptionExpirationDate = subscription.SubscriptionExpirationDate,
                IsActive = subscription.IsActive,
                RecurringPayment = subscription.RecurringPayment,
                Customer = subscription.Customer,
                PricingPackage = subscription.pricingPackage

            };
        }
    }
}