using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Cases.App.Model
{
    public class CaseFollowerViewModel
    {
        public Guid Id { get; set; }

        public Guid CaseResearchId { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Education { get; set; }

        public string JobName { get; set; }

        public double InCome { get; set; }

        public string Notes { get; set; }

        public HealthStatuses HealthStatus { get; set; }

        public string HealthStatusDescription { get; set; }

        public string HealthNotes { get; set; }
    }
}
