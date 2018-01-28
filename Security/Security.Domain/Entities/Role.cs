using BusinessSolutions.Common.Core.Entities;
using BusinessSolutions.Common.Infra.Validation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;

namespace Security.Domain
{
    public class Role : Entity<Guid>, IRole<Guid>
    {
        private string _name;
        public Role()
        {

        }        

        public string Name {
            get
            {
                return _name;
            }
            set
            {
                Guard.StringIsNull<ArgumentOutOfRangeException>(value, nameof(Name));
                _name = value;
            }
        }
        
        public string NameAr { get; set; }
    }
}