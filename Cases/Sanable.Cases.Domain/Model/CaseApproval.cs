using BusinessSolutions.Common.Core.Entities;
using CommonSettings.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Model
{
    public class CaseApproval : Entity<Guid>
    {
        public CaseApproval()
        {
            Id = Guid.NewGuid();
        }

        public Guid CaseId { get; set; }
       // [SqlDefaultValue(DefaultValue = false)]
        public bool bApproved { get; set; }

        public Guid? nApprovedBy { get; set; }

        public DateTime dtApprovalDate { get; set; }

        public bool bRejected { get; set; }

        public Guid? nRejectedBy { get; set; }

        public DateTime dtRejectionDate { get; set; }

        public string Comments { get; set; }

        public bool bSuspended { get; set; }

        public Guid? nSuspendedBy { get; set; }

        public DateTime dtSuspensionDate { get; set; }

        public Case CaseTable { get; set; }
        

    }
}
