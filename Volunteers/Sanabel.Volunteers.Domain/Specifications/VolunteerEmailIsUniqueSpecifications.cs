using BusinessSolutions.Common.Core.Specifications;
using Sanabel.Volunteers.Domain.Model;
using Sanabel.Volunteers.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Volunteers.Domain.Specifications
{
    public class VolunteerEmailIsUniqueSpecifications : ISpecification<Volunteer>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        public VolunteerEmailIsUniqueSpecifications(IVolunteerRepository volunteerRepository)
        {
            this._volunteerRepository = volunteerRepository;
        }

        public bool IsSatisfiedBy(Volunteer entity)
        {
            var volunteer = _volunteerRepository.GetVolunteerByEmail(entity.Email);
            return volunteer == null || volunteer.Id == entity.Id;
        }
    }
}
