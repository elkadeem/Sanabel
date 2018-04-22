using BusinessSolutions.Common.Infra.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core.Validation
{
    public class EntityValidator<Entity> : IEntityValidator<Entity> where Entity : class
    {
        private readonly Dictionary<string, IValidationRule<Entity>> _validationRules;

        public EntityValidator()
        {
            _validationRules = new Dictionary<string, IValidationRule<Entity>>();
        }

        public void Add(string ruleName, IValidationRule<Entity> validationRule)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(ruleName, nameof(ruleName));
            Guard.ArgumentIsNull<ArgumentNullException>(validationRule, nameof(validationRule));
            if (_validationRules.Keys.Contains(ruleName))
                throw new ArgumentException("Rule name is already exist.", nameof(ruleName));

            _validationRules.Add(ruleName, validationRule);
        }

        public void Remove(string ruleName)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(ruleName, nameof(ruleName));
            _validationRules.Remove(ruleName);
        }

        public ValidationResult Validate(Entity entity)
        {
            ValidationResult validationResult = new ValidationResult();
            foreach(var rule in _validationRules.Values)
            {
                if (!rule.Validate(entity))
                    validationResult.Add(new ValidationError(rule.ValidationProperty
                        , rule.ValidationMessage));
            }

            return validationResult;
        }
    }
}
