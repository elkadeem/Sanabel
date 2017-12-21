using System.Collections.Generic;
using BusinessSolutions.Common.Core;
using CommonSettings.Domain.Entities;

namespace CommonSettings.Domain.Repositories
{
    public interface IDistrictRepository : IRepository<int, District>
    {
        PagedEntity<District> GetDistricts(int regionId, int cityId, string districtName, string code, int pageIndex, int pageSize);

        List<District> GetDistrictsByCityId(int cityId);
    }
}