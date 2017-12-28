using System;
using System.Collections.Generic;
using NSI.DC.SubscriptionRepository;

namespace NSI.BLL.Interfaces
{
    public interface ISubscriptionManipulation
    {
        SubscriptionDto GetSubscription(int subscriptionId);
        IEnumerable<SubscriptionDto> GetAllSubscriptions();
        IEnumerable<SubscriptionDto> GetActiveSubscriptions();
        SubscriptionDto SaveSubscription(SubscriptionDto subscription);
        bool DeleteSubscriptionById(int subscriptionId);
        SubscriptionDto GetCustomerSubscription(int customerId);
        SubscriptionDto UpdateSubscription(SubscriptionDto subscription);
        int GetBonusDays(int subscriptionId, int pricingPackageId);
    }
}
