using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Contact
    {
        public Contact()
        {
            CaseContact = new HashSet<CaseContact>();
            ClientContact = new HashSet<ClientContact>();
            Email = new HashSet<Email>();
            Phone = new HashSet<Phone>();
        }

        public int Contact1 { get; set; }
        public string FirsttName { get; set; }
        public string LastName { get; set; }
        public int? AddressId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int CreatedByUserId { get; set; }

        public Address Address { get; set; }
        public UserInfo CreatedByUser { get; set; }
        public ICollection<CaseContact> CaseContact { get; set; }
        public ICollection<ClientContact> ClientContact { get; set; }
        public ICollection<Email> Email { get; set; }
        public ICollection<Phone> Phone { get; set; }
    }
}
