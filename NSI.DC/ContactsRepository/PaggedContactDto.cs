using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.ContactsRepository
{
    [DataContract]
    public class PaggedContactDto
    {
        [DataMember]
        public int Total { get; set; }
        [DataMember]
        public ICollection<ContactDto> Contacts { get; set; }
    }
}
