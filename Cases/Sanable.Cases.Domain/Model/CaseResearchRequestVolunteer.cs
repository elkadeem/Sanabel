using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Model
{
    public class CaseResearchRequestVolunteer
    {
        public Guid VolunteerId { get; set; }

        public Guid CaseResearchRequestId { get; set; }

        public string Comments { get; set; }
    }
}
