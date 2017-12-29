using System;
using System.Runtime.Serialization;

namespace NSI.DC.ClientsRepository
{
    [DataContract]
    public class ClientSearchDTO
    {
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public bool? IsDeleted { get; set; }
        [DataMember]
        public int ClientTypeId { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public int? AddressId { get; set; }
        [DataMember]
        public DateTime? FromCreated;
        [DataMember]
        public DateTime? ToCreated;
        [DataMember]
        public DateTime? FromModified;
        [DataMember]
        public DateTime? ToModified;
    }
}
