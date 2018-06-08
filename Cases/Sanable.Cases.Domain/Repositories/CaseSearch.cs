using Sanable.Cases.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Repositories
{
    public class CaseSearch
    {
        public string CaseName { get; set; }

        public string Phone { get; set; }

        public CaseTypes? CaseType { get; set; }

        public int CountryId { get; set; }

        public int RegionId { get; set; }

        public int CityId { get; set; }

        public int DistrictId { get; set; }

        public CaseStatus? CaseStatus { get; set; }

    }
}
