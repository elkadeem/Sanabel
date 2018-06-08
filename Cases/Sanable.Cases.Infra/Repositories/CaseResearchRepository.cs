using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.EntityFramework;
using Sanable.Cases.Domain.Model;
using Sanable.Cases.Domain.Repositories;
using System;

namespace Sanable.Cases.Infra
{
    public class CaseResearchRepository : BaseEntityFrameworkRepository<Guid
        , CaseResearch>, ICaseResearchRepository
    {
        public CaseResearchRepository(CaseResearchDataContext dbContext) : base(dbContext)
        {
        }

        
    }
}
