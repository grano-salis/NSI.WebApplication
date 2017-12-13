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
            var latestTransaction = _dbContext.Transaction.Where(t => t.CustomerId == customerId).OrderByDescending(x => x.DateCreated).FirstOrDefault();
            if( latestTransaction != null )
            {
                return MapToDto(latestTransaction.Subscription);
            }
            return null;
        }

    }
}
