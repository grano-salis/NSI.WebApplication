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

        public TransactionDto GetTransaction(int transactionId){
            return _transactionRepository.GetTransaction(transactionId);
        }

        public IEnumerable<TransactionDto> GetTransactions()
        {
            return _transactionRepository.GetAllTransactions();
        }

        public IEnumerable<TransactionDto> GetAllTransactionsByCustomer(int customerId)
        {
            return _transactionRepository.GetAllTransactionsByCustomer(customerId);
        }

        public TransactionDto SaveTransaction(TransactionDto transaction){
            return _transactionRepository.SaveTransaction(transaction);
        }
    }
}
