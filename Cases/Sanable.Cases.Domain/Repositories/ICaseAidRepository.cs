using BusinessSolutions.Common.Core;
using Sanable.Cases.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Repositories
{
    public interface ICaseAidRepository : IRepository<Guid, Aid>
    {
    }
}
