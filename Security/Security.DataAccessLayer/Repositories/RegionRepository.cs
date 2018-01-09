using BusinessSolutions.Common.EntityFramework;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;
using BusinessSolutions.Common.Core;

namespace Security.DataAccessLayer.Repositories
{
    public class RegionRepository : BaseReadOnlyEntityFrameworkRepository<int, Region>
        , IReadOnlyRepository<int, Region>
    {
        public RegionRepository(SecurityContext dbContext) : base(dbContext)
        {
        }
    }
}
