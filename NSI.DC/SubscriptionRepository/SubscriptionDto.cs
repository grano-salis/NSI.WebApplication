using System;
using System.Runtime.Serialization;

namespace NSI.DC.SubscriptionRepository
{
    [DataContract]
    public class SubscriptionDto
    {
        [DataMember]
        public int SubscriptionId{ get; set; }
        [DataMember]
        public int PricingPackageId { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public DateTime SubscriptionStartDate { get; set; }
        [DataMember]
        public DateTime SubscriptionExpirationDate { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public bool RecurringPayment { get; set; }
        [DataMember]
        public Customer Customer { get; set; }
        [DataMember]
        public PricingPackage PricingPackage { get; set; }
    }
}