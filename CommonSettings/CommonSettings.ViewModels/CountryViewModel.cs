using BusinessSolutions.MVCCommon;
using BusinessSolutions.MVCCommon.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.ViewModels
{
    public class CountryViewModel 
    {
        public int CountryId { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType =typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(50, MinimumLength =3, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [ArabicTextOnly(ErrorMessageResourceName = "ArabicTextOnlyErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(ResourceType =typeof(CommonSettings.Localization.CommonSettingsResources), Name = "CountryName")]
        public string CountryName { get; set; }

        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "CountryNameEn")]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [EnglishTextOnly(ErrorMessageResourceName = "EnglishTextOnlyErrorMessage", ErrorMessageResourceType =typeof(BusinessSolutions.Localization.CommonResources))]
        public string CountryNameEn { get; set; }

        
        [StringLength(5, MinimumLength = 2, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "CountryCode")]
        public string CountryCode { get; set; }
        
    }
}
