using BusinessSolutions.Common.Core.Entities;
using System;

namespace Sanable.Cases.Domain.Model
{
    public class Volunteer : Entity<Guid>
    {        
        public Volunteer()
        {
            
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        
    }
}
