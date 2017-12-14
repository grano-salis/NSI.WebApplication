using System;
using System.Runtime.Serialization;

namespace NSI.DC.ClientsRepository
{
    [DataContract]
    public class ClientDTO
    {
        [DataMember]
        public int? ClientId { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public DateTime? DateCreated { get; set; }
        [DataMember]
        public DateTime? DateModified { get; set; }
        [DataMember]
        public bool? IsDeleted { get; set; }
        [DataMember]
        public int ClientTypeId { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public int? AddressId { get; set; }
        [DataMember]
        public int CreatedByUserId { get; set; }
    }
}
