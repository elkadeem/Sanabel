using BusinessSolutions.Common.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Model
{
    public abstract class Aid : Entity<Guid>
    {
        public Guid CaseId { get; protected set; }

        public DateTime AidDate { get; protected set; }

        public string AidDescription { get; protected set; }

        public Case ApprovedCase { get; protected set; }
    }
}
