using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Infra.Validation
{
    public class EntityError
    {
        private readonly string _property;
        private readonly string _message;
        private readonly ValidationErrorTypes _validationErrorType;

        public string Property => _property;

        public string Message => _message;

        public ValidationErrorTypes ValidationErrorType => _validationErrorType;

        public EntityError(string message, ValidationErrorTypes validationErrorType)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));
           
            _message = message;
            _validationErrorType = validationErrorType;
        }

        public EntityError(string property, string message, ValidationErrorTypes validationErrorType)
        {
            if (string.IsNullOrEmpty(property))
                throw new ArgumentNullException(nameof(property));

            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            _property = property;
            _message = message;
            _validationErrorType = validationErrorType;
        }
    }
}
