using System;
using System.Linq;
using IkarusEntities;
using NSI.DC.TransactionRepository;
using NSI.Repository.Interfaces;
using System.Collections.Generic;

namespace NSI.Repository
{
    public partial class TransactionRepository : ITransactionRepository

    {
        private readonly IkarusContext _dbContext;

        public TransactionRepository(IkarusContext dbContext)
        {
            _dbContext = dbContext;

        }

        TransactionDto ITransactionRepository.GetTransaction(int transactionId)
        {
            Transaction transaction = _dbContext.Transaction.FirstOrDefault(x => x.TransactionId == transactionId);

            if (transaction != null) return TransactionRepository.MapToDto(transaction);
            else return null;

        }

        IEnumerable<TransactionDto> ITransactionRepository.GetAllTransactions()
        {
            return _dbContext.Transaction.ToList().Select(x => TransactionRepository.MapToDto(x));
        }

        IEnumerable<TransactionDto> ITransactionRepository.GetAllTransactionsByCustomer(int customerId)
        {
            List<TransactionDto> customerTransactions =_dbContext.Transaction.Where(x => x.CustomerId == customerId).Select(x => TransactionRepository.MapToDto(x)).ToList();

            return customerTransactions;

        }

        TransactionDto ITransactionRepository.SaveTransaction(TransactionDto transaction)
        {
            var newTransaction = MapToDbEntity(transaction);
            _dbContext.Transaction.Add(newTransaction);
            if (_dbContext.SaveChanges() != 0) return MapToDto(newTransaction);
            return null;
        }
    }
}
