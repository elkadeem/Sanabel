using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Infra.Log
{
    public interface ILogger
    {
        void Error(Exception exception);

        void Error(string error);

        void Info(string message);
    }
}
