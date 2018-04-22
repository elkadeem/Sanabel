using BusinessSolutions.Common.Infra.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Model
{
    public class MedicalAid : Aid
    {
        public MedicalAid()
        {
        }

        public MedicalAid(Case approvedCase, DateTime aidDate
            , string aidDescription, string medicine)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(approvedCase, nameof(approvedCase));
            Guard.StringIsNull<ArgumentNullException>(aidDescription, nameof(aidDescription));            

            this.ApprovedCase = approvedCase;
            this.AidDate = AidDate;
            this.AidDescription = AidDescription;            
            this.CaseId = approvedCase.Id;
            this.Medicine = medicine;
        }

        public double AidAmount { get; private set; }

        public string Medicine { get; private set; }
    }
}
