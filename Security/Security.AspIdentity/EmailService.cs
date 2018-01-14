using BusinessSolutions.Common.Infra.Notifications;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.AspIdentity
{
    public class EmailService : IIdentityMessageService
    {
        private IEmailService _emailService;
        public EmailService(IEmailService emailService)
        {
            if (emailService == null)
                throw new ArgumentNullException("emailService");
            _emailService = emailService;
        }

        public Task SendAsync(IdentityMessage message)
        {
            return _emailService.SendEmail(message.Destination, message.Subject, message.Body);
        }
    }
}
