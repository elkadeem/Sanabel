using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Infra.Validation
{
    public class EntityResult
    {
        private readonly List<EntityError> _errors = new List<EntityError>();
        private readonly bool _succeeded = false;

        public bool Succeeded => _succeeded;

        public IEnumerable<EntityError> ValidationErrors => _errors.AsReadOnly();

        public EntityResult(params EntityError[] errors)
        {
            if (errors == null || !errors.Any())
                throw new ArgumentNullException("errors");
            
            foreach (var error in errors)
            {
                _errors.Add(error);
            }
        }

        public EntityResult(IEnumerable<EntityError> errors)
        {
            if (errors == null || !errors.Any())
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

        public static EntityResult Failed(params EntityError[] errors)
        {
            return new EntityResult(errors);
        }
    }
}
