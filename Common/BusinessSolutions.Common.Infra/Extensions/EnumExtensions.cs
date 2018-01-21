using BusinessSolutions.Common.Infra.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Infra.Extensions
{
    public static class EnumExtensions
    {
        public static string GetLocalizedEnumDescription(this Enum @enum)
        {
            System.Reflection.FieldInfo field = @enum.GetType().GetField(@enum.ToString());

            LocalizedDescriptionAttribute customAttribute = field.GetCustomAttribute<LocalizedDescriptionAttribute>(false);
            if (customAttribute != null)
            {
                string name = customAttribute.Description;
                if (!string.IsNullOrEmpty(name))
                    return name;
            }
            return field.Name;
        }
    }
}
