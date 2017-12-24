using System.Collections.Generic;
using BusinessSolutions.Common.EntityFramework;
using CommonSettings.Domain.Repositories;
using CommonSettings.Domain.Entities;
using BusinessSolutions.Common.Core;
using System.Linq;
using System.Data.Entity;
using Grace.DependencyInjection.Attributes;

namespace CommonSettings.DAL
{
    [ExportByInterfaces()]
    public class CityRepository : BaseEntityFrameworkRepository<int, Place, City>, ICityRepository
    {
        public CityRepository(CommonSettingDataContext dataContext) : base(dataContext)
        {
        }

        public PagedEntity<City> GetCities(int countryId, int regionId, string cityName
            , string code
            , int pageIndex, int pageSize)
        {
            var query = Set.AsQueryable();
            if (countryId > 0)
                query = query.Where(c => c.CountryId == countryId);
            if (regionId > 0)
                query = query.Where(c => c.ParentPlaceId == regionId);
            if (string.IsNullOrEmpty(cityName))
                query = query.Where(c => c.Name.Contains(cityName)
                 || c.NameEn.Contains(cityName));
            if (string.IsNullOrEmpty(code))
                query = query.Where(c => c.Code.Contains(code));

            int totalCount = query.Count();
            var items = query.OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize).Select(c => GetDomainEntity(c))
                .ToList();

            return new PagedEntity<City>(items, totalCount);
        }

        public List<City> GetCitiesByCountryId(int CountryId)
        {
            return Set.Include(c => c.ParentPlace).Where(c => c.CountryId == CountryId)
                .Select(c => GetDomainEntity(c))
                .ToList();
        }

        public List<City> GetCitiesByRegionId(int regionId)
        {
            return Set.Include(c => c.ParentPlace).Where(c => c.ParentPlaceId == regionId)
                .Select(c => GetDomainEntity(c))
                .ToList();
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

            var currentCity = Set.Local.FirstOrDefault(c => c.Id == domainEntity.Id);
            if (currentCity == null)
                currentCity = new Place();
            currentCity.Name = domainEntity.Name;
            currentCity.NameEn = domainEntity.Name;
            currentCity.ParentPlaceId = domainEntity.RegionId;
            currentCity.CountryId = Set.FirstOrDefault(c => c.Id == domainEntity.RegionId).CountryId;
            currentCity.PlaceTypeId = 2;

            return currentCity;
        }
    }
}