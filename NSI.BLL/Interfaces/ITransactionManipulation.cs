using System;
using System.Collections.Generic;

namespace NSI.BLL.Interfaces
{
    public interface ITransactionManipulation
    {
        ICollection<DC.TransactionRepository.TransactionDto> GetTransactions();
    }
}
