using BusinessSolutions.MVCCommon.Attributes;
using Sanabel.Cases.App.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Cases.App.Model
{
    public class CaseViewModel
    {

        public Guid CaseId { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(100, MinimumLength = 5, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "CaseName", ResourceType = typeof(CasesResource))]
        public string CaseName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "City", ResourceType = typeof(CasesResource))]
        public int CityId { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "District", ResourceType = typeof(CasesResource))]
        public int? DistrictId { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(14, MinimumLength = 10, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [ArabicTextOnly(ErrorMessageResourceName = "ArabicTextOnlyErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "Phone", ResourceType = typeof(CasesResource))]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "Gender", ResourceType = typeof(CasesResource))]
        public Genders Gender { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage"
            , ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "CaseType", ResourceType = typeof(CasesResource))]
        public CaseTypes CaseType { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage"
            , ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(200, ErrorMessageResourceName = "StringLengthErrorMessage"
            , ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "Address", ResourceType = typeof(CasesResource))]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage"
            , ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(2000, ErrorMessageResourceName = "StringLengthErrorMessage"
            , ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "Description", ResourceType = typeof(CasesResource))]
        public string Description { get; set; }

        [Display(Name = "Country", ResourceType = typeof(CasesResource))]
        public string CountryName { get; set; }

        [Display(Name = "City", ResourceType = typeof(CasesResource))]
        public string CityName { get; set; }

        [Display(Name = "Region", ResourceType = typeof(CasesResource))]
        public string RegionName { get; set; }

        [Display(Name = "District", ResourceType = typeof(CasesResource))]
        public string DistrictName { get; set; }

        [Display(Name = "Country", ResourceType = typeof(CasesResource))]
        public int? CountryId { get; set; }

        [Display(Name = "Region", ResourceType = typeof(CasesResource))]
        public int? RegionId { get; set; }


        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "CaseStatus", ResourceType = typeof(CasesResource))]
        public CaseStatus CaseStatus { get; set; }

        [Display(Name = "Action", ResourceType = typeof(CasesResource))]
        public string Action { get; set; }

        [Display(Name = "Comment", ResourceType = typeof(CasesResource))]
        public string Comment { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [Display(Name = "CaseSuspensionDate", ResourceType = typeof(CasesResource))]
        public string CaseSuspensionDate { get; set; }
    }
}
