using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core.Specifications
{
    public class ExpressionSpecification<T> : CompositeSpecification<T>
    {
        private Expression<Func<T, bool>> _expression;

        public Expression<Func<T, bool>> Expression => _expression;

        public ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            _expression = expression;
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return this._expression.Compile().Invoke(entity);
        }
    }
}
