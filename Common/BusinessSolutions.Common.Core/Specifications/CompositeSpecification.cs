using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core.Specifications
{
    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T entity);

        public ISpecification<T> And(ISpecification<T> specification)
        {
            if (specification == null)
                throw new ArgumentNullException("specification");

            return new AndSpecification<T>(this, specification);
        }

        public ISpecification<T> Not(ISpecification<T> specification)
        {
            if (specification == null)
                throw new ArgumentNullException("specification");

            return new NotSpecification<T>(specification);
        }

        public ISpecification<T> Or(ISpecification<T> specification)
        {
            if (specification == null)
                throw new ArgumentNullException("specification");

            return new OrSpecification<T>(this, specification);
        }
    }
}
