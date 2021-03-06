﻿using BusinessSolutions.MVCCommon.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.ViewModels
{
    public class SearchRegionViewModel : BaseSearchViewModel<RegionViewModel>
    {
        public SearchRegionViewModel(): base()
        {

        }

        public SearchRegionViewModel(int pageSize) : base(pageSize)
        {

        }

        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "RegionName")]
        public string RegionName { get; set; }

        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "RegionCode")]
        public string RegionCode { get; set; }

        [Display(ResourceType = typeof(CommonSettings.Localization.CommonSettingsResources), Name = "Country")]
        public int CountryId { get; set; }

    }
}
