using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.EntityFramework;
using Sanable.Cases.Domain.Model;
using Sanable.Cases.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Sanable.Cases.Infra
{
    public class CaseAidRepository : BaseEntityFrameworkRepository<Guid, Aid>, ICaseAidRepository
    {
        public CaseAidRepository(CaseResearchDataContext dbContext) : base(dbContext)
        {
        }

        public override Task<Aid> GetByIDAsync(Guid key)
        {
            return Set.Include(c => c.Case.City.Region.Country)
                .Include(c => c.Case.District).FirstOrDefaultAsync(c => c.Id == key);
        }
    }
}
