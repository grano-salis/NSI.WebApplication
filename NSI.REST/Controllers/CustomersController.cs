using System;
using NSI.BLL;
using NSI.DC.CustomersRepository;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NSI.BLL.Interfaces;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/customer")]
    public class CustomersController : Controller
    {

        ICustomerManipulation _customersManipulation { get; set; }

        public CustomersController(ICustomerManipulation customersManipulation)
        {
            _customersManipulation = customersManipulation;
        }

        [HttpGet]
        public ActionResult GetCustomers()
        {   
            
            return Ok(_customersManipulation.GetCustomers());
        }
        [HttpGet("all")]
        public ActionResult GetAllCustomers()
        {   
            return Ok(_customersManipulation.GetAllCustomers());
        }

        [HttpGet("{id}")]
        public ActionResult GetCustomerById(int id)
        {
            return Ok(_customersManipulation.GetCustomerById(id));
        }

        [HttpPost]
        public ActionResult CreateNewCustomer(CustomerDto customerDto)
        {
            return Ok(_customersManipulation.CreateCustomer(customerDto));
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            return Ok(_customersManipulation.DeleteCustomerById(id));
        }

        [HttpPut]
        public ActionResult EditCustomer(CustomerDto customerDto)
        {
            return Ok(_customersManipulation.EditCustomer(customerDto));
        }

        [HttpPost("search")]
        public ActionResult SearchCustomers([FromBody]CustomerSearchDto customerSearch){
            return Ok(_customersManipulation.SearchCustomer(customerSearch));
        }


    }
}