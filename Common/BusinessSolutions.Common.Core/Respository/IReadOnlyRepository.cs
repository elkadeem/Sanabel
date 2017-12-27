using BusinessSolutions.Common.Core.Entities;
using BusinessSolutions.Common.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core
{
    public interface IReadOnlyRepository<TKey, TEntity> where TEntity : Entity<TKey> 
    {
        List<TEntity> GetAll();

        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        PagedEntity<TEntity> PageAll(int pageIndex, int pageSize);

        Task<PagedEntity<TEntity>> PageAllAsync(int pageIndex, int pageSize);

        Task<PagedEntity<TEntity>> PageAllAsync(CancellationToken cancellationToken, int pageIndex, int pageSize);

        TEntity GetByID(TKey key);

        Task<TEntity> GetByIDAsync(TKey key);

        Task<TEntity> GetByIDAsync(CancellationToken cancellationToken, TKey key);

        List<TEntity> Find(ExpressionSpecification<TEntity> specification);

        PagedEntity<TEntity> Find(ExpressionSpecification<TEntity> specification, int pageIndex, int pageSize);

    }
}
