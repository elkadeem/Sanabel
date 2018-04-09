using Sanabel.Volunteers.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sanabel.Volunteers.Application.Models
{
    public class ViewVolunteerViewModel
    {
        public Guid Id { get; set; }
        
        [Display(Name = "Name", ResourceType = typeof(VolunteerResource))]
        public string VolunteerName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(VolunteerResource))]
        public string VolunteerEmail { get; set; }

        [Display(Name = "Country", ResourceType = typeof(VolunteerResource))]
        public string CountryName { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(VolunteerResource))]
        public string Phone { get; set; }

        [Display(Name = "Region", ResourceType = typeof(VolunteerResource))]
        public string RegionName { get; set; }

        [Display(Name = "City", ResourceType = typeof(VolunteerResource))]
        public string CityName { get; set; }

        [Display(Name = "District", ResourceType = typeof(VolunteerResource))]
        public string DistrictName { get; set; }

        [Display(Name = "Address", ResourceType = typeof(VolunteerResource))]
        public string Address { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(VolunteerResource))]
        public Genders Gender{ get; set; }
    }
}
