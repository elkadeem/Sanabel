using BusinessSolutions.MVCCommon.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.ViewModels
{
    public class SearchCountryViewModel : BaseSearchViewModel<CountryViewModel>
    {
        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "CountryName")]
        public string CountryName { get; set; }

        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "CountryCode")]
        public string CountryCode { get; set; }

    }
}
