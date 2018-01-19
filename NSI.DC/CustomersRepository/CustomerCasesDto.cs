using System;
using System.Runtime.Serialization;
namespace NSI.DC.CustomersRepository
{
    [DataContract]
    public class CustomerCasesDto
    {
        [DataMember]
        public int? NumberOfCases { get; set; }
        [DataMember]
        public int? YearOfCases { get; set; }
        [DataMember]
        public int? MonthOfCases { get; set; }
    }
}
