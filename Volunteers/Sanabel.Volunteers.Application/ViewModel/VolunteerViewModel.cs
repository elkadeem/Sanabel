using BusinessSolutions.Common.Infra.Attributes;
using BusinessSolutions.MVCCommon.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Volunteers.Application.Models
{
    public class VolunteerViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(100, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "Name", ResourceType = typeof(Localization.VolunteerResource))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(100, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Localization.VolunteerResource))]
        [Display(Name = "Email", ResourceType =typeof(Localization.VolunteerResource))]        
        public string Email { get; set; }

        [StringLength(200, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "Address", ResourceType = typeof(Localization.VolunteerResource))]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(15, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "Phone", ResourceType = typeof(Localization.VolunteerResource))]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "City", ResourceType = typeof(Localization.VolunteerResource))]
        public int CityId { get; set; }

        [Display(Name = "District", ResourceType = typeof(Localization.VolunteerResource))]
        public int? DistrictId { get; set; }
        
        [Display(Name = "Country", ResourceType = typeof(Localization.VolunteerResource))]
        public int? CountryId { get; set; }

        [Display(Name = "Region", ResourceType = typeof(Localization.VolunteerResource))]
        public int? RegionId { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Localization.VolunteerResource))]
        public Genders Gender { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "HasCar", ResourceType = typeof(Localization.VolunteerResource))]
        public bool HasCar { get;  set; }

        [Display(Name = "Notes", ResourceType = typeof(Localization.VolunteerResource))]
        public string Notes { get;  set; }
    }

    public enum Genders : byte
    {
        [LocalizedDescription("Male", typeof(Localization.VolunteerResource))]
        Male = 1,
        [LocalizedDescription("Male", typeof(Localization.VolunteerResource))]
        Female = 2,
    }
}
