using System;
using System.Collections.Generic;
using NSI.BLL.Interfaces;
using NSI.DC.SubscriptionRepository;
using NSI.DC.PricingPackageRepository;
using NSI.Repository.Interfaces;

namespace NSI.BLL
{
    public class SubscriptionManipulation:ISubscriptionManipulation
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IPricingPackageRepository _pricingPackageRepository;

        public SubscriptionManipulation(ISubscriptionRepository subscriptionRepository,IPricingPackageRepository pricingPackageRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _pricingPackageRepository = pricingPackageRepository;
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
                if(userSubscription != null)
                {
                    int bonusDays = GetBonusDays(userSubscription.SubscriptionId,subscription.PricingPackageId);
                    subscription.SubscriptionExpirationDate = subscription.SubscriptionExpirationDate.AddDays(bonusDays);
                }
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

        public int GetBonusDays(int subscriptionId, int pricingPackageId)
        {
            int bonusDays = 0;
            SubscriptionDto userSubscription = _subscriptionRepository.GetSubscription(subscriptionId);
            PricingPackageDto newPricingPackage = _pricingPackageRepository.GetPricingPackage(pricingPackageId);
            

            if(userSubscription!=null)
            {
                PricingPackageDto oldPricingPackage = _pricingPackageRepository.GetPricingPackage(userSubscription.PricingPackageId);
                bonusDays = Convert.ToInt32((oldPricingPackage.Price*(userSubscription.SubscriptionExpirationDate - DateTime.Now).Days)/newPricingPackage.Price);
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine(bonusDays);
            }

            return bonusDays;
        }

    }
}
