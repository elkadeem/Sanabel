using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.MVCCommon
{
    public class BaseModel
    {
        private Dictionary<string, List<string>> _validationErrors;

        public ReadOnlyDictionary<string, ReadOnlyCollection<string>> ValidationErrors
        {
            get
            {
                return new ReadOnlyDictionary<string, ReadOnlyCollection<string>>(_validationErrors.ToDictionary(k => k.Key
                , v => v.Value.AsReadOnly()));
            }
        }

        public BaseModel()
        {
            _validationErrors = new Dictionary<string, List<string>>();
        }

        public bool HasErrors
        {
            get
            {
                return _validationErrors.Any(c => c.Value != null && c.Value.Count > 0);
            }
        }

        public void ClearValidation()
        {
            _validationErrors = new Dictionary<string, List<string>>();
        }

        public void AddPropertyError(Expression<Func<object>> propertyExpression, string message)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException("propertyExpression");

            var memberexpression = (propertyExpression.Body as MemberExpression);
            string propertyName = memberexpression?.Member.Name;
            AddPropertyError(propertyName, message);
        }

        protected void AddPropertyError(string propertyName, string message)
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

        public void ClearProperyErrors(Expression<Func<BaseModel>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException("propertyExpression");

            var memberexpression = (propertyExpression.Body as MemberExpression);
            string propertyName = memberexpression?.Member.Name;
            ClearProperyErrors(propertyName);
        }

        protected void ClearProperyErrors(string propertyName)
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
