using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Customer
    {
        public Customer()
        {
            AddressType = new HashSet<AddressType>();
            CaseCategory = new HashSet<CaseCategory>();
            CaseInfo = new HashSet<CaseInfo>();
            Client = new HashSet<Client>();
            ClientType = new HashSet<ClientType>();
            DocumentCategory = new HashSet<DocumentCategory>();
            Transaction = new HashSet<Transaction>();
            UserInfo = new HashSet<UserInfo>();
        }

        public long CustomerId { get; set; }
        public long CustomerName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
        public long? PricingPackageId { get; set; }
        public long? AddressId { get; set; }

        public Address Address { get; set; }
        public PricingPackage PricingPackage { get; set; }
        public ICollection<AddressType> AddressType { get; set; }
        public ICollection<CaseCategory> CaseCategory { get; set; }
        public ICollection<CaseInfo> CaseInfo { get; set; }
        public ICollection<Client> Client { get; set; }
        public ICollection<ClientType> ClientType { get; set; }
        public ICollection<DocumentCategory> DocumentCategory { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
        public ICollection<UserInfo> UserInfo { get; set; }
    }
}
