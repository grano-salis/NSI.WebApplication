using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public int PaymentGatewayId { get; set; }
        public int PricingPackageId { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public PaymentGateway PaymentGateway { get; set; }
        public PricingPackage PricingPackage { get; set; }
    }
}
