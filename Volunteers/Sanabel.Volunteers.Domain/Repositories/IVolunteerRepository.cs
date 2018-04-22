using BusinessSolutions.Common.Core;
using Sanabel.Volunteers.Domain.Model;
using System;
using System.Threading.Tasks;

namespace Sanabel.Volunteers.Domain.Repositories
{
    public interface IVolunteerRepository
    {
        Task<Volunteer> GetVolunteerById(Guid id);

        Task<PagedEntity<Volunteer>> SearchVolunteer(string name, string email, string phone
            , int countryId, int regionId
            , int cityId, int districtId, Genders? gender
            , int pageIndex, int pageSize);

        Task AddVolunteer(Volunteer volunteer);

        Task UpdateVolunteer(Volunteer volunteer);

        Task RemoveVolunteer(Volunteer volunteer);

        Volunteer GetVolunteerByPhone(string phone);

        Volunteer GetVolunteerByEmail(string email);
    }
}
