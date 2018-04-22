using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();

        void Commit();

        void Rollback();

        int Save();

        Task<int> SaveAsync();

        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
