using BusinessSolutions.MVCCommon.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sanabel.Security.Application
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(100, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Localization.SecurityResource))]
        [Display(Name = "Email", ResourceType =typeof(Localization.SecurityResource))]        
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})", ErrorMessageResourceName ="InvalidPassword", ErrorMessageResourceType = typeof(Localization.SecurityResource))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Localization.SecurityResource))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Localization.SecurityResource))]
        [Compare("Password", ErrorMessageResourceName ="ConfirmPasswordInvalid", ErrorMessageResourceType = typeof(Localization.SecurityResource))]
        public string ConfirmPassword { get; set; }
        
        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(100, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "FullName", ResourceType = typeof(Localization.SecurityResource))]
        public string FullName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(15, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "Phone", ResourceType = typeof(Localization.SecurityResource))]
        public string Phone { get; set; }        

        [CollectionLengthValidationAttribute(1, ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "Roles", ResourceType = typeof(Localization.SecurityResource))]
        public List<Guid> Roles { get; set; }        
    }
}
