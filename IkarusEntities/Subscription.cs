using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Subscription
    {
        public int SubscriptionId { get; set; }
        public int PricingPackageId { get; set; }
        public int CustomerId { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public bool RecurringPayment { get; set; }

        public Customer Customer { get; set; }
        public PricingPackage PricingPackage { get; set; }
    }
}
