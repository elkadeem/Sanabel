using BusinessSolutions.MVCCommon.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Application.Models
{
    public class SearchUsersViewModel : BaseSearchViewModel<ViewUserViewModel>
    {
        public SearchUsersViewModel() : base()
        {
        }

        public SearchUsersViewModel(int pageSize) : base(pageSize)
        {
        }

        [Display(Name = "UserName", ResourceType = typeof(Localization.SecurityResource))]
        public string UserName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Localization.SecurityResource))]
        public string Email { get; set; }

        [Display(Name = "FullName", ResourceType = typeof(Localization.SecurityResource))]
        public string FullName { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Localization.SecurityResource))]
        public int CountryId { get; set; }

        [Display(Name = "City", ResourceType = typeof(Localization.SecurityResource))]
        public int CityId { get; set; }

        [Display(Name = "Region", ResourceType = typeof(Localization.SecurityResource))]
        public int RegionId { get; set; }

        [Display(Name = "District", ResourceType = typeof(Localization.SecurityResource))]
        public int DistrictId { get; set; }


    }
}
