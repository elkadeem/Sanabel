using BusinessSolutions.Common.Core.Entities;
using System.Collections.Generic;

namespace Security.Domain
{
    public class District : Entity<int>
    {        
        public string Name { get; set; }
                
        public string NameEn { get; set; }
                
        public string Code { get; set; }
                
        public int CityId { get; set; }

        public City City { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
