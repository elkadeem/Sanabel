using BusinessSolutions.Common.Core.Entities;

namespace Common.UnitTesting
{
    public class TestEntity : Entity<int>
    {
        public string Name { get; set; }
        public override int Id { get; set; }
    }
}