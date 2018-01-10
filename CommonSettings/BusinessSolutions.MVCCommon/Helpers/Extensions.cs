using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections;
using System.Globalization;

namespace BusinessSolutions.MVCCommon.Helpers
{
    public static class Extensions
    {
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper
            , Expression<Func<TModel, TProperty>> expression
            , IEnumerable<SelectListItem> selectList
            , object htmlAttributes)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string htmlFullName = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (string.IsNullOrEmpty(htmlFullName))
                throw new ArgumentNullException("name");

            string[] values = null;
            if (metaData.Model != null && metaData.Model is IEnumerable)
            {
                var items = ((IEnumerable)metaData.Model).Cast<object>();
                values = items.Select(c => c.ToString()).ToArray();
            }

            var containerDivTag = new TagBuilder("div");
            containerDivTag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            IDictionary<string, object> dictionary = htmlHelper.GetUnobtrusiveValidationAttributes(name, metaData);
            if (selectList != null && selectList.Count() > 0)
            {
                int index = 0;
                foreach (var item in selectList)
                {
                    if (values != null && values.Contains(item.Value))
                        item.Selected = true;
                    CreateCheckBoxItem(containerDivTag, index, item, name, dictionary);
                    index++;
                }
            }


            return MvcHtmlString.Create(containerDivTag.ToString());
        }

        private static void CreateCheckBoxItem(TagBuilder container, int index, SelectListItem item
            , string name, IDictionary<string, object> metaData)
        {

            var labelTag = new TagBuilder("label");
            labelTag.AddCssClass("checkbox-inline");
            var checkBoxTag = new TagBuilder("input");
            checkBoxTag.MergeAttribute("type", "checkbox");
            checkBoxTag.MergeAttribute("name", name);
            checkBoxTag.MergeAttribute("value", item.Value);

            if (index == 0)
                checkBoxTag.MergeAttributes(metaData);
            if (item.Selected)
                checkBoxTag.MergeAttribute("checked", "checked");
            if (item.Disabled)
                checkBoxTag.MergeAttribute("checked", "checked");

            labelTag.InnerHtml = checkBoxTag.ToString(TagRenderMode.SelfClosing) + item.Text;
            container.InnerHtml += labelTag.ToString();
        }
    }
}
