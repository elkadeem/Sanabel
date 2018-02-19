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
using System.Web.Mvc.Html;

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
                throw new ArgumentNullException(nameof(expression), "The model name is not exist for expression");

            string[] values = null;
            if (metaData.Model != null && metaData.Model is IEnumerable)
            {
                var items = ((IEnumerable)metaData.Model).Cast<object>();
                values = items.Select(c => c.ToString()).ToArray();
            }

            var containerDivTag = new TagBuilder("div");
            containerDivTag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            IDictionary<string, object> dictionary = htmlHelper.GetUnobtrusiveValidationAttributes(name, metaData);
            if (selectList != null && selectList.Any())
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

        public static MvcHtmlString LocalizedEnumDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper
            , Expression<Func<TModel, TProperty>> expression
            , string optionLabel
            , object htmlAttributes)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            if (metadata == null)
                throw new ArgumentException("metad data is null", nameof(expression));
            if (metadata.ModelType == (Type)null)
                throw new ArgumentException("metadata type is null", nameof(expression));
            if (!EnumHelper.IsValidForEnumHelper(metadata.ModelType))
                throw new ArgumentException("value is not enum", nameof(expression));

            string expressionText = ExpressionHelper.GetExpressionText((LambdaExpression)expression);
            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionText);
            Enum @enum = (Enum)null;
            if (!string.IsNullOrEmpty(fullHtmlFieldName))
                @enum = GetModelStateValue(htmlHelper, fullHtmlFieldName, metadata.ModelType) as Enum;
            if (@enum == null && !string.IsNullOrEmpty(expressionText))
                @enum = htmlHelper.ViewData.Eval(expressionText) as Enum;
            if (@enum == null)
                @enum = metadata.Model as Enum;
            IList<SelectListItem> selectList = EnumHelperExtension.GetSelectList(metadata.ModelType, @enum);
            if (!string.IsNullOrEmpty(optionLabel) && selectList.Count != 0 && string.IsNullOrEmpty(selectList[0].Text))
            {
                selectList[0].Text = optionLabel;
                selectList[0].Value = "";
                optionLabel = (string)null;
            }

            return htmlHelper.BuildEnumDropDown(metadata, optionLabel, fullHtmlFieldName, selectList, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        private static MvcHtmlString BuildEnumDropDown(this HtmlHelper htmlHelper
            , ModelMetadata metadata, string optionLabel, string name, IEnumerable<SelectListItem> selectList
            , IDictionary<string, object> htmlAttributes)
        {
            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (string.IsNullOrEmpty(fullHtmlFieldName))
                throw new ArgumentException(nameof(name));
            
            StringBuilder stringBuilder = new StringBuilder();
            TagBuilder optionTag = null;
            if (!string.IsNullOrEmpty(optionLabel))
            {
                optionTag = new TagBuilder("option");
                optionTag.SetInnerText(optionLabel);
                stringBuilder.Append(optionTag.ToString());
            }

            foreach (var item in selectList)
            {
                optionTag = new TagBuilder("option");
                optionTag.MergeAttribute("value", item.Value);
                if(item.Selected)
                    optionTag.MergeAttribute("selected", "selected");
                optionTag.SetInnerText(item.Text);
                stringBuilder.Append(optionTag.ToString());
            }

            TagBuilder tagBuilder1 = new TagBuilder("select");
            tagBuilder1.InnerHtml = stringBuilder.ToString();
            tagBuilder1.MergeAttributes<string, object>(htmlAttributes);
            tagBuilder1.MergeAttribute(nameof(name), fullHtmlFieldName, true);
            tagBuilder1.GenerateId(fullHtmlFieldName);

            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out modelState) && modelState.Errors.Count > 0)
                tagBuilder1.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            tagBuilder1.MergeAttributes<string, object>(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));
            return MvcHtmlString.Create(tagBuilder1.ToString(TagRenderMode.Normal));
        }



        private static object GetModelStateValue<TModel>(HtmlHelper<TModel> htmlHelper, string key, Type destinationType)
        {
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(key, out modelState) && modelState.Value != null)
                return modelState.Value.ConvertTo(destinationType, (CultureInfo)null);
            return (object)null;
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
