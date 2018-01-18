using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.EntityFramework;
using Sanable.Cases.Domain.Model;
using Sanable.Cases.Domain.Repositories;
using System;

namespace Sanable.Cases.Infra
{
    public class CaseRepository : BaseEntityFrameworkRepository<Guid, Case>, ICaseRepository
    {
        public CaseRepository(CaseResearchDataContext dbContext) : base(dbContext)
        {
        }
    }
}
