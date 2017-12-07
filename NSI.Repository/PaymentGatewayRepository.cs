using System;
using System.Collections.Generic;
using System.Linq;
using IkarusEntities;
using NSI.DC.PaymentGatewayRepository;
using NSI.Repository.Interfaces;

namespace NSI.Repository
{
    public partial class PaymentGatewayRepository:IPaymentGatewayRepository
    {
        private readonly IkarusContext _dbContext;

        public PaymentGatewayRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;
        }

        PaymentGatewayDto IPaymentGatewayRepository.GetPaymentGateway(int paymentGatewayId)
        {
            PaymentGateway t = _dbContext.PaymentGateway.FirstOrDefault(x => x.PaymentGatewayId == paymentGatewayId);
            return t != null ? PaymentGatewayRepository.MapToDto(t) : null;

        }

        IEnumerable<PaymentGatewayDto> IPaymentGatewayRepository.GetAllPaymentGateways()
        {
            return _dbContext.PaymentGateway.ToList().Select(x => PaymentGatewayRepository.MapToDto(x));
        }


        public PaymentGatewayDto SavePaymentGateway(PaymentGatewayDto paymentGateway){
            var newPaymentGateway = MapToDbEntity(paymentGateway);
            _dbContext.PaymentGateway.Add(newPaymentGateway);
            if (_dbContext.SaveChanges() != 0) return MapToDto(newPaymentGateway);
            return null;
        }
    }
}
