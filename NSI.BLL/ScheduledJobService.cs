using System;
using System.Collections.Generic;
using NSI.BLL.Interfaces;
using NSI.DC.Mailer;
using NSI.DC.SubscriptionRepository;

namespace NSI.BLL
{
    public class ScheduledJobService:IScheduledJobService
    {
        private readonly IMailerService _mailService;
        private readonly ISubscriptionManipulation _subscriptionManipulation;
        public ScheduledJobService(IMailerService mailService, ISubscriptionManipulation subscriptionManipulation){
            _mailService = mailService;
            _subscriptionManipulation = subscriptionManipulation;
        }

        private void sendNotificationMail(String recepientName,String recepientEMail,String text){
            
            List<EmailAddress> toAddresses = new List<EmailAddress>();
            toAddresses.Add(new EmailAddress(){Name=recepientName,Address=recepientEMail});

            List<EmailAddress> senderAddresses = new List<EmailAddress>();
            senderAddresses.Add(new EmailAddress(){Name="NSI Developer",Address="nsi2017developer@gmail.com"});

            EmailMessage mail = new EmailMessage()
            {
                ToAddresses = toAddresses,
                FromAddresses = senderAddresses,
                Content = text,
                Subject = "NSI Lawyers subscription notification "
            };
            _mailService.Send(mail);
        }

        private void unsubscribeUser(int userId){
            var activeSubscription = _subscriptionManipulation.GetCustomerSubscription(userId);
            activeSubscription.IsActive = false;
            _subscriptionManipulation.UpdateSubscription(activeSubscription);
        } 

        public void DailyJob(){
            List<SubscriptionDto> activeSubscriptions = (List<NSI.DC.SubscriptionRepository.SubscriptionDto>)_subscriptionManipulation.GetActiveSubscriptions();
            DateTime now = DateTime.Now;
            foreach(SubscriptionDto subscription in activeSubscriptions){
                int dayDifference = (subscription.SubscriptionExpirationDate - now).Days;
                if(dayDifference>=0 && dayDifference<=5){

                    //this is the part where a user and his mail should be retrieved
                    //as there is no real user handling in the application, hardcoded
                    //values will be used for proof of concept testing
                    string mailContent = "Hello NSI Test User, \n you are receiving this mail because your active subscription ends in ";
                    mailContent += dayDifference.ToString();
                    mailContent += "days.";
                    sendNotificationMail("NSI Test User","nsi2017developer@gmail.com",mailContent);
                }
                else if(dayDifference<0){
                    
                    this.unsubscribeUser(subscription.CustomerId);

                    //this is the part where a user and his mail should be retrieved
                    //as there is no real user handling in the application, hardcoded
                    //values will be used for proof of concept testing
                    string mailContent = "Hello NSI Test User, \n you are receiving this mail because your subscription just ended. We are hoping that you will subscribe to our services again";
                    sendNotificationMail("NSI Test User", "nsi2017developer@gmail.com", mailContent);
                }
            }
        }

        public void MonthlyJob(){
            System.Console.WriteLine("A monthly recurrent job isn't implemented yet!");
        }
    }
}
