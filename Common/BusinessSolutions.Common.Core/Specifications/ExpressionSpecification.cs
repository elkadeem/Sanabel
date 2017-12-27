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

        private ExpressionSpecification<T> And(Expression<Func<T, bool>> expression)
        {
            BinaryExpression andExpression = System.Linq.Expressions.Expression.AndAlso(this.Expression.Body
                , expression.Body);
            
            
            var paramExpr = System.Linq.Expressions.Expression.Parameter(typeof(T));
            andExpression = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(andExpression);

            Expression<Func<T, bool>> result = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(andExpression
                , Expression.Parameters.Single());
            return new ExpressionSpecification<T>(result);
        }

        private ExpressionSpecification<T> Or(Expression<Func<T, bool>> expression)
        {
            BinaryExpression orExpression = System.Linq.Expressions.Expression.OrElse(this.Expression
                , expression);
            Expression<Func<T, bool>> result = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>
                (orExpression
                , Expression.Parameters.Single());
            return new ExpressionSpecification<T>(result);
        }

        private ExpressionSpecification<T> Not(Expression<Func<T, bool>> expression)
        {
            UnaryExpression notExpression = System.Linq.Expressions.Expression.Not(expression);
            Expression<Func<T, bool>> result = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(notExpression);
            return new ExpressionSpecification<T>(result);
        }
    }

    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;


        protected override Expression VisitParameter(ParameterExpression node)
            => base.VisitParameter(_parameter);

        internal ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }
    }
}
