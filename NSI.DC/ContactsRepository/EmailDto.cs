using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.ContactsRepository
{
    [DataContract]
    public class EmailDto
    {
        [DataMember]
        public int EmailId { get; set; }
        [DataMember]
        public int ContactId { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
    }
}
