using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sanabel.Presentation;
using Sanabel.Presentation.Controllers;
using System.Net.Http;

namespace Sanabel.Presentation.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestClient()
        {
            HttpClient client = new HttpClient();
            for (int i = 0; i <= 10; i++)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", "jOKmVKau0ZJ72ryndTXuSX3tULbRFHR-oUSy_GwyozgOmkK52VrYwa-GMHZ1Js9glFGTcYOBloU7fiWkee1PlWT3soWr8f3f5-tiPDx2d7FI47MGKeJeg323PHJqPaEaUj3mPmIE5TRP-VDVGujL66HMiFptZDpvp9rMvVwBOOrr12bf8b5jNTZv3DzOC9WJ4dxjKoK6PPpQpW70MqWAD61ojbpJnw6hG9VI6bC6MCVa3IL1F0e2GypaqlEPRq_kX9PcIy1c0dMBqaNRnZfsJ5jt1JATdMdADg94nOTJI6ryVQGueMO6yEZl4Cw4CzJ61HfHtUKiAJh_DAk6xAgZ4QFt-fcU8Pa1do7G2rhpI6Andh1aPGTxAFZ7xRRxi0rYxKVQHwC-UzJTumvgwTEW_PK-BTP9GsCN8atL226YPhlRnX3oL_xR_f0FdLJfB9nq9GZZXNsNVYYU7gOti9DWc3i94ACPp-SD9je79GsbWDIYjc0gOKuGZJPMDyI292ws6JAJYRn4QjhI0qE3tP64xFk1Ym3ihW5nylncgAlTE_r_wWynmJybTjNxqL0ASBGR4ck8AZA7B8pafPykkWPmq4X2eb7ewN8XZsU5kBOUQyXMsceKKpDJTQ2YULqg2-ZKA8bvymgkETKv1kX7IJ3cbw");
                var task = client.GetAsync("https://apps.balady.gov.sa/InteractiveService/RatingMobile/api/Rating/GetRegions?timeStamp=" + Guid.NewGuid());
                task.Wait();

                var result = task.Result;
                if (!result.IsSuccessStatusCode)
                    throw new Exception();
            }

            Assert.IsTrue(true);

        }
    }
}
