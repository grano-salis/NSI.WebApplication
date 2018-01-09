using System;
using System.Collections.Generic;
using NSI.DC.Mailer;

namespace NSI.BLL.Interfaces
{
    public interface IMailerService
    {
        void Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}
