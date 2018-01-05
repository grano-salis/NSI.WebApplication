using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class PricingPackage
    {
        public PricingPackage()
        {
            Customer = new HashSet<Customer>();
            Subscription = new HashSet<Subscription>();
            Transaction = new HashSet<Transaction>();
        }

        public int PricingPackageId { get; set; }
        public long PricingPackageName { get; set; }
        public bool? IsActive { get; set; }
        public decimal Price { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string Description { get; set; }

        public ICollection<Customer> Customer { get; set; }
        public ICollection<Subscription> Subscription { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
    }
}
