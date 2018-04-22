using BusinessSolutions.Common.Core.Entities;
using BusinessSolutions.Common.Infra.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Model
{
    public class CaseResearchRequest : Entity<Guid>
    {
        private CaseResearchRequest()
        {

        }

        public CaseResearchRequest(DateTime requestDate, Case caseToSearch, List<CaseResearchRequestVolunteer> volunteers)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(caseToSearch, nameof(caseToSearch));
            Guard.CollectioNullOrEmpty<CaseResearchRequestVolunteer>(volunteers, nameof(volunteers));

            CaseToSearch = caseToSearch;
            Volunteers = volunteers;
            RequestDate = requestDate; 
        }

        public DateTime RequestDate { get; private set; }

        public ICollection<CaseResearchRequestVolunteer> Volunteers { get; private set; }

        public Case CaseToSearch { get; private set; }

    }
}
