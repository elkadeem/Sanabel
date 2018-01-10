using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections;

namespace BusinessSolutions.MVCCommon.Attributes
{
    public class CollectionLengthValidationAttribute : ValidationAttribute, IClientValidatable
    {
        private int _minimumLength;
        public CollectionLengthValidationAttribute()
        {
            _minimumLength = 1;
        }

        public int MaximumLength { get; set; }

        public int MinimumLength => _minimumLength;

        public CollectionLengthValidationAttribute(int minimumLength)
        {
            _minimumLength = minimumLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var errorMessage = "";
            if (value != null && value is IEnumerable)
            {
                bool isValid = true;
                var items = ((IEnumerable)value).Cast<object>();
                //Check For Minimum Items
                if (MinimumLength > 0)
                {
                    if (items.Count() < MinimumLength)
                        isValid = false;
                }

                //Check for maximum Number of items
                if (MaximumLength > 0)
                {
                    if (items.Count() > MaximumLength)
                        isValid = false;
                }

                if (isValid)
                    return ValidationResult.Success;
            }

            errorMessage = FormatErrorMessage(validationContext.DisplayName);
            return new ValidationResult(errorMessage);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata
            , ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.DisplayName);
            rule.ValidationType = "collectionlengthvalidation";
            rule.ValidationParameters.Add("minimumlength", MinimumLength.ToString());
            rule.ValidationParameters.Add("maximumlength", MaximumLength.ToString());
            yield return rule;
        }
    }
}
