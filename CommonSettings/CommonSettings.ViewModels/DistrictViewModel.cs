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
    public class DistrictViewModel
    {        
        public int DistrictId { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType =typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(50, MinimumLength =3, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [ArabicTextOnly(ErrorMessageResourceName = "ArabicTextOnlyErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(ResourceType =typeof(CommonSettings.Localization.CommonSettingsResources), Name = "DistrictName")]
        public string DistricName { get; set; }

        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "DistrictNameEn")]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [EnglishTextOnly(ErrorMessageResourceName = "EnglishTextOnlyErrorMessage", ErrorMessageResourceType =typeof(BusinessSolutions.Localization.CommonResources))]
        public string DistricNameEn { get; set; }

        
        [StringLength(5, MinimumLength = 2, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "DistrictCode")]
        public string DistricCode { get; set; }       
        

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "City")]
        public int CityId { get; set; }

    }
}
