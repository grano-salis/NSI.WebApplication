using System;
using System.Collections.Generic;
using NSI.DC.TransactionRepository;
namespace NSI.Repository.Interfaces
{
    public interface ITransactionRepository
    {
        TransactionDto GetTransaction(int transactionId);
        IEnumerable<TransactionDto> GetAllTransactions();
        TransactionDto SaveTransaction(TransactionDto transaction);

    }
}
