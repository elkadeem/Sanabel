using BusinessSolutions.Common.Core.Entities;
using CommonSettings.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Model
{
    public class Case : Entity<Guid>
    {
        
        public Case()
        {
            Id = Guid.NewGuid();
            CaseResearchs = new HashSet<CaseResearch>();
            CaseActions = new HashSet<CaseAction>();
        }

        public string Name { get; set; }

        public int CityId { get; set; }

        public int? DistrictId { get; set; }

        public string Phone { get; set; }

        public Genders Gender { get; set; }

        public CaseTypes CaseType { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public virtual  ICollection<CaseResearch> CaseResearchs { get; private set; }

        public City City { get; set; }

        public District District { get; set; }

        public CaseStatus CaseStatus { get; set; }

        public virtual ICollection<CaseAction> CaseActions { get; set; }

    }
}
