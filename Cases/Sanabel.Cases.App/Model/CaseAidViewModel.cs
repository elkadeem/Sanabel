using BusinessSolutions.Localization;
using Sanabel.Cases.App.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Cases.App.Model
{
    public class CaseAidViewModel
    {
        public Guid AidId { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "AidDate", ResourceType = typeof(CasesResource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AidDate { get; set; }        
        
        public Guid CaseId { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage"
            , ErrorMessageResourceType = typeof(CommonResources))]        
        [Display(Name = "AidType", ResourceType = typeof(CasesResource))]
        public AidTypes AidType { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(500, ErrorMessageResourceName = "StringLengthErrorMessage"
            , ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "Description", ResourceType = typeof(CasesResource))]
        public string Description { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(500, ErrorMessageResourceName = "StringLengthErrorMessage"
            , ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "Notes", ResourceType = typeof(CasesResource))]
        public string Notes { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Range(0, 50000, ErrorMessageResourceName = "RangeErrorMessage"
            , ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [Display(Name = "Amount", ResourceType = typeof(CasesResource))]
        public double Amount { get; set; }
    }
}
