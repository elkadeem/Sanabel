using BusinessSolutions.MVCCommon.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sanabel.Security.Application
{
    public class EditUserViewModel
    {
        public Guid Id { get; set; }
         
        public string Email { get; set; }
        
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

        [Display(Name = "IsLockOut", ResourceType = typeof(Localization.SecurityResource))]
        public bool IsLockOut { get;  set; }
    }
}
