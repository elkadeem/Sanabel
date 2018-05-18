using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Infra.Validation;
using Sanabel.Cases.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Cases.App
{
    public interface ICasesService
    {
        Task<PagedEntity<CaseViewModel>> GetCases(SearchCaseViewModel searchViewModel);

        Task<PagedEntity<CaseViewModel>> GetNonApprovedCases(SearchCaseViewModel searchViewModel);

        Task<PagedEntity<CaseViewModel>> GetApprovedCases(SearchCaseViewModel searchViewModel);

        Task<CaseViewModel> GetCase(Guid caseId);

        Task<EntityResult> AddCase(CaseViewModel caseModel);

        Task<EntityResult> UpdateCase(CaseViewModel caseModel);

        Task<EntityResult> DeleteCase(Guid caseId);

        Task<EntityResult> ApproveCase(CaseViewModel caseModel);

        Task<EntityResult> SuspendCase(Guid caseId);
    }
}
