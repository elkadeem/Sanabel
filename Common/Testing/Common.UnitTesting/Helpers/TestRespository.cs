using BusinessSolutions.Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.UnitTesting
{
    public class TestRespository : BaseEntityFrameworkRepository<int, TestEntity>
    {
        public TestRespository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
