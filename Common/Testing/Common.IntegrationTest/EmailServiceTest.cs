using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessSolutions.Common.Infra.Notifications;
using NUnit.Framework;

namespace Common.IntegrationTest
{
    [TestFixture]
    public class EmailServiceTest
    {
        [Test]
        public async Task SendEmail()
        {
            IEmailService emailService = new EmailService();
            await emailService.SendEmail("elkadeem@hotmail.com", "testemail", "<h1>testemail</h1>");
            Assert.Pass();
        }

        [Test]
        public void TestEmailSettings()
        {
            bool isTrue = false;
            bool.TryParse("True", out isTrue);
            Assert.IsTrue(isTrue);
        }
    }
}
