using System.Collections.Generic;
using CommonSettings.Domain.Entities;
using CommonSettings.Domain.Repositories;
using BusinessSolutions.Common.EntityFramework;
using BusinessSolutions.Common.Core;
using System.Linq;
using System.Data.Entity;
using Grace.DependencyInjection.Attributes;

namespace CommonSettings.DAL

{
    [ExportByInterfaces()]
    public class DistrictRepository : BaseEntityFrameworkRepository<int, District>, IDistrictRepository
    {
        public DistrictRepository(CommonSettingDataContext dataContext) : base(dataContext)
        {
        }

        public PagedEntity<District> GetDistricts(int regionId, int cityId, string districtName
            , string code, int pageIndex, int pageSize)
        {
            var query = Set.AsQueryable();
            if (regionId > 0)
                query = query.Where(c => c.City.RegionId == regionId);
            if (cityId > 0)
                query = query.Where(c => c.CityId == cityId);
            if (string.IsNullOrEmpty(districtName))
                query = query.Where(c => c.Name.Contains(districtName)
                 || c.NameEn.Contains(districtName));
            if (string.IsNullOrEmpty(code))
                query = query.Where(c => c.Code.Contains(code));

            int totalCount = query.Count();
            var items = query.OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedEntity<District>(items, totalCount);
        }

        public List<District> GetDistrictsByCityId(int cityId)
        {
            return Set.Include(c => c.City).Where(c => c.CityId == cityId)                
                .ToList();
        }
    }
}