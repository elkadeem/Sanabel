using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core
{
    public interface IRepository<Tkey, TEntity> : IReadOnlyRepository<Tkey, TEntity> where TEntity: class 
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(Tkey id);

        Tkey GetPrimaryKey(TEntity entity);
    }
}
