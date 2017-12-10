using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.ContactsRepository
{
    [DataContract]
    public class PhoneDto
    {
        [DataMember]
        public int PhoneId { get; set; }
        [DataMember]
        public int ContactId { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
    }
}
