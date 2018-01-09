using BusinessSolutions.Common.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Domain
{
    public class City : Entity<int>
    {
        public string Name { get; set; }

        public string NameEn { get; set; }

        public string Code { get; set; }

        public int RegionId { get; set; }

        public Region Region { get; set; }

        public ICollection<District> Districts { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
