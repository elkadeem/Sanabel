using BusinessSolutions.Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.UnitTesting
{
    public class TestRespository : BaseEntityFrameworkRepository<int, TestEntity, TestDomainEntity>
    {
        public TestRespository(DbContext dbContext) : base(dbContext)
        {

        }

        public override TestDomainEntity GetDomainEntity(TestEntity entity)
        {
            if (entity == null)
                return null;

            return new TestDomainEntity
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        public override TestEntity GetEntity(TestDomainEntity domainEntity)
        {
            if (domainEntity == null)
                return null;

            return new TestEntity
            {
                Id = domainEntity.Id,
                Name = domainEntity.Name,
            };
        }
    }
}
