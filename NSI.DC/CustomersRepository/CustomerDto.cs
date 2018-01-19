﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.CustomersRepository
{
    [DataContract]
    public class CustomerDto
    {
        [DataMember]
        public int? CustomerId { get; set; }
        [DataMember]
        public String CustomerName  { get; set; }
        [DataMember]
        public bool? IsActive { get; set; }
        [DataMember]
        public DateTime? DateCreated { get; set; }
        [DataMember]
        public DateTime? DateModified { get; set; }
        [DataMember]
        public bool? IsDeleted { get; set; }
        [DataMember]
        public int? PricingPackageId { get; set; }
        [DataMember]
        public int? AddressId { get; set; }
        [DataMember]
        public string logoLink { get; set; }
    }
}
