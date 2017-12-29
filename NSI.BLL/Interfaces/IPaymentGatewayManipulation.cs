using System;
using System.Collections.Generic;
using NSI.DC.PaymentGatewayRepository;

namespace NSI.BLL.Interfaces
{
    public interface IPaymentGatewayManipulation
    {
        PaymentGatewayDto GetPaymentGateway(int paymentGatewayId);
        IEnumerable<PaymentGatewayDto> GetPaymentGateways();
        PaymentGatewayDto SavePaymentGateway(PaymentGatewayDto paymentGateway);
    }
}
