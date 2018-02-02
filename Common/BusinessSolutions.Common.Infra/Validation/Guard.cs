using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Infra.Validation
{
    public static class Guard
    {
        public static void ArgumentIsNull<T>(object obj, string paramName, string message = null) where T : ArgumentException
        {
            if (obj == null)
                if (!string.IsNullOrEmpty(message))
                    throw (T)Activator.CreateInstance(typeof(T), paramName, message);
                else
                    throw (T)Activator.CreateInstance(typeof(T), paramName);
        }

        public static void StringIsNull<T>(string obj, string paramName, string message = null) where T : ArgumentException
        {            
            if (string.IsNullOrEmpty(obj))
                if (!string.IsNullOrEmpty(message))
                    throw (T)Activator.CreateInstance(typeof(T), paramName, message);
                else
                    throw (T)Activator.CreateInstance(typeof(T), paramName);
        }

        public static void GuidIsEmpty<T>(Guid obj, string paramName, string message = null) where T : ArgumentException
        {
            if (obj == Guid.Empty)
                if (!string.IsNullOrEmpty(message))
                    throw (T)Activator.CreateInstance(typeof(T), paramName, message);
                else
                    throw (T)Activator.CreateInstance(typeof(T), paramName);
        }

        public static void LessThanOrEqualZero(int value, string paramName, string message = null)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(paramName);
        }
    }
}
