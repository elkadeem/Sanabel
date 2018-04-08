using BusinessSolutions.Common.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Model
{
    public class CaseResearch : Entity<Guid>
    {
        public CaseResearch()
        {
            Id = Guid.NewGuid();
            CaseFollowers = new HashSet<CaseFollower>();
        }

        public Guid CaseId { get; set; }

        public Guid CaseResearchRequest { get; set; }

        public DateTime ResearchDate { get; set; }

        public ResearchTypes ResearchType { get; set; }

        public string OtherResearchType { get; set; }

        public string JobName { get; set; }

        public string QualificationName { get; set; }

        public string Address { get; set; }

        public int? EgyptRegionId { get; set; }

        public string MembershipNo { get; set; }

        public DateTime? MembershipDate { get; set; }

        public bool IsMemberInOtherAssociation { get; set; }

        public string OtherAssociationName { get; set; }

        public JobTypes JobType { get; set; }

        public string LeaveWorkReason { get; set; }

        public double Income { get; set; }

        public string NoIncomeReason { get; set; }

        public int NumberOfFollowers { get; set; }

        public bool HasClosedRelatives { get; set; }

        public string ClosedRelativeName { get; set; }

        public string ClosedRelativePhone { get; set; }

        public double YearlyHouseRent { get; set; }

        public bool IsHouseRentPaid { get; set; }

        public byte TransportationType { get; set; }

        public string Description { get; set; }

        public string Skils { get; set; }

        public string NeededHelp { get; set; }

        public HealthStatuses HealthStatus { get; set; }

        public string HealthStatusDescription { get; set; }

        public string HealthNotes { get; set; }

        public Case Case { get; set; }

        public ICollection<CaseFollower> CaseFollowers { get; private set; }
    }
}
