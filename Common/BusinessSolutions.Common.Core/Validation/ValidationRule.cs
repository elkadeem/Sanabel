using System;
using BusinessSolutions.Common.Core.Specifications;
using BusinessSolutions.Common.Infra.Validation;

namespace BusinessSolutions.Common.Core.Validation
{
    public class ValidationRule<Entity> : IValidationRule<Entity> where Entity : class
    {
        private readonly string _validationProperty;
        private readonly string _validationMessage;
        private readonly ISpecification<Entity> _specification;

        public ValidationRule(ISpecification<Entity> specification
            , string validationProperty
            , string validationMessage)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(specification, nameof(specification));
            Guard.StringIsNull<ArgumentNullException>(validationProperty, nameof(validationProperty));
            Guard.StringIsNull<ArgumentNullException>(validationMessage, nameof(validationMessage));

            _specification = specification;
            _validationProperty = validationProperty;
            _validationMessage = validationMessage;
        }

        public string ValidationProperty => _validationProperty;

        public string ValidationMessage => _validationMessage;

        public bool Validate(Entity entity)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(entity, nameof(entity));
            return _specification.IsSatisfiedBy(entity);
        }
    }
}