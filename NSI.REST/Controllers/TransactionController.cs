using System;
using NSI.BLL;
using NSI.DC.TransactionRepository;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/Transactions")]
    public class TransactionController : Controller
    {
        TransactionManipulation _transactionManipulation;

        public TransactionController(TransactionManipulation trm){
            _transactionManipulation = trm;
        }

        [HttpGet]
        public ICollection<TransactionDto> Get()
        {
            System.Console.WriteLine("workd");
            return _transactionManipulation.GetTransactions();
        }


    }
}

