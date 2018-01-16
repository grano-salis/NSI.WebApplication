using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
namespace NSI.DC.CustomersRepository
{
    [DataContract]
    public class CustomerReportDto
    {
        [DataMember]
        public int? CustomerId { get; set; }
        [DataMember]
        public String CustomerName { get; set; }
        [DataMember]
        public int? NumberOfClient { get; set; }
        [DataMember]
        public ICollection<CustomerCasesDto> Cases { get; set; }
    }
}
