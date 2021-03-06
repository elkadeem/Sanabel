﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Security.Application.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword", ResourceType = typeof(Localization.SecurityResource))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})", ErrorMessageResourceName = "InvalidPassword", ErrorMessageResourceType = typeof(Localization.SecurityResource))]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Localization.SecurityResource))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Localization.SecurityResource))]
        [Compare("NewPassword", ErrorMessageResourceName = "ConfirmPasswordInvalid", ErrorMessageResourceType = typeof(Localization.SecurityResource))]
        public string ConfirmPassword { get; set; }
    }
}
