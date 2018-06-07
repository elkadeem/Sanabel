using BusinessSolutions.Common.Core.Entities;
using CommonSettings.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Model
{
    public class CaseAction : Entity<Guid>
    {
        public CaseAction()
        {
            Id = Guid.NewGuid();
        }

        public DateTime CaseActionDate { get; set; }

        public CaseStatus OldStatus { get; set; }

        public CaseStatus Status { get; set; }

        public string Comment { get; set; }

        public string CreatedBy { get; set; }

        public Guid CaseId { get; set; }

    }
}
