using BusinessSolutions.Common.Infra.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Model
{
    public class FinancialAid : Aid
    {
        public FinancialAid()
        {
        }

        public FinancialAid(Case approvedCase, DateTime aidDate, double amount, string aidDescription)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(approvedCase, nameof(approvedCase));
            Guard.LessThanOrEqualZero(amount, nameof(amount));

            this.ApprovedCase = approvedCase;
            this.AidDate = AidDate;
            this.AidDescription = AidDescription;
            this.AidAmount = amount;
            this.CaseId = approvedCase.Id;
        }

        public double AidAmount { get; private set; }
    }
}
