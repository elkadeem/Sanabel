using BusinessSolutions.Common.Core;
using Sanabel.Volunteers.Domain.Model;
using System;
using System.Threading.Tasks;

namespace Sanabel.Volunteers.Domain.Repositories
{
    public interface IVolunteerRepository
    {
        Task<Volunteer> GetVolunteerById(Guid id);

        Task<PagedEntity<Volunteer>> SearchVolunteer(string name, string email, int countryId, int regionId
            , int cityId, int districtId, Genders? gender);

        Task AddVolunteer(Volunteer volunteer);

        Task UpdateVolunteer(Volunteer volunteer);

        Task RemoveVolunteer(Volunteer volunteer);
    }
}
