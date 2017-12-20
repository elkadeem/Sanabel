using System.Collections.Generic;
using BusinessSolutions.Common.EntityFramework;
using CommonSettings.Domain.Repositories;
using CommonSettings.Domain.Entities;
using BusinessSolutions.Common.Core;

namespace CommonSettings.DAL
{
    public class CityRepository : BaseEntityFrameworkRepository<int, Place, City>, ICityRepository
    {
        public CityRepository(CommonSettingDataContext dataContext) : base(dataContext)
        {
        }

        public PagedEntity<City> GetCities(int countryId, int regionId, string cityName, string code
            , int pageIndex, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public List<City> GetCitiesByCountryId(int CountryId)
        {
            throw new System.NotImplementedException();
        }

        public List<City> GetCitiesByRegionId(int regionId)
        {
            throw new System.NotImplementedException();
        }

        public override City GetDomainEntity(Place entity)
        {
            if (entity == null)
                return null;

            return new City
            {
                Code = entity.Code,
                Id = entity.Id,
                Name = entity.Name,
                NameEn = entity.NameEn,
                RegionId = entity.ParentPlaceId ?? 0,
            };
        }

        public override Place GetEntity(City domainEntity)
        {
            if (domainEntity == null)
                return null;

            return null;
        }
    }
}