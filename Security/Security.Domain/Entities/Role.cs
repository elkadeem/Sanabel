using BusinessSolutions.Common.Core.Entities;
using BusinessSolutions.Common.Infra.Validation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;

namespace Sanabel.Security.Domain
{
    public class Role : Entity<Guid>, IRole<Guid>
    {
        private string _name;
        private Role()
        {
            Id = Guid.NewGuid();
        }

        public Role(string name, string nameAr) : this()
        {
            Guard.StringIsNull<ArgumentNullException>(name, nameof(name));
            Guard.StringIsNull<ArgumentNullException>(nameAr, nameof(nameAr));
            this.Name = name;
            this.NameAr = nameAr;
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
        
        public string NameAr { get; private set; }
    }
}