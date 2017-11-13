using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Client
    {
        public Client()
        {
            CaseInfo = new HashSet<CaseInfo>();
            ClientContact = new HashSet<ClientContact>();
        }

        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
        public long ClientTypeId { get; set; }
        public long CustomerId { get; set; }
        public long? AddressId { get; set; }
        public long CreatedByUserId { get; set; }

        public Address Address { get; set; }
        public ClientType ClientType { get; set; }
        public UserInfo CreatedByUser { get; set; }
        public Customer Customer { get; set; }
        public ICollection<CaseInfo> CaseInfo { get; set; }
        public ICollection<ClientContact> ClientContact { get; set; }
    }
}
