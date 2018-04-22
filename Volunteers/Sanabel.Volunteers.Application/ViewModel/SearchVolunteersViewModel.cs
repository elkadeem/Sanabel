using BusinessSolutions.MVCCommon.Common;
using System.ComponentModel.DataAnnotations;
using Sanabel.Volunteers.Resources;

namespace Sanabel.Volunteers.Application.Models
{
    public class SearchVolunteersViewModel : BaseSearchViewModel<ViewVolunteerViewModel>
    {
        public SearchVolunteersViewModel() : base()
        {
        }

        public SearchVolunteersViewModel(int pageSize) : base(pageSize)
        {
        }
                
        [Display(Name = "Email", ResourceType = typeof(VolunteerResource))]
        public string VolunteerEmail { get; set; }

        [Display(Name = "Name", ResourceType = typeof(VolunteerResource))]
        public string VolunteerName { get; set; }

        [Display(Name = "Country", ResourceType = typeof(VolunteerResource))]
        public int CountryId { get; set; }

        [Display(Name = "City", ResourceType = typeof(VolunteerResource))]
        public int CityId { get; set; }

        [Display(Name = "Region", ResourceType = typeof(VolunteerResource))]
        public int RegionId { get; set; }

        [Display(Name = "District", ResourceType = typeof(VolunteerResource))]
        public int DistrictId { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(VolunteerResource))]
        public string Phone { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(VolunteerResource))]
        public Genders? Gender { get; set; }
    }
}
