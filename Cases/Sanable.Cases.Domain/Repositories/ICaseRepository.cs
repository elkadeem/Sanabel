using BusinessSolutions.Common.Core;
using Sanable.Cases.Domain.Model;
using System;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Repositories
{
    public interface ICaseRepository : IRepository<Guid, Case>
    {
        Task<PagedEntity<Case>> SearchCases(string caseName, string phone
            , CaseTypes? caseType, int countryId, int regionId, int cityId, int districtId
            , int pageIndex, int pageSize);

        Task<PagedEntity<Case>> SearchNonApprovedCases(string caseName, string phone
           , CaseTypes? caseType, int countryId, int regionId, int cityId, int districtId
           , int pageIndex, int pageSize);
    }
}
