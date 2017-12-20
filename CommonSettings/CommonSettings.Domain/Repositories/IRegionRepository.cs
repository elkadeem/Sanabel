using System.Collections.Generic;
using BusinessSolutions.Common.Core;
using CommonSettings.Domain.Entities;

namespace CommonSettings.Domain.Repositories
{
    public interface IRegionRepository : IRepository<int, Region>
    {
        PagedEntity<Region> GetRegions(int countryId, string regionName, string code, int pageIndex, int pageSize);

        List<Region> GetRegionsByCountryId(int countryId);
    }
}