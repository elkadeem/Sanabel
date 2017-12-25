using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Infra.Validation
{
    public class ValidationError
    {
        private string _property;
        private string _message;

        public string Property => _property;

        public string Message => _message;

        public ValidationError(string property, string message)
        {
            if (string.IsNullOrEmpty(property))
                throw new ArgumentNullException("property");

            if(string.IsNullOrEmpty(message))
                throw new ArgumentNullException("message");

            _property = property;
            _message = message;
        }
    }
}
