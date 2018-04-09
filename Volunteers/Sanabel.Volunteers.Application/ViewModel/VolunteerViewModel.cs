using BusinessSolutions.Common.Infra.Attributes;
using BusinessSolutions.Localization;
using Sanabel.Volunteers.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sanabel.Volunteers.Application.Models
{
    public class VolunteerViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(CommonResources))]
        [StringLength(100, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(CommonResources))]
        [Display(Name = "Name", ResourceType = typeof(VolunteerResource))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(CommonResources))]
        [StringLength(100, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(CommonResources))]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(VolunteerResource))]
        [Display(Name = "Email", ResourceType =typeof(VolunteerResource))]        
        public string Email { get; set; }

        [StringLength(200, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(CommonResources))]
        [Display(Name = "Address", ResourceType = typeof(VolunteerResource))]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(CommonResources))]
        [StringLength(15, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(CommonResources))]
        [Display(Name = "Phone", ResourceType = typeof(VolunteerResource))]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(CommonResources))]
        [Display(Name = "City", ResourceType = typeof(VolunteerResource))]
        public int CityId { get; set; }

        [Display(Name = "District", ResourceType = typeof(VolunteerResource))]
        public int? DistrictId { get; set; }
        
        [Display(Name = "Country", ResourceType = typeof(VolunteerResource))]
        public int? CountryId { get; set; }

        [Display(Name = "Region", ResourceType = typeof(VolunteerResource))]
        public int? RegionId { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(VolunteerResource))]
        public Genders Gender { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(CommonResources))]
        [Display(Name = "HasCar", ResourceType = typeof(VolunteerResource))]
        public bool HasCar { get;  set; }

        [Display(Name = "Notes", ResourceType = typeof(VolunteerResource))]
        public string Notes { get;  set; }

        [Display(Name = "Country", ResourceType = typeof(VolunteerResource))]
        public string CountryName { get; set; }

        [Display(Name = "Region", ResourceType = typeof(VolunteerResource))]
        public string RegionName { get; set; }

        [Display(Name = "City", ResourceType = typeof(VolunteerResource))]
        public string CityName { get; set; }

        [Display(Name = "District", ResourceType = typeof(VolunteerResource))]
        public string DistrictName { get; set; }
    }

    public enum Genders : byte
    {
        [LocalizedDescription("Male", typeof(VolunteerResource))]
        Male = 1,
        [LocalizedDescription("Male", typeof(VolunteerResource))]
        Female = 2,
    }
}
