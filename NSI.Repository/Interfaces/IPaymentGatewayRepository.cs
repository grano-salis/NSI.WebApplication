using System;
using System.Collections.Generic;
using NSI.DC.PaymentGatewayRepository;

namespace NSI.Repository.Interfaces
{
    public interface IPaymentGatewayRepository
    {
        PaymentGatewayDto GetPaymentGateway(int paymentGatewayId);
        IEnumerable<PaymentGatewayDto> GetAllPaymentGateways();
        PaymentGatewayDto SavePaymentGateway(PaymentGatewayDto paymentGateway);
    }
}
