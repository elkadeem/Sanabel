using BusinessSolutions.Common.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Volunteers.Domain.Events
{
    public class VolunteerCreated : IDomainEvent
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int CityId { get; set; }

        public int? DistrictId { get; set; }
    }
}
