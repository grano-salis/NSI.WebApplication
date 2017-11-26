using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DC.AddressRepository
{
    [DataContract]
    public class AddressDto
    {
        [DataMember]
        public int AddressId { get; set; }
        [DataMember]
        public string Address1 { get; set; }
        [DataMember]
        public string Address2 { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        public int AddressTypeId { get; set; }
        [DataMember]
        public int CreatedByUserId { get; set; }
    }
}
