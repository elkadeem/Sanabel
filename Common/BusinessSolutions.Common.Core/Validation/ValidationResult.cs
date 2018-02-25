using BusinessSolutions.Common.Infra.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessSolutions.Common.Core.Validation
{
    public class ValidationResult
    {
        private readonly List<ValidationError> _validationErrors;
        public ValidationResult()
        {
            _validationErrors = new List<ValidationError>();
        }

        public bool IsValid => _validationErrors.Count == 0;

        public IEnumerable<ValidationError> ValidationErrors
        {
            get
            {
                foreach (var validationError in _validationErrors)
                    yield return validationError;
            }
        }

        public void Add(ValidationError validationError)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(validationError, nameof(validationError));
            _validationErrors.Add(validationError);
        }

        public void Remove(ValidationError validationError)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(validationError, nameof(validationError));
            if (_validationErrors.Contains(validationError))
                _validationErrors.Remove(validationError);
        }
    }
}