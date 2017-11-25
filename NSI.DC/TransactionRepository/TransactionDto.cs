using System;
using System.Runtime.Serialization;

namespace NSI.DC.TransactionRepository
{
    [DataContract]
    public class TransactionDto
    {
        [DataMember]
        public int TransactionId { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

    }
}
