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
        private readonly IEmailService _emailService;
        public EmailService(IEmailService emailService)
        {
            if (emailService == null)
                throw new ArgumentNullException("emailService");
            _emailService = emailService;
        }

        public async Task SendAsync(IdentityMessage message)
        {
            
                await _emailService.SendEmail(message.Destination, message.Subject, message.Body);
            
        }
    }
}
