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
    public class RegionViewModel 
    {
        public int RegionId { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType =typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(50, MinimumLength =3, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [ArabicTextOnly(ErrorMessageResourceName = "ArabicTextOnlyErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(ResourceType =typeof(CommonSettings.Localization.CommonSettingsResources), Name = "RegionName")]
        public string RegionName { get; set; }

        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "RegionNameEn")]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [EnglishTextOnly(ErrorMessageResourceName = "EnglishTextOnlyErrorMessage", ErrorMessageResourceType =typeof(BusinessSolutions.Localization.CommonResources))]
        public string RegionNameEn { get; set; }

        
        [StringLength(5, MinimumLength = 2, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "RegionCode")]
        public string RegionCode { get; set; }

        
        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "Country")]
        public string CountryId { get; set; }

    }
}
