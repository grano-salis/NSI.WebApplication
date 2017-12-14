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
            try{
                SubscriptionDto userSubscription = GetCustomerSubscription(subscription.CustomerId);
                if (userSubscription != null)
                {
                    _subscriptionRepository.Deactivate(userSubscription.SubscriptionId);
                }
                subscription.IsActive = true;
                subscription.RecurringPayment = false;
                subscription.SubscriptionStartDate = DateTime.Now;
                subscription.SubscriptionExpirationDate = subscription.SubscriptionStartDate.AddMonths(1);
                return _subscriptionRepository.SaveSubscription(subscription);
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public bool DeleteSubscriptionById(int subscriptionId)
        {
            return _subscriptionRepository.DeleteSubscription(subscriptionId);
        }

        public SubscriptionDto GetCustomerSubscription(int customerId)
        {
            return _subscriptionRepository.GetCustomerSubscription(customerId);
        }

        public SubscriptionDto UpdateSubscription(SubscriptionDto subscription)
        {
            return _subscriptionRepository.UpdateSubscription(subscription);
        }

    }
}
