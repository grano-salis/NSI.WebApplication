using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.ContactsRepository
{
    public class ContactSearchCriteriaDto
    {
        [DataMember]
        public int TaskId { get; set; }
        [DataMember]
        public int Contact1 { get; set; }
        [DataMember]
        public string FirsttName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Mobile { get; set; }
        [DataMember]
        public string Email { get; set; }
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
    }
}
