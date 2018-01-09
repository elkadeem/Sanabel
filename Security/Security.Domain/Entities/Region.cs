using BusinessSolutions.Common.Core.Entities;
using System.Collections.Generic;

namespace Security.Domain
{
    public class Region : Entity<int>
    {
        public string Name { get; set; }
                
        public string NameEn { get; set; }
                
        public string Code { get; set; }
                
        public int CountryId { get; set; }
        
        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
