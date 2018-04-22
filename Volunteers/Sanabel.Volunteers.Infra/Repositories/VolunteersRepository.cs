using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.EntityFramework;
using BusinessSolutions.Common.Infra.Validation;
using Sanabel.Volunteers.Domain.Model;
using Sanabel.Volunteers.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Sanabel.Volunteers.Infra.Repositories
{
    internal class VolunteersRepository : IVolunteerRepository
    {
        private readonly BaseEntityFrameworkRepository<Guid, Volunteer>  _repository;
        private readonly VolunteersDbCotext _dbContext;
        public VolunteersRepository(VolunteersDbCotext dbContext)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(dbContext, nameof(dbContext));
            _dbContext = dbContext;
            _repository = new BaseEntityFrameworkRepository<Guid, Volunteer>(dbContext);
        }


        public Task AddVolunteer(Volunteer volunteer)
        {
            _dbContext.Volunteers.Add(volunteer);
            return Task.FromResult(0);
        }

        public Volunteer GetVolunteerByEmail(string email)
        {
            return _dbContext.Volunteers.FirstOrDefault(c => c.Email == email);
        }

        public Task<Volunteer> GetVolunteerById(Guid id)
        {
            return _dbContext.Volunteers.Include(c => c.City.Region.Country)
                .Include(c => c.District).FirstOrDefaultAsync(c => c.Id == id);
        }

        public Volunteer GetVolunteerByPhone(string phone)
        {
            return _dbContext.Volunteers.FirstOrDefault(c => c.Phone == phone);
        }

        public Task RemoveVolunteer(Volunteer volunteer)
        {
            _dbContext.Volunteers.Remove(volunteer);
            return Task.FromResult(0);
        }

        public async Task<PagedEntity<Volunteer>> SearchVolunteer(string name, string email, string phone, int countryId
            , int regionId, int cityId, int districtId, Genders? gender
            , int pageIndex, int pageSize)
        {
            var query = _repository.Query
                .Include(c => c.District)
                .Include(c => c.City.Region.Country);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.Name.Contains(name));
            if (!string.IsNullOrEmpty(email))
                query = query.Where(c => c.Email.Contains(email));
            if (!string.IsNullOrEmpty(phone))
                query = query.Where(c => c.Phone.Contains(phone));
            if (countryId > 0)
                query = query.Where(c => c.City.Region.CountryId == countryId);
            if (regionId > 0)
                query = query.Where(c => c.City.RegionId == regionId);
            if (cityId > 0)
                query = query.Where(c => c.CityId == cityId);
            if (districtId > 0)
                query = query.Where(c => c.DistrictId == districtId);
            if (gender.HasValue)
                query = query.Where(c => c.Gender == gender);

            var count = await query.CountAsync();
            var result = await query.OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToListAsync();

            return new PagedEntity<Volunteer>(result
                , count);
        }

        public Task UpdateVolunteer(Volunteer volunteer)
        {
            _repository.Update(volunteer);
            return Task.FromResult(0);
        }
    }
}
