using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Infra.Validation
{
    public enum ValidationErrorTypes
    {
        EmptyString,
        ZeroValue,
        NegativeValue,
        UniqueViolation,
    }
}
