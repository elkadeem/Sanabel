
using Sanabel.Cases.App.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Sanabel.Cases.App.Model
{
    public class CaseAidsViewModel
    {
        [Display(Name = "Case", ResourceType = typeof(CasesResource))]
        public CaseViewModel Case { get; set; }

        [Display(Name = "Case", ResourceType = typeof(CasesResource))]
        public Guid CaseId { get; set; }

        [Display(Name = "Aids", ResourceType = typeof(CasesResource))]
        public List<CaseAidViewModel> CaseAids { get; set; }
    }
}

