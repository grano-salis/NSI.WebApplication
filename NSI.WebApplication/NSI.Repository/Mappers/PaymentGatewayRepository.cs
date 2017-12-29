using System;
using IkarusEntities;
using NSI.DC.PaymentGatewayRepository;

namespace NSI.Repository
{
    public partial class PaymentGatewayRepository
    {
        public static PaymentGateway MapToDbEntity(PaymentGatewayDto paymentGateway)
        {
            return new PaymentGateway()
            {
                PaymentGatewayId = paymentGateway.PaymentGatewayId,
                GatewayName = paymentGateway.GatewayName,
                IsActive = paymentGateway.IsActive
            };
        }

        public static PaymentGatewayDto MapToDto(PaymentGateway paymentGateway)
        {
            return new PaymentGatewayDto()
            {
                PaymentGatewayId = paymentGateway.PaymentGatewayId,
                GatewayName = paymentGateway.GatewayName,
                IsActive = paymentGateway.IsActive
            };
        }
    }
}
