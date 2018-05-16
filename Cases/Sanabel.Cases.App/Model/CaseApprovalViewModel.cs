
using Sanabel.Cases.App.Resources;
using System;

using System.ComponentModel.DataAnnotations;


namespace Sanabel.Cases.App.Model
{
    class CaseApprovalViewModel
    {
        public Guid Id { get; set; }

        public Guid CaseId { get; set; }

        public Guid nApprovedBy { get; set; }

        public Guid nRejectedBy { get; set; }

        [Display(Name = "Approved", ResourceType = typeof(CasesResource))]
        public bool bApproved { get; set; }

        [Display(Name = "ApprovalDate", ResourceType = typeof(CasesResource))]
        public DateTime ApprovalDate { get; set; }

        [Display(Name = "Rejected", ResourceType = typeof(CasesResource))]
        public bool bRejected { get; set; }

        [Display(Name = "RejectionDate", ResourceType = typeof(CasesResource))]
        public DateTime RejectionDate { get; set; }

        [Display(Name = "Comments", ResourceType = typeof(CasesResource))]
        public ResearchTypes sComments { get; set; }

        public Guid nSuspendedBy { get; set; }

        [Display(Name = "Suspend", ResourceType = typeof(CasesResource))]
        public bool bSuspended { get; set; }

        [Display(Name = "SuspensionDate", ResourceType = typeof(CasesResource))]
        public DateTime dtSuspensionDate { get; set; }
    }
}

