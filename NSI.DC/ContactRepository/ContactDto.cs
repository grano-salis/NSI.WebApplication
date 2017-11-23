using IkarusEntities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.ContactRepository
{
    [DataContract]
    public class ContactDto
    {
        [DataMember]
        public int Contact1 { get; set; }
        [DataMember]
        public string FirsttName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public int? AddressId { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public DateTime? ModifiedDate { get; set; }
        [DataMember]
        public bool? IsDeleted { get; set; }
        [DataMember]
        public int CreatedByUserId { get; set; }

        [DataMember]
        public Address Address { get; set; }
        [DataMember]
        public UserInfo CreatedByUser { get; set; }
        [DataMember]
        public ICollection<CaseContact> CaseContact { get; set; }
        [DataMember]
        public ICollection<ClientContact> ClientContact { get; set; }
        [DataMember]
        public ICollection<Email> Email { get; set; }
        [DataMember]
        public ICollection<Phone> Phone { get; set; }
    }
}
