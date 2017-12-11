using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.DC.PaymentGatewayRepository;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PaymentGatewayController:Controller
    {
        private readonly IPaymentGatewayManipulation _paymentGatewayManipulation; 
        public PaymentGatewayController(IPaymentGatewayManipulation paymentGatewayManipulation)
        {
            _paymentGatewayManipulation = paymentGatewayManipulation;
        }

        [HttpGet]
        public IEnumerable<PaymentGatewayDto> GetPaymentGateways()
        {
            return _paymentGatewayManipulation.GetPaymentGateways();
        }

        [HttpGet("{id}")]
        public IActionResult GetPaymentGateway(int id)
        {
            var paymentGateway = _paymentGatewayManipulation.GetPaymentGateway(id);
            if (paymentGateway == null)
            {
                return BadRequest(id);
            }
            return Ok(paymentGateway);
        }

        [HttpPost]
        public IActionResult Post([FromBody]PaymentGatewayDto paymentGateway)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _paymentGatewayManipulation.SavePaymentGateway(paymentGateway);
                    if (result != null) return Ok(result);
                }
                else return BadRequest(paymentGateway);
            }
            catch (Exception e)
            {

            }
            return BadRequest();
        }
    }
}
