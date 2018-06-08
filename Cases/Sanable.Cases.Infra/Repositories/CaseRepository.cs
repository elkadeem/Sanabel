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

        public async Task<PagedEntity<Case>> SearchCases(CaseSearch caseSearch, int pageIndex, int pageSize)
        {
            var query = Set.Include(c => c.City.Region.Country)
                 .Include(c => c.District);

            if (!string.IsNullOrEmpty(caseSearch.CaseName))
                query = query.Where(c => c.Name.Contains(caseSearch.CaseName));
            if (!string.IsNullOrEmpty(caseSearch.Phone))
                query = query.Where(c => c.Phone.Contains(caseSearch.Phone));
            if (caseSearch.CaseType.HasValue && caseSearch.CaseType.Value > 0)
                query = query.Where(c => c.CaseType == caseSearch.CaseType.Value);
            if (caseSearch.CountryId > 0)
                query = query.Where(c => c.City.Region.CountryId == caseSearch.CountryId);
            if (caseSearch.RegionId > 0)
                query = query.Where(c => c.City.RegionId == caseSearch.RegionId);
            if (caseSearch.CityId > 0)
                query = query.Where(c => c.CityId == caseSearch.CityId);
            if (caseSearch.DistrictId > 0)
                query = query.Where(c => c.DistrictId == caseSearch.DistrictId);
            if (caseSearch.CaseStatus.HasValue && caseSearch.CaseStatus.Value > 0)
                query = query.Where(c => c.CaseStatus == caseSearch.CaseStatus.Value);

            int totalItemCount = await query.CountAsync();
            var items = await query.OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize).Take(pageSize)
                .ToListAsync();
            return new PagedEntity<Case>(items, totalItemCount);
        }

        public int GetCasesCount(CaseStatus? caseStatus)
        {
           return caseStatus.HasValue ?
                 Set.Count(c => c.CaseStatus == caseStatus)
                :  Set.Count();

        }
    }
}
