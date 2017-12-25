﻿using BusinessSolutions.Common.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommonSettings.Domain.Entities
{
    public class Country : Entity<int>
    {        
        public string Name { get; set; }                
        
        public string NameEn { get; set; }        
        
        public string Code { get; set; }

        public ICollection<Region> Regions { get; set; }
    }
}
