using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.DC.SubscriptionRepository;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SubscriptionController:Controller
    {
        private readonly ISubscriptionManipulation _subscriptionManipulation;
        public SubscriptionController(ISubscriptionManipulation subscriptionManipulation)
        {
            _subscriptionManipulation = subscriptionManipulation;
        }

        [HttpGet]
        public IEnumerable<subscriptionDto> GetAllSubscriptions()
        {
            return _subscriptionManipulation.GetAllSubscriptions();
        }

        [HttpGet("{id}")]
        public IActionResult GetSubscription(int id){
            var subscription = _subscriptionManipulation.GetSubscription(id);
            if(subscription==null){
                return BadRequest(id);
            }
            return Ok(subscription);
        }

        [HttpGet("Active")]
        public IEnumerable<SubscriptionDto> GetActiveSubscriptions()
        {
            return _subscriptionManipulation.GetActiveSubscriptions();
        }

        [HttpPost]
        public IActionResult Post([FromBody] SubscriptionDto subscription)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Ovdje bi vjerovatno trebalo povuci subscriptione radi eventualne provjere
                    subscription.SubscriptionStartDate = new DateTime();
                    subscription.SubscriptionEndDate = subscription.SubscriptionStartDate;
                    var result = _subscriptionManipulation.SaveSubscription(subscription);
                    if (result != null) return Ok(result);
                }
                else return BadRequest(subscription);
            }
            catch (Exception e)
            {

            }
            return BadRequest();
        }
    }
}
