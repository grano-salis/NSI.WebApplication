using System;
using NSI.BLL;
using NSI.DC.TransactionRepository;
using NSI.DC.SubscriptionRepository;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NSI.BLL.Interfaces;
using Stripe.net;
using Stripe;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/Transactions")]
    public class TransactionController : Controller
    {
        private readonly ITransactionManipulation _transactionManipulation;
        private readonly ISubscriptionManipulation _subscriptionManipulation;

        //Braintree gateway instantiation, used for paypal
        private Braintree.BraintreeGateway gateway = new Braintree.BraintreeGateway("access_token$sandbox$f2j8p8tkw323cc6r$533d48e1dff3204ffb1f8a2ccafb85b1");

        public TransactionController(ITransactionManipulation trm, ISubscriptionManipulation sm ){
            _transactionManipulation = trm;
            _subscriptionManipulation = sm;
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
                    transaction.DateCreated = DateTime.Now;
                    // Ovdje treba uraditi provjeru statusa?
                    transaction.Status = "Succeeded";
                    var result = _transactionManipulation.SaveTransaction(transaction);
                    if (result != null) return Ok(result);
                }
                else return BadRequest(transaction);
            }
            catch(Exception e){
                Logger.Logger.LogError(e);
            }
            return BadRequest();
        }

        [HttpPost("MakePayment")]
        public IActionResult Post([FromBody] StripePaymentRequest paymentRequest){

            //Stripe developer api key
            StripeConfiguration.SetApiKey("sk_test_IhD98M0gMGB1G7rbcHifS3GP");

            //configuration of stripe chrarge object
            var myCharge = new StripeChargeCreateOptions();
            myCharge.SourceTokenOrExistingSourceId = paymentRequest.tokenId;
            myCharge.Amount = paymentRequest.amount;
            myCharge.Currency = "gbp";
            myCharge.Description = paymentRequest.productName;
            myCharge.Metadata = new Dictionary<string, string>();
            myCharge.Metadata["OurRef"] = "OurRef-" + Guid.NewGuid().ToString();

            var chargeService = new StripeChargeService();
            StripeCharge stripeCharge = chargeService.Create(myCharge);
            if(stripeCharge.Status.Equals("succeeded")){
                
                TransactionDto transaction = new TransactionDto();
                transaction.Amount = (decimal)(paymentRequest.amount / 100.0);
                transaction.Status = "succeeded";
                transaction.CustomerId = 1;
                transaction.PaymentGatewayId = 1;
                transaction.PricingPackageId = paymentRequest.packageId;
                transaction.DateCreated = DateTime.Now;
                _transactionManipulation.SaveTransaction(transaction);


                SubscriptionDto subscription = new SubscriptionDto();
                subscription.CustomerId = 1;
                subscription.PricingPackageId = paymentRequest.packageId;

                _subscriptionManipulation.GetCustomerSubscription(1);

                _subscriptionManipulation.SaveSubscription(subscription);

            }
            return Ok(stripeCharge);
        }



        [HttpGet("PaypalToken")]
        public String CreateClientPaypalToken(){
            String clientToken = gateway.ClientToken.generate();
            return clientToken;
        }

        [HttpPost("CreatePaypalTransaction")]
        public IActionResult CreatePaypalTransaction([FromBody] PaypalPaymentRequest paymentRequest){
            Console.WriteLine(paymentRequest.paymentNonce);
            Braintree.TransactionRequest request = new Braintree.TransactionRequest()
            {
                PaymentMethodNonce = paymentRequest.paymentNonce,
                Amount = (decimal)(paymentRequest.amount)
            };

            Braintree.Result<Braintree.Transaction> result = gateway.Transaction.Sale(request);
            if(result.IsSuccess()) 
            {

                TransactionDto transaction = new TransactionDto();
                transaction.Amount = (decimal)(paymentRequest.amount);
                transaction.Status = "succeeded";
                transaction.CustomerId = 1;
                transaction.PaymentGatewayId = 2;
                transaction.PricingPackageId = paymentRequest.packageId;
                transaction.DateCreated = DateTime.Now;
                _transactionManipulation.SaveTransaction(transaction);


                SubscriptionDto subscription = new SubscriptionDto();
                subscription.CustomerId = 1;
                subscription.PricingPackageId = paymentRequest.packageId;

                _subscriptionManipulation.GetCustomerSubscription(1);

                _subscriptionManipulation.SaveSubscription(subscription);
                return Ok("Uspjesan placanje");
            }
            else
            {
                return BadRequest("Neuspjesna transakcija!");
            }
        }

        public class StripePaymentRequest
        {
            public string tokenId { get; set; }
            public string productName { get; set; }
            public int amount { get; set; }
            public int packageId { get; set; }
        }

        public class PaypalPaymentRequest
        {
            public string paymentNonce { get; set; }
            public double amount { get; set; }
            public int packageId { get; set; }
        }

    }
}

