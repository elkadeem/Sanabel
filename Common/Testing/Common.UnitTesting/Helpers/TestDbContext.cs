using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.UnitTesting
{
    public class TestDbContext : DbContext
    {
        public TestDbContext()
        {
        }

        public virtual DbSet<TestEntity> TestEntities { get; set; }
    }
}
