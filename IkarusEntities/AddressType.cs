using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class AddressType
    {
        public AddressType()
        {
            Address = new HashSet<Address>();
        }

        public long AddressTypeId { get; set; }
        public long AddressTypeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public long? CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Address> Address { get; set; }
    }
}
