using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.EntityFramework;
using CommonSettings.Domain.Entities;
using CommonSettings.Domain.Repositories;
using Grace.DependencyInjection.Attributes;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CommonSettings.DAL
{
    [ExportByInterfaces()]
    public class CityRepository : BaseEntityFrameworkRepository<int, City>, ICityRepository
    {
        public CityRepository(CommonSettingDataContext dataContext) : base(dataContext)
        {
            
        }

        public override City GetByID(int key)
        {
            return Set.Include(c => c.Region.Country).FirstOrDefault(c => c.Id == key);
        }

        public PagedEntity<City> GetCities(int countryId, int regionId, string cityName
            , string code
            , int pageIndex, int pageSize)
        {
            var query = Set.Include(c => c.Region.Country);
            if (countryId > 0)
                query = query.Where(c => c.Region.CountryId == countryId);
            if (regionId > 0)
                query = query.Where(c => c.RegionId == regionId);
            if (!string.IsNullOrEmpty(cityName))
                query = query.Where(c => c.Name.Contains(cityName)
                 || c.NameEn.Contains(cityName));
            if (!string.IsNullOrEmpty(code))
                query = query.Where(c => c.Code.Contains(code));

            int totalCount = query.Count();
            var items = query.OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToList();

            return new PagedEntity<City>(items, totalCount);
        }

        public List<City> GetCitiesByCountryId(int CountryId)
        {
            return Set.Include(c => c.Region).Where(c => c.Region.CountryId == CountryId)
                .ToList();
        }

        public List<City> GetCitiesByRegionId(int regionId)
        {
            return Set.Include(c => c.Region).Where(c => c.RegionId == regionId)
                .ToList();
        }
    }
}