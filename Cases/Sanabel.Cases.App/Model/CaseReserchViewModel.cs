using Sanabel.Cases.App.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Cases.App.Model
{
    public class CaseReserchViewModel
    {
        public int Id { get; set; }

        [Display(Name = "RequestNumber", ResourceType = typeof(CasesResource))]
        public string RequestNumber { get; set; }

        [Display(Name = "RequestDate", ResourceType = typeof(CasesResource))]
        public DateTime RequestDate { get; set; }


        [Display(Name = "Case", ResourceType = typeof(CasesResource))]
        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        public int CaseId { get; set; }

        [Display(Name = "Description", ResourceType = typeof(CasesResource))]
        [Required(ErrorMessageResourceName = "RequiredFieldErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        [StringLength(500, MinimumLength = 5, ErrorMessageResourceName = "StringLengthErrorMessage", ErrorMessageResourceType = typeof(BusinessSolutions.Localization.CommonResources))]
        public string Description { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(CasesResource))]
        public string Notes { get; set; }

        [Display(Name = "Volunteers", ResourceType = typeof(CasesResource))]
        public List<CaseReserchVolunteerViewModel> Volunteers { get; set; }

        public CaseViewModel Case { get; set; }
    }
}
