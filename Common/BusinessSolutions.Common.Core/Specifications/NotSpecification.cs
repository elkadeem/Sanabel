using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core.Specifications
{
    internal class NotSpecification<T> : CompositeSpecification<T>
    {
        private ISpecification<T> _specification;
        
        public NotSpecification(ISpecification<T> specification)
        {
            _specification = specification;            
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return !this._specification.IsSatisfiedBy(entity);
        }
    }
}
