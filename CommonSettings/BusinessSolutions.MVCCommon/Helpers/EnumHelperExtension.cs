using BusinessSolutions.Common.Infra.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BusinessSolutions.MVCCommon.Helpers
{
    public static class EnumHelperExtension
    {
        public static IList<SelectListItem> GetSelectList(Type type)
        {
            if (type == (Type)null)
                throw new ArgumentNullException("type");
            if (!EnumHelper.IsValidForEnumHelper(type))
                throw new ArgumentException("type");
            IList<SelectListItem> selectListItemList = (IList<SelectListItem>)new List<SelectListItem>();
            Type type1 = Nullable.GetUnderlyingType(type);
            if ((object)type1 == null)
                type1 = type;
            Type type2 = type1;
            if (type2 != type)
                selectListItemList.Add(new SelectListItem()
                {
                    Text = string.Empty,
                    Value = string.Empty
                });
            foreach (FieldInfo field in type2.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField))
            {
                object rawConstantValue = field.GetRawConstantValue();
                selectListItemList.Add(new SelectListItem()
                {
                    Text = EnumHelperExtension.GetDisplayName(field),
                    Value = rawConstantValue.ToString()
                });
            }
            return selectListItemList;
        }

        public static IList<SelectListItem> GetSelectList(Type type, Enum value)
        {
            IList<SelectListItem> selectList = EnumHelperExtension.GetSelectList(type);
            Type type1 = value == null ? (Type)null : value.GetType();
            if (type1 != (Type)null && type1 != type && type1 != Nullable.GetUnderlyingType(type))
                throw new ArgumentException("value");

            if (value == null && selectList.Count != 0 && string.IsNullOrEmpty(selectList[0].Value))
            {
                selectList[0].Selected = true;
            }
            else
            {
                string str = value == null ? "0" : value.ToString("d");
                bool flag = false;
                for (int index = selectList.Count - 1; !flag && index >= 0; --index)
                {
                    SelectListItem selectListItem = selectList[index];
                    selectListItem.Selected = str == selectListItem.Value;
                    flag |= selectListItem.Selected;
                }
                if (!flag)
                {
                    if (selectList.Count != 0 && string.IsNullOrEmpty(selectList[0].Value))
                    {
                        selectList[0].Selected = true;
                        selectList[0].Value = str;
                    }
                    else
                        selectList.Insert(0, new SelectListItem()
                        {
                            Selected = true,
                            Text = string.Empty,
                            Value = str
                        });
                }
            }
            return selectList;
        }

        private static string GetDisplayName(FieldInfo field)
        {
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
