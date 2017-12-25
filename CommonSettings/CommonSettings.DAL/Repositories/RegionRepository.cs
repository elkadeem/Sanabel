using BusinessSolutions.Common.EntityFramework;
using CommonSettings.Domain.Entities;
using CommonSettings.Domain.Repositories;
using System.Collections.Generic;
using BusinessSolutions.Common.Core;
using System.Linq;
using Grace.DependencyInjection.Attributes;

namespace CommonSettings.DAL
{
    [ExportByInterfaces()]
    public class RegionRepository : BaseEntityFrameworkRepository<int, Region>, IRegionRepository
    {
        public RegionRepository(CommonSettingDataContext dataContext) : base(dataContext)
        {
        }

        public PagedEntity<Region> GetRegions(int countryId, string regionName, string code, int pageIndex, int pageSize)
        {
            var query = Set.AsQueryable();
            if (countryId > 0)
                query = query.Where(c => c.CountryId == countryId);
            if (!string.IsNullOrEmpty(regionName))
                query = query.Where(c => c.Name.Contains(regionName) || c.NameEn.Contains(regionName));
            if (!string.IsNullOrEmpty(code))
                query = query.Where(c => c.Code.Contains(code));

            int totalCount = query.Count();
            var items = query
                .OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            
            return new PagedEntity<Region>(items, totalCount);
        }

        public List<Domain.Entities.Region> GetRegionsByCountryId(int countryId)
        {
            var items = Set.Where(c => c.CountryId == countryId)
                .ToList();

            return items;
        }
    }
}