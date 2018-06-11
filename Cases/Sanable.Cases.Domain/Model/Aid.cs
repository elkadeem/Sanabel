using BusinessSolutions.Common.Core.Entities;
using BusinessSolutions.Common.Infra.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Model
{
    public class Aid : Entity<Guid>
    {
        public Aid()
        {
        }

        public Guid CaseId { get; set; }

        public DateTime AidDate { get; set; }

        public string AidDescription { get; set; }

        public double AidAmount { get; set; }

        public AidTypes AidType { get; set; }

        public bool IsDelivered { get; set; }

        public string Notes { get; set; }

        public Case Case { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }





    }
}
