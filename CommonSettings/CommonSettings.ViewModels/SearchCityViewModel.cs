using BusinessSolutions.MVCCommon.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.ViewModels
{
    public class SearchCityViewModel : BaseSearchViewModel<CityViewModel>
    {
        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "CityName")]
        public string CityName { get; set; }

        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "CityCode")]
        public string CityCode { get; set; }

        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "Region")]
        public int RegionId { get; set; }

        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "Country")]
        public int CountryId { get; set; }
    }
}
