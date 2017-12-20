using BusinessSolutions.Common.EntityFramework;
using CommonSettings.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSettings.Domain.Entities;
using BusinessSolutions.Common.Core;

namespace CommonSettings.DAL
{
    public class CountryRepository : BaseEntityFrameworkRepository<int, Country, Domain.Entities.Country>, ICountryRepository
    {
        public CountryRepository(CommonSettingDataContext dataContext) : base(dataContext)
        {
        }

        public PagedEntity<Domain.Entities.Country> GetCountries(string countryName, string code, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public override Domain.Entities.Country GetDomainEntity(Country entity)
        {
            if (entity == null)
                return null;

            return new Domain.Entities.Country
            {
                Code = entity.Code,
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
            };
        }

        public override Country GetEntity(Domain.Entities.Country domainEntity)
        {
            if (domainEntity == null)
                return null;

            var currentCountry = Set.Local.FirstOrDefault(c => c.Id == domainEntity.Id);
            if (currentCountry == null)
                return null;

            currentCountry.Code = domainEntity.Code;
            currentCountry.Name = domainEntity.Name;
            currentCountry.NameEn = domainEntity.NameEn;

            return currentCountry;
        }
    }
}
