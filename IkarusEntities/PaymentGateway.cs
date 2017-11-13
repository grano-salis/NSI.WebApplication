using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class PaymentGateway
    {
        public PaymentGateway()
        {
            Transaction = new HashSet<Transaction>();
        }

        public long PaymentGatewayId { get; set; }
        public string GatewayName { get; set; }
        public bool? IsActive { get; set; }

        public ICollection<Transaction> Transaction { get; set; }
    }
}
