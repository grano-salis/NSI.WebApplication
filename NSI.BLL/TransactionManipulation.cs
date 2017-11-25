using System;
using System.Collections.Generic;
using NSI.BLL.Interfaces;
using NSI.DC.TransactionRepository;
using NSI.Repository;
using NSI.Repository.Interfaces;
namespace NSI.BLL
{
    public class TransactionManipulation:ITransactionManipulation
    {
        private ITransactionRepository _transactionRepository;
        public TransactionManipulation(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public ICollection<TransactionDto> GetTransactions()
        {
            return _transactionRepository.GetAllTransactions();
        }
    }
}
