using BusinessSolutions.Common.Infra.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core.Validation
{
    public sealed class ValidationError : IEquatable<ValidationError>
    {
        public ValidationError(string propertyName, string message)
        {
            Guard.StringIsNull<ArgumentNullException>(propertyName, nameof(propertyName));
            Guard.StringIsNull<ArgumentNullException>(message, nameof(message));

            PropertyName = propertyName;
            Message = message;
        }

        public string PropertyName { get; private set; }

        public string Message { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null && obj.GetType() != typeof(ValidationError))
                return false;

            return Equals((ValidationError)obj);
        }

        public bool Equals(ValidationError other)
        {
            if (other == null)
                return false;

            return this.Message.Equals(other.Message) 
                && this.PropertyName.Equals(other.PropertyName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Message.GetHashCode() * 256) ^ (PropertyName.GetHashCode() * 128);
            }
        }
    }
}
