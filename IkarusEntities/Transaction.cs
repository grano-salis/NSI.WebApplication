using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Transaction
    {
        public long TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public long PaymentGatewayId { get; set; }
        public long PricingPackageId { get; set; }
        public long CustomerId { get; set; }

        public Customer Customer { get; set; }
        public PaymentGateway PaymentGateway { get; set; }
        public PricingPackage PricingPackage { get; set; }
    }
}
