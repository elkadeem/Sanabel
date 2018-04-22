using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core.Specifications
{
    public interface ISpecification<in T>
    {
        bool IsSatisfiedBy(T entity);
    }
}
