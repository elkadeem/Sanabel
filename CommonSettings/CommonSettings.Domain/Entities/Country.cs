using BusinessSolutions.Common.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommonSettings.Domain.Entities
{
    public class Country : Entity<int>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
                
        [StringLength(50)]
        public string NameEn { get; set; }
        
        [StringLength(10)]
        public string Code { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
