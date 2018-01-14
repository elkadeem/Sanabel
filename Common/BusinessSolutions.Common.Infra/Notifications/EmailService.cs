using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace BusinessSolutions.Common.Infra.Notifications
{
    public class EmailService : IEmailService
    {
        public string FromEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["FromEmail"];
            }
        }

        public string FromEmailName
        {
            get
            {
                return ConfigurationManager.AppSettings["FromEmailName"];
            }
        }

        public string MailServer
        {
            get
            {
                return ConfigurationManager.AppSettings["MailServer"];
            }
        }

        public int MailServerPort
        {
            get
            {
                int port = 0;
                int.TryParse(ConfigurationManager.AppSettings["MailServerPort"], out port);
                return port;
            }
        }

        public string MailUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["MailUserName"];
            }
        }

        public string MailPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["MailPassword"];
            }
        }

        public bool EmailIsSSL
        {
            get
            {
                bool emailIsSSL = false;
                bool.TryParse(ConfigurationManager.AppSettings["EmailIsSSL"], out emailIsSSL);
                return emailIsSSL;
            }
        }

        public bool EmailIsAuthenticated
        {
            get
            {
                bool emailIsAuthenticated = false;
                bool.TryParse(ConfigurationManager.AppSettings["EmailIsAuthenticated"], out emailIsAuthenticated);
                return emailIsAuthenticated;
            }
        }

        public EmailService()
        {

        }

        public Task SendEmail(string to, string subject, string body, string cc = "", string bCc = "")
        {
            if (string.IsNullOrEmpty(to))
                throw new ArgumentNullException("tos");

            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException("subject");

            if (string.IsNullOrEmpty(body))
                throw new ArgumentNullException("body");

            var message = new MimeMessage();
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            message.From.Add(new MailboxAddress(FromEmailName, FromEmail));
            AddAddress(to, message.To);
            AddAddress(cc, message.Cc);
            AddAddress(bCc, message.Bcc);

            return SendMessage(message);
        }

        private async Task SendMessage(MimeMessage message)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                await client.ConnectAsync(MailServer, MailServerPort
                    , EmailIsSSL);

                if (EmailIsAuthenticated)
                {
                    await client.AuthenticateAsync(MailUserName, MailPassword);
                }

                await client.SendAsync(message);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddAddress(string addresses, InternetAddressList internetAddressList)
        {
            if (!string.IsNullOrEmpty(addresses))
            {
                string[] toAddress = addresses.Split(',');
                foreach (var address in toAddress)
                    internetAddressList.Add(new MailboxAddress(address));
            }
        }
    }
}
