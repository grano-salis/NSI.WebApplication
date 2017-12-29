using System;
using System.Collections.Generic;
using NSI.BLL.Interfaces;
using NSI.DC.PaymentGatewayRepository;
using NSI.Repository.Interfaces;

namespace NSI.BLL
{
    public class PaymentGatewayManipulation:IPaymentGatewayManipulation
    {
        private readonly IPaymentGatewayRepository _paymentGatewayRepository;

        public PaymentGatewayManipulation(IPaymentGatewayRepository paymentGatewayRepository)
        {
            _paymentGatewayRepository = paymentGatewayRepository;
        }

        public PaymentGatewayDto GetPaymentGateway(int paymentGatewayId)
        {
            return _paymentGatewayRepository.GetPaymentGateway(paymentGatewayId);
        }

        public IEnumerable<PaymentGatewayDto> GetPaymentGateways()
        {
            return _paymentGatewayRepository.GetAllPaymentGateways();
        }

        public PaymentGatewayDto SavePaymentGateway(PaymentGatewayDto paymentGateway)
        {
            return _paymentGatewayRepository.SavePaymentGateway(paymentGateway);
        }
    }
}
