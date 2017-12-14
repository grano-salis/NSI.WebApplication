using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using NSI.DC.AddressRepository;
using NSI.DC.PricingPackageRepository;

namespace NSI.DC.CustomersRepository
{
    [DataContract]
    public class CustomerSearchDto
    {
        [DataMember]
        public string CustomerName;

        [DataMember]
        public bool? IsActive;

        [DataMember]
        public bool? IsDeleted;

        [DataMember]
        public int? PricingPackageId;

        [DataMember]
        public int? AddressId;

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