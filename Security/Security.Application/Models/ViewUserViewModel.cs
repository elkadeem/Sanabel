using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Application.Models
{
    public class ViewUserViewModel
    {
        public Guid UserId { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(Localization.SecurityResource))]
        public string UserName { get; set; }

        [Display(Name = "FullName", ResourceType = typeof(Localization.SecurityResource))]
        public string FullName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Localization.SecurityResource))]
        public string Email { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Localization.SecurityResource))]
        public string CountryName { get; set; }

        [Display(Name = "Region", ResourceType = typeof(Localization.SecurityResource))]
        public string RegionName { get; set; }

        [Display(Name = "City", ResourceType = typeof(Localization.SecurityResource))]
        public string CityName { get; set; }

        [Display(Name = "District", ResourceType = typeof(Localization.SecurityResource))]
        public string DistrictName { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Localization.SecurityResource))]
        public string Address { get; set; }

        public bool IsLockOut { get; internal set; }
    }
}
