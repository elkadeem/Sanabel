using BusinessSolutions.Common.Infra.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.IntegrationTest
{
    public class TestLogger : ILogger
    {
        public void Error(Exception exception)
        {
            NUnit.Framework.TestContext.WriteLine($"Exception: {exception.ToString()}");
        }

        public void Error(string error)
        {
            NUnit.Framework.TestContext.WriteLine($"Error: {error}");
        }

        public void Info(string message)
        {
            NUnit.Framework.TestContext.WriteLine($"Message: {message}");
        }
    }
}
