using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.EntityFramework;
using Sanable.Cases.Domain.Model;
using Sanable.Cases.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;

namespace Sanable.Cases.Infra
{
    public class CaseRepository : BaseEntityFrameworkRepository<Guid, Case>, ICaseRepository
    {
        public CaseRepository(CaseResearchDataContext dbContext) : base(dbContext)
        {
        }

        public override Task<Case> GetByIDAsync(Guid key)
        {
            return Set.Include(c => c.City.Region.Country)
                .Include(c => c.District).FirstOrDefaultAsync(c => c.Id == key);
        }

        public async Task<PagedEntity<Case>> SearchCases(string caseName, string phone
            , CaseTypes? caseType, int countryId, int regionId
            , int cityId, int districtId, int pageIndex, int pageSize)
        {
           var query = Set.Include(c => c.City.Region.Country)
                .Include(c => c.District);

            if (!string.IsNullOrEmpty(caseName))
                query = query.Where(c => c.Name.Contains(caseName));
            if(!string.IsNullOrEmpty(phone))
                query = query.Where(c => c.Phone.Contains(phone));
            if (caseType.HasValue && caseType.Value > 0)
                query = query.Where(c => c.CaseType == caseType.Value);
            if (countryId > 0)
                query = query.Where(c => c.City.Region.CountryId == countryId);
            if(regionId > 0)
                query = query.Where(c => c.City.RegionId == regionId);
            if (cityId > 0)
                query = query.Where(c => c.CityId == cityId);
            if (districtId > 0)
                query = query.Where(c => c.DistrictId == districtId);

            int totalItemCount = await query.CountAsync();
            var items = await query.OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize).Take(pageSize)
                .ToListAsync();
            return new PagedEntity<Case>(items, totalItemCount);
        }

        public async Task<PagedEntity<Case>> SearchNonApprovedCases(string caseName, string phone
            , CaseTypes? caseType, int countryId, int regionId
            , int cityId, int districtId, int pageIndex, int pageSize)
        {
            var query = Set.Include(c => c.City.Region.Country)
                 .Include(c => c.District);

            if (!string.IsNullOrEmpty(caseName))
                query = query.Where(c => c.Name.Contains(caseName));
            if (!string.IsNullOrEmpty(phone))
                query = query.Where(c => c.Phone.Contains(phone));
            if (caseType.HasValue && caseType.Value > 0)
                query = query.Where(c => c.CaseType == caseType.Value);
            if (countryId > 0)
                query = query.Where(c => c.City.Region.CountryId == countryId);
            if (regionId > 0)
                query = query.Where(c => c.City.RegionId == regionId);
            if (cityId > 0)
                query = query.Where(c => c.CityId == cityId);
            if (districtId > 0)
                query = query.Where(c => c.DistrictId == districtId);

            int totalItemCount = await query.CountAsync();
            var items = await query.OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize).Take(pageSize)
                .ToListAsync();
            return new PagedEntity<Case>(items, totalItemCount);
        }
        


    }
}
