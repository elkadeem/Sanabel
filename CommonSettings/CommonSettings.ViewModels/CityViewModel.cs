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
    public class CityViewModel 
    {
        public int CityId { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType =typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(50, MinimumLength =3, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [ArabicTextOnly(ErrorMessageResourceName = "ArabicTextOnlyErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(ResourceType =typeof(CommonSettings.Localization.CommonSettingsResources), Name = "CityName")]
        public string CityName { get; set; }

        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "CityNameEn")]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [EnglishTextOnly(ErrorMessageResourceName = "EnglishTextOnlyErrorMessage", ErrorMessageResourceType =typeof(BusinessSolutions.Localization.CommonResources))]
        public string CityNameEn { get; set; }

        
        [StringLength(5, MinimumLength = 2, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "CityCode")]
        public string CityCode { get; set; }       
        

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "Region")]
        public int RegionId { get; set; }

    }
}
