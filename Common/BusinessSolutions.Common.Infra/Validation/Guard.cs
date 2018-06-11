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

        public static void CollectioNullOrEmpty<T>(IEnumerable<T> items, string paramName, string message = null)
        {
            if (items == null)
            {
                if (string.IsNullOrEmpty(message))
                    throw new ArgumentNullException(nameof(paramName));
                else
                    throw new ArgumentNullException(nameof(paramName), message);
            }
            else if (!items.Any())
            {
                if (string.IsNullOrEmpty(message))
                    throw new ArgumentOutOfRangeException(nameof(paramName));
                else
                    throw new ArgumentOutOfRangeException(nameof(paramName), message);
            }
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

        public static void LessThanOrEqualZero(double value, string paramName, string message = null)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(paramName);
        }

        public static void LessThanZero(double value, string paramName, string message = null)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(paramName);
        }
    }
}
