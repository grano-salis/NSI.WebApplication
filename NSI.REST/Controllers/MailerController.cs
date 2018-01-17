using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.DC.Mailer;

namespace NSI.REST.Controllers
{
    [Route("api/[controller]")]
    public class MailerController:Controller
    {
        private readonly IMailerService _mailerService;

        public MailerController(IMailerService mailerService)
        {
            _mailerService = mailerService;
        }


        //this is a method exposed for testing if the mailer works
        [HttpGet]
        public IActionResult send(){
            List<EmailAddress> addresses = new List<EmailAddress>();
            addresses.Add(new EmailAddress(){Name="NSI dev",Address="nsi2017developer@gmail.com"});
            EmailMessage message = new EmailMessage()
            {
                Subject = "NSI Mail Test",
                Content = "This is a test message",
                ToAddresses = addresses,
                FromAddresses = addresses

            };
            try{
                _mailerService.Send(message);
            }
            catch(Exception e){
                Logger.Logger.LogError(e.Message);
                return new BadRequestObjectResult("neuspjesan zahtjev");

            }
           
            return new OkResult();
        }

    }
}
