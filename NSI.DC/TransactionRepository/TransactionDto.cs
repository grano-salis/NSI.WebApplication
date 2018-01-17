﻿using System;
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
        [DataMember]
        public DateTime DateCreated { get; set; }
        [DataMember]
        public int PaymentGatewayId { get; set; }
        [DataMember]
        public int PricingPackageId { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}
