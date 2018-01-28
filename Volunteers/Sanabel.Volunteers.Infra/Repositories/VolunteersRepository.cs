using BusinessSolutions.Common.Core;
using Sanabel.Volunteers.Domain.Model;
using Sanabel.Volunteers.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Volunteers.Infra.Repositories
{
    public class VolunteersRepository : IVolunteerRepository
    {
        public Task AddVolunteer(Volunteer volunteer)
        {
            throw new NotImplementedException();
        }

        public Task<Volunteer> GetVolunteerById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveVolunteer(Volunteer volunteer)
        {
            throw new NotImplementedException();
        }

        public Task<PagedEntity<Volunteer>> SearchVolunteer(string name, string email, int countryId
            , int regionId, int cityId, int districtId, Genders? gender)
        {
            throw new NotImplementedException();
        }

        public Task UpdateVolunteer(Volunteer volunteer)
        {
            throw new NotImplementedException();
        }
    }
}
