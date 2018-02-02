using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Volunteers.Application.Models
{
    public class ViewVolunteerViewModel
    {
        public Guid Id { get; set; }
        
        [Display(Name = "Name", ResourceType = typeof(Localization.VolunteerResource))]
        public string VolunteerName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Localization.VolunteerResource))]
        public string VolunteerEmail { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Localization.VolunteerResource))]
        public string CountryName { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(Localization.VolunteerResource))]
        public string Phone { get; set; }

        [Display(Name = "Region", ResourceType = typeof(Localization.VolunteerResource))]
        public string RegionName { get; set; }

        [Display(Name = "City", ResourceType = typeof(Localization.VolunteerResource))]
        public string CityName { get; set; }

        [Display(Name = "District", ResourceType = typeof(Localization.VolunteerResource))]
        public string DistrictName { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Localization.VolunteerResource))]
        public string Address { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Localization.VolunteerResource))]
        public Genders Gender{ get; set; }
    }
}
