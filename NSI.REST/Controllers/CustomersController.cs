﻿using System;
using NSI.BLL;
using NSI.DC.CustomersRepository;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NSI.BLL.Interfaces;
using NSI.DC.Exceptions;

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
            List<CustomerDto> CustomerDto=_customersManipulation.GetCustomers().ToList();
            CustomerDto.ForEach(x => x.logoLink="https://www.seoclerk.com/pics/want54841-1To5V31505980185.png");
            return Ok( CustomerDto );
            //return Ok(_customersManipulation.GetCustomers());
        }
        [HttpGet("all")]
        public ActionResult GetAllCustomers()
        {   
            List<CustomerDto> CustomerDto=_customersManipulation.GetAllCustomers().ToList();
            CustomerDto.ForEach(x => x.logoLink="https://www.seoclerk.com/pics/want54841-1To5V31505980185.png");
            return Ok( CustomerDto );
        }

        [HttpGet("{id}")]
        public ActionResult GetCustomerById(int id)
        {
            return Ok(_customersManipulation.GetCustomerById(id));
        }

        [HttpPost]
        public ActionResult CreateNewCustomer([FromBody]CustomerDto customerDto)
        {
            try{
                return Ok(_customersManipulation.CreateCustomer(customerDto));
            }catch(NSIException error){
                //if(error.)
                return BadRequest(error.Message);
            } catch(Exception ex){
                
                return StatusCode(500,ex.Message);;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            try{
                return Ok(_customersManipulation.DeleteCustomerById(id));
            }catch(Exception error){
                return BadRequest(error.Message);
            }
        }


        [HttpPut("{id}")]
        public ActionResult EditCustomer(int id,[FromBody]CustomerDto customerDto)
        {
            try{
            return Ok(_customersManipulation.EditCustomer(id, customerDto));
            }catch(Exception error){
                return BadRequest(error.Message);
            }
        }

        [HttpPost("search")]
        public ActionResult SearchCustomers([FromBody]CustomerSearchDto customerSearch){
            return Ok(_customersManipulation.SearchCustomer(customerSearch));
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
