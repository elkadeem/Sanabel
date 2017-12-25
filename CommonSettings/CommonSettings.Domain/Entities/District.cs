using BusinessSolutions.Common.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.Domain.Entities
{
    public class District : Entity<int>
    {        
        public string Name { get; set; }
                
        public string NameEn { get; set; }
                
        public string Code { get; set; }
                
        public int CityId { get; set; }

        public City City { get; set; }
    }
}
