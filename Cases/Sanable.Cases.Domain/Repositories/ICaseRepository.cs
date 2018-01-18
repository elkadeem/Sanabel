using BusinessSolutions.Common.Core;
using Sanable.Cases.Domain.Model;
using System;

namespace Sanable.Cases.Domain.Repositories
{
    public interface ICaseRepository : IRepository<Guid, Case>
    {
    }
}
