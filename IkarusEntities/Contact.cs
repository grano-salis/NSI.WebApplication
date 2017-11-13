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
        }

        public long Contact1 { get; set; }
        public string FirsttName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public long? AddressId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public long CreatedByUserId { get; set; }

        public Address Address { get; set; }
        public UserInfo CreatedByUser { get; set; }
        public ICollection<CaseContact> CaseContact { get; set; }
        public ICollection<ClientContact> ClientContact { get; set; }
    }
}
