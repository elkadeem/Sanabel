using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Infra.Validation
{
    public class EntityResult
    {
        private List<ValidationError> _errors = new List<ValidationError>();
        private bool _succeeded = false;

        public bool Succeeded => _succeeded;

        public IEnumerable<ValidationError> ValidationErrors => _errors.AsReadOnly();

        public EntityResult(params ValidationError[] errors)
        {
            if (errors == null || errors.Count() == 0)
                throw new ArgumentNullException("errors");
            
            foreach (var error in errors)
            {
                _errors.Add(error);
            }
        }

        public EntityResult(IEnumerable<ValidationError> errors)
        {
            if (errors == null || errors.Count() == 0)
                throw new ArgumentNullException("errors");

            foreach (var error in errors)
            {
                _errors.Add(error);
            }
        }

        public EntityResult(bool success)
        {
            _succeeded = success;
        }

        public static EntityResult Success => new EntityResult(true);

        public static EntityResult Failed(params ValidationError[] errors)
        {
            return new EntityResult(errors);
        }
    }
}
