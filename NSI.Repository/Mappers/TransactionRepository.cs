using System;
using IkarusEntities;
using NSI.DC.TransactionRepository;

namespace NSI.Repository
{
    public partial class TransactionRepository
    {
        public static Transaction MapToDbEntity(TransactionDto transaction)
        {
            return new Transaction()
            {
                TransactionId = transaction.TransactionId,
                Amount = transaction.Amount,
                DateCreated = transaction.DateCreated,
                PricingPackageId = transaction.PricingPackageId,
                PaymentGatewayId = transaction.PaymentGatewayId,
                CustomerId = transaction.CustomerId
            };
        }

        public static TransactionDto MapToDto(Transaction transaction)
        {
            return new TransactionDto()
            {
                TransactionId = transaction.TransactionId,
                Amount = transaction.Amount,
                DateCreated = transaction.DateCreated,
                PricingPackageId = transaction.PricingPackageId,
                PaymentGatewayId = transaction.PaymentGatewayId,
                CustomerId = transaction.CustomerId
            };
        }

    }
}
