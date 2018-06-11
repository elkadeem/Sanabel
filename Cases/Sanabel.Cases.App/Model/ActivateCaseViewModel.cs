
using Sanabel.Cases.App.Resources;
using System;

using System.ComponentModel.DataAnnotations;


namespace Sanabel.Cases.App.Model
{
    public class ActivateCaseViewModel
    {
        [Display(Name = "Case", ResourceType = typeof(CasesResource))]
        public CaseViewModel Case { get; set; }

        [Display(Name = "Case", ResourceType = typeof(CasesResource))]
        public Guid CaseId { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(500, ErrorMessageResourceName = "StringLengthErrorMessage"
            , ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "Comment", ResourceType = typeof(CasesResource))]
        public string Comment { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "StartApplyDate", ResourceType = typeof(CasesResource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartApplyDate { get; set; }
    }
}

