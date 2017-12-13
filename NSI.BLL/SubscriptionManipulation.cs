using System;
using System.Collections.Generic;
using NSI.BLL.Interfaces;
using NSI.DC.SubscriptionRepository;
using NSI.Repository.Interfaces;

namespace NSI.BLL
{
    public class SubscriptionManipulation:ISubscriptionManipulation
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionManipulation(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public SubscriptionDto GetSubscription(int subscriptionId)
        {
            return _subscriptionRepository.GetSubscription(subscriptionId);
        }

        public IEnumerable<SubscriptionDto> GetAllSubscriptions()
        {
            return _subscriptionRepository.GetAllSubscriptions();
        }

        public IEnumerable<SubscriptionDto> GetActiveSubscriptions()
        {
            return _subscriptionRepository.GetActiveSubscriptions();
        }
        public SubscriptionDto SaveSubscription(SubscriptionDto subscription)
        {
            return _subscriptionRepository.SaveSubscription(subscription);
        }

        public bool DeleteSubscriptionById(int subscriptionId)
        {
            return _subscriptionRepository.DeleteSubscription(subscriptionId);
        }

        public SubscriptionDto GetCustomerSubscription(int customerId)
        {
            return _subscriptionRepository.GetCustomerSubscription(customerId);
        }

    }
}
