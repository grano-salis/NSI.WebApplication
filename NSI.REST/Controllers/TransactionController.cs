using System;
using NSI.BLL;
using NSI.DC.TransactionRepository;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NSI.BLL.Interfaces;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/Transactions")]
    public class TransactionController : Controller
    {
        private readonly ITransactionManipulation _transactionManipulation;

        public TransactionController(ITransactionManipulation trm){
            _transactionManipulation = trm;
        }

        [HttpGet]
        public IEnumerable<TransactionDto> GetTransactions()
        {
            return _transactionManipulation.GetTransactions();
        }

        [HttpGet("{id}")]
        public TransactionDto GetTransaction(int id)
        {
            return _transactionManipulation.GetTransaction(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody]TransactionDto transaction)
        {
            try{
                if (ModelState.IsValid)
                {
                    var result = _transactionManipulation.SaveTransaction(transaction);
                    if (result != null) return Ok(result);
                }
                else return BadRequest(transaction);
            }
            catch(Exception e){
                
            }
            return BadRequest();
        }

    }
}

