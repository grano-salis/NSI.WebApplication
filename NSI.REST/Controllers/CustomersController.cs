using System;
using NSI.BLL;
using NSI.DC.CustomersRepository;

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
        public ActionResult GetAllCustomers()
        {
            return Ok(_customersManipulation.GetCustomers());
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

        [HttpPut("{id}")]
        public ActionResult EditCustomer(int id, CustomerDto customerDto)
        {
            return Ok(_customersManipulation.EditCustomer(id, customerDto));
        }

        [HttpGet("clientno")]
        public ActionResult GetCustomerClients()
        {
            return Ok(_customersManipulation.GetCustomerClients());
        }

        [HttpGet("caseyearly/{CustomerId}")]
        public ActionResult GetCustomerCasesYearly(int CustomerId)
        {
            return Ok(_customersManipulation.GetCustomerCasesYearly(CustomerId));
        }

        [HttpGet("casemonthly/{CustomerId}")]
        public ActionResult GetCustomerCasesMonthly(int CustomerId, int Year)
        {
            return Ok(_customersManipulation.GetCustomerCasesMonthly(CustomerId,Year));
        }
    }
}
