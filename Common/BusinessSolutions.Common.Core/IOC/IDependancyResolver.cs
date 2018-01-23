using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core.IOC
{
    public interface IDependancyResolver
    {
        T Get<T>();

        IEnumerable<T> GetAll<T>();
    }
}
