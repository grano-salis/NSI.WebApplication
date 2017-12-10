using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DC.AddressRepository
{
    [DataContract]
    public class AddressTypeDto
    {
        [DataMember]
        public int AddressTypeId { get; set; }
        [DataMember]
        public string AddressTypeName { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public DateTime? ModifiedDate { get; set; }
        [DataMember]
        public bool? IsDeleted { get; set; }
    }
}
