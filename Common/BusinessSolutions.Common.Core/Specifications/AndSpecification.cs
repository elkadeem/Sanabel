using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core.Specifications
{
    internal class AndSpecification<T> : CompositeSpecification<T>
    {
        private ISpecification<T> _leftSpecification;
        private ISpecification<T> _rightSpecification;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {            
            _leftSpecification = left;
            _rightSpecification = right;
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return this._leftSpecification.IsSatisfiedBy(entity)
                || this._rightSpecification.IsSatisfiedBy(entity);
        }
    }
}
