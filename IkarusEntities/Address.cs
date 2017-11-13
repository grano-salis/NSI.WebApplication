using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Address
    {
        public Address()
        {
            Client = new HashSet<Client>();
            Contact = new HashSet<Contact>();
            Customer = new HashSet<Customer>();
        }

        public long AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
        public long AddressTypeId { get; set; }
        public long CreatedByUserId { get; set; }

        public AddressType AddressType { get; set; }
        public UserInfo CreatedByUser { get; set; }
        public ICollection<Client> Client { get; set; }
        public ICollection<Contact> Contact { get; set; }
        public ICollection<Customer> Customer { get; set; }
    }
}
