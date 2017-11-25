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
            System.Console.WriteLine("tu smo!");
            _dbContext = dbContext;

        }

        TransactionDto ITransactionRepository.GetTransaction(int transactionId)
        {
            Transaction t = _dbContext.Transaction.FirstOrDefault(x => x.TransactionId == transactionId);
            return t != null ? TransactionRepository.MapToDto(t):null;

        }

        ICollection<TransactionDto> ITransactionRepository.GetAllTransactions()
        {
            return _dbContext.Transaction != null ? (ICollection<TransactionDto>)_dbContext.Transaction.ToList().Select(x => TransactionRepository.MapToDto(x)) : null;
        }

        long ITransactionRepository.SaveTransaction(TransactionDto transaction)
        {
            var t = _dbContext.Transaction.Add(TransactionRepository.MapToDbEntity(transaction));
            return _dbContext.SaveChanges();
        }


    }
}
