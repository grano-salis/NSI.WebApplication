using System;
using System.Collections.Generic;
using NSI.DC.TransactionRepository;

namespace NSI.BLL.Interfaces
{
    public interface ITransactionManipulation
    {
        TransactionDto GetTransaction(int transactionId);
        IEnumerable<TransactionDto> GetTransactions();
        TransactionDto SaveTransaction(TransactionDto transaction);
    }
}
