using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core.Validation
{
    public interface IValidationRule<in TEntity> where TEntity : class
    {
        string ValidationProperty { get; }

        string ValidationMessage { get;}

        bool Validate(TEntity entity);
    }
}
