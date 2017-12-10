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
        //[Route("ByCustomer/{customerId}")]
        [HttpGet("{customerId}")]
        public IEnumerable<TransactionDto> GetTransactionsByCustomer(int customerId)
        {
            System.Console.WriteLine("iiiiiiiiiiiiiiiiiiiiii?");
            System.Console.WriteLine(customerId);
            return _transactionManipulation.GetAllTransactionsByCustomer(customerId);
        }

        [HttpGet("{id}")]
        public IActionResult GetTransaction(int id)
        {
            var transaction = _transactionManipulation.GetTransaction(id);
            if(transaction == null){
                return BadRequest(id);
            }
            return Ok(transaction);
        }

        [HttpPost]
        public IActionResult Post([FromBody]TransactionDto transaction)
        {
            try{
                if (ModelState.IsValid)
                {
                    // Ovdje bi vjerovatno trebalo povuci pricingpackage radi eventualne provjere
                    transaction.DateCreated = new DateTime();
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

