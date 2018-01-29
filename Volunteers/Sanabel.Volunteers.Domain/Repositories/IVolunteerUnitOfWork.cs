using BusinessSolutions.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Volunteers.Domain.Repositories
{
    public interface IVolunteerUnitOfWork : IUnitOfWork
    {
        IVolunteerRepository VolunteerRepository { get; }
    }
}
