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

        Task<CaseViewModel> GetCase(Guid caseId);

        Task<EntityResult> AddCase(CaseViewModel caseModel);

        Task<EntityResult> UpdateCase(CaseViewModel caseModel);

        Task<EntityResult> DeleteCase(Guid caseId);

        Task<EntityResult> UpdateCaseStatus(CaseActionViewModel caseModel);

        Task<CaseActionViewModel> GetCaseAction(Guid caseId);

        int GetCasesCount(CaseStatus? caseStatus);

        Task<CaseAidsViewModel> GetCaseAids(Guid caseId);

        Task<EntityResult> AddCaseAid(Guid caseId, CaseAidViewModel caseAidViewModel);

        Task<EntityResult> UpdateCaseAid(CaseAidViewModel caseAidViewModel);

        Task<EntityResult> DeleteCaseAid(CaseAidViewModel caseAidViewModel);
        Task<CaseAidViewModel> GetCaseAidById(Guid aidId);
    }
}
