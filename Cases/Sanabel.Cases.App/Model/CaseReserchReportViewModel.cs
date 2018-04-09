using Sanabel.Cases.App.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Cases.App.Model
{
    public class CaseReserchReportViewModel
    {
        public Guid CaseId { get; set; }

        [Display(Name = "CaseResearchRequest", ResourceType = typeof(CasesResource))]
        public Guid CaseResearchRequestId { get; set; }

        [Display(Name = "ResearchDate", ResourceType = typeof(CasesResource))]
        public DateTime ResearchDate { get; set; }

        [Display(Name = "ResearchType", ResourceType = typeof(CasesResource))]
        public ResearchTypes ResearchType { get; set; }

        [Display(Name = "OtherResearchType", ResourceType = typeof(CasesResource))]
        public string OtherResearchType { get; set; }

        [Display(Name = "CaseName", ResourceType = typeof(CasesResource))]
        public string JobName { get; set; }

        [Display(Name = "CaseName", ResourceType = typeof(CasesResource))]
        public string QualificationName { get; set; }

        [Display(Name = "Address", ResourceType = typeof(CasesResource))]
        public string Address { get; set; }

        [Display(Name = "EgyptRegion", ResourceType = typeof(CasesResource))]
        public int? EgyptRegionId { get; set; }

        [Display(Name = "MembershipNo", ResourceType = typeof(CasesResource))]
        public string MembershipNo { get; set; }

        [Display(Name = "MembershipDate", ResourceType = typeof(CasesResource))]
        public DateTime? MembershipDate { get; set; }

        [Display(Name = "IsMemberInOtherAssociation", ResourceType = typeof(CasesResource))]
        public bool IsMemberInOtherAssociation { get; set; }

        [Display(Name = "OtherAssociationName", ResourceType = typeof(CasesResource))]
        public string OtherAssociationName { get; set; }

        [Display(Name = "JobType", ResourceType = typeof(CasesResource))]
        public JobTypes JobType { get; set; }

        [Display(Name = "LeaveWorkReason", ResourceType = typeof(CasesResource))]
        public string LeaveWorkReason { get; set; }

        [Display(Name = "Income", ResourceType = typeof(CasesResource))]
        public double Income { get; set; }

        [Display(Name = "NoIncomeReason", ResourceType = typeof(CasesResource))]
        public string NoIncomeReason { get; set; }

        [Display(Name = "NumberOfFollowers", ResourceType = typeof(CasesResource))]
        public int NumberOfFollowers { get; set; }

        [Display(Name = "HasClosedRelatives", ResourceType = typeof(CasesResource))]
        public bool HasClosedRelatives { get; set; }

        [Display(Name = "ClosedRelativeName", ResourceType = typeof(CasesResource))]
        public string ClosedRelativeName { get; set; }

        [Display(Name = "ClosedRelativePhone", ResourceType = typeof(CasesResource))]
        public string ClosedRelativePhone { get; set; }

        [Display(Name = "YearlyHouseRent", ResourceType = typeof(CasesResource))]
        public double YearlyHouseRent { get; set; }

        [Display(Name = "IsHouseRentPaid", ResourceType = typeof(CasesResource))]
        public bool IsHouseRentPaid { get; set; }

        [Display(Name = "TransportationType", ResourceType = typeof(CasesResource))]
        public byte TransportationType { get; set; }

        [Display(Name = "Description", ResourceType = typeof(CasesResource))]
        public string Description { get; set; }

        [Display(Name = "Skils", ResourceType = typeof(CasesResource))]
        public string Skils { get; set; }

        [Display(Name = "NeededHelp", ResourceType = typeof(CasesResource))]
        public string NeededHelp { get; set; }

        [Display(Name = "HealthStatus", ResourceType = typeof(CasesResource))]
        public HealthStatuses HealthStatus { get; set; }

        [Display(Name = "CaseName", ResourceType = typeof(CasesResource))]
        public string HealthStatusDescription { get; set; }

        [Display(Name = "HealthNotes", ResourceType = typeof(CasesResource))]
        public string HealthNotes { get; set; }

        [Display(Name = "Case", ResourceType = typeof(CasesResource))]
        public CaseViewModel Case { get; set; }

        [Display(Name = "CaseFollowers", ResourceType = typeof(CasesResource))]
        public List<CaseFollowerViewModel> CaseFollowers { get; private set; }
    }
}
