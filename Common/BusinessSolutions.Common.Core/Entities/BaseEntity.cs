using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core.Entities
{
    public class BaseEntity
    {
        private Dictionary<string, List<string>> _validationErrors;

        public bool IsValid
        {
            get
            {
                return _validationErrors.Count > 0;
            }
        }



        public BaseEntity()
        {
            _validationErrors = new Dictionary<string, List<string>>();
        }

        public void ClearValidation()
        {
            _validationErrors = new Dictionary<string, List<string>>();
        }

        public void AddPropertyError(Expression<Func<BaseEntity>> propertyExpression, string message)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException("propertyExpression");

            string propertyName = (propertyExpression.Body as MemberExpression).Member.Name;
            AddPropertyError(propertyName, message);
        }

        public void AddPropertyError(string propertyName, string message)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("propertyName");

            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("message");

            List<string> errorMessages;
            if (_validationErrors.ContainsKey(propertyName))
            {
                errorMessages = _validationErrors[propertyName];
            }
            else
                errorMessages = new List<string>();

            errorMessages.Add(message);
        }

        public void ClearProperyErrors(Expression<Func<BaseEntity>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException("propertyExpression");

            string propertyName = (propertyExpression.Body as MemberExpression).Member.Name;
            ClearProperyErrors(propertyName);
        }

        public void ClearProperyErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("propertyName");

            if (_validationErrors.ContainsKey(propertyName))
            {
                _validationErrors.Remove(propertyName);
            }

        }
    }
}
