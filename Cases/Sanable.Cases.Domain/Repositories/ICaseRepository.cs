using BusinessSolutions.Common.Core;
using Sanable.Cases.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Repositories
{
    public interface ICaseRepository : IRepository<Guid, Case>
    {
        Task<PagedEntity<Case>> SearchCases(CaseSearch caseSearch
            , int pageIndex, int pageSize);

        int GetCasesCount(CaseStatus? caseStatus);

        Task<Case> GetCaseWithAids(Guid caseId);
    }
}
