using System;
using System.Collections.Generic;
using NSI.DC.TransactionRepository;
namespace NSI.Repository.Interfaces
{
    public interface ITransactionRepository
    {
        TransactionDto GetTransaction(int transactionId);
        ICollection<TransactionDto> GetAllTransactions();
        //IEnumerable<TransactionDto> SearchTransactions(DocumentSearchCriteriaDto searchCriteria);
        long SaveTransaction(TransactionDto transaction);

    }
}
