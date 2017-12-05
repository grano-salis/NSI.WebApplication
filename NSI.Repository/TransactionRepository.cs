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
            Transaction t = _dbContext.Transaction.FirstOrDefault(x => x.TransactionId == transactionId);
            return t != null ? TransactionRepository.MapToDto(t):null;

        }

        IEnumerable<TransactionDto> ITransactionRepository.GetAllTransactions()
        {
            return _dbContext.Transaction.ToList().Select(x => TransactionRepository.MapToDto(x));
            //return _dbContext.Transaction != null ? (ICollection<TransactionDto>)_dbContext.Transaction.ToList().Select(x => TransactionRepository.MapToDto(x)) : new ;
        }

        IEnumerable<TransactionDto> ITransactionRepository.GetAllTransactionsByCustomer(int customerId)
        {
            var r=_dbContext.Transaction.Where(x => x.CustomerId == customerId).Select(x => TransactionRepository.MapToDto(x)).ToList();

            return r;
            //return _dbContext.Transaction != null ? (ICollection<TransactionDto>)_dbContext.Transaction.ToList().Select(x => TransactionRepository.MapToDto(x)) : new ;
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
