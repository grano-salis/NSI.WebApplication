using System;
using System.Runtime.Serialization;

namespace NSI.DC.PaymentGatewayRepository
{
    [DataContract]
    public class PaymentGatewayDto
    {
        [DataMember]
        public int PaymentGatewayId { get; set; }
        [DataMember]
        public string GatewayName { get; set; }
        [DataMember]
        public bool? IsActive { get; set; }
    }
}
