using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.AddressTypeDto
{
    [DataContract]
    public class AddressTypeDto
    {
        [DataMember]
        public int AddressTypeId { get; set; }
        [DataMember]
        public string AddressTypeName { get; set; }
        [DataMember]
        public int? CustomerId { get; set; }
    }
}
