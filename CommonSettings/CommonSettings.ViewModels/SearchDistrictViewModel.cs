using BusinessSolutions.MVCCommon.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.ViewModels
{
    public class SearchDistrictViewModel : BaseSearchViewModel<DistrictViewModel>
    {
        public SearchDistrictViewModel() : base()
        {

        }

        public SearchDistrictViewModel(int pageSize) : base(pageSize)
        {

        }

        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "DistrictName")]
        public string DistrictName { get; set; }
        
        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "DistrictCode")]
        public string DistrictCode { get; set; }
                
        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "City")]
        public int CityId { get; set; }
                
        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "Region")]
        public int RegionId { get; set; }
                
        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "Country")]
        public int CountryId { get; set; }
    }
}
