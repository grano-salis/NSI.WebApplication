using System;
using System.Collections.Generic;
using IkarusEntities;
using NSI.DC.SubscriptionRepository;
using NSI.Repository.Interfaces;
using System.Linq;

namespace NSI.Repository
{
    public partial class SubscriptionRepository:ISubscriptionRepository
    {
        private readonly IkarusContext _dbContext;

        public SubscriptionRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;

        }

        SubscriptionDto ISubscriptionRepository.GetSubscription(int subscriptionId)
        {
            Subscription subscription = _dbContext.Subscription.FirstOrDefault(x => x.SubscriptionId == subscriptionId);
            return subscription != null ? SubscriptionRepository.MapToDto(subscription) : null;

        }

        IEnumerable<SubscriptionDto> ISubscriptionRepository.GetAllSubscriptions()
        {
            return _dbContext.Subscription.ToList().Select(x => SubscriptionRepository.MapToDto(x));
        }



       SubscriptionDto ISubscriptionRepository.SaveSubscription(SubscriptionDto subscription)
        {
            var newSubscription = MapToDbEntity(subscription);
            _dbContext.Subscription.Add(newSubscription);
            if (_dbContext.SaveChanges() != 0) return MapToDto(newSubscription);
            return null;
        }

        IEnumerable<SubscriptionDto> ISubscriptionRepository.GetActiveSubscriptions()
        {
            return _dbContext.Subscription.Where(x => x.IsActive == true).Select(s => MapToDto(s)).ToList();
        }

        bool ISubscriptionRepository.DeleteSubscription(int subscriptionId)
        {
            var subscription = _dbContext.Subscription.FirstOrDefault(x => x.SubscriptionId == subscriptionId);
            subscription.RecurringPayment = false;
            subscription.IsActive = false;
            if (_dbContext.SaveChanges() != 0) return true;
            return false;
        }

        SubscriptionDto ISubscriptionRepository.GetCustomerSubscription(int customerId)
        {
            var latestSubscription = _dbContext.Subscription.Where(t => t.CustomerId == customerId && t.IsActive == true).OrderByDescending(x => x.SubscriptionStartDate).FirstOrDefault();
            if( latestSubscription != null )
            {
                return MapToDto(latestSubscription);
            }
            return null;
        }

        void ISubscriptionRepository.Deactivate(int subscriptionId){
            var pendingSubscription = _dbContext.Subscription.FirstOrDefault(s => s.SubscriptionId == subscriptionId);

            pendingSubscription.IsActive = false;
            _dbContext.SaveChanges();
        }

        SubscriptionDto ISubscriptionRepository.UpdateSubscription(SubscriptionDto subscription){
            var pendingSubscription = _dbContext.Subscription.FirstOrDefault(s => s.SubscriptionId == subscription.SubscriptionId);
            
            if(pendingSubscription!= null){

                pendingSubscription.IsActive = subscription.IsActive;
                pendingSubscription.RecurringPayment = subscription.RecurringPayment;
                pendingSubscription.SubscriptionExpirationDate= subscription.SubscriptionExpirationDate;

            }
            _dbContext.SaveChanges();
            return MapToDto(pendingSubscription);
        }

    }
}
