using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessSolutions.MVCCommon
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MustBeGreateThanZeroFilterAttribute : ActionFilterAttribute
    {
        private readonly List<string> _paramterNames;
        public MustBeGreateThanZeroFilterAttribute(params string[] paramterNames)
        {
            if (paramterNames == null || paramterNames.Length == 0)
                throw new ArgumentNullException("paramterNames");

            _paramterNames = paramterNames.ToList();
        }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public object RouteValues { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var parameters = filterContext.ActionParameters;
            bool isAllParamtersPoistive = true;
            foreach (var parameter in _paramterNames)
            {
                if (!parameters.Keys.Contains(parameter))
                    throw new ArgumentOutOfRangeException(parameter, $"Parameter {parameter} is not exist");

                var parameterValue = parameters[parameter];
                double result = double.MinValue;
                if (parameterValue == null
                    || !double.TryParse(parameterValue.ToString(), out result)
                    || result <= 0)
                    isAllParamtersPoistive = false;
            }

            if (!isAllParamtersPoistive)
            {
                UrlHelper helper = new UrlHelper(filterContext.RequestContext);
                string url = "";
                if (!string.IsNullOrEmpty(ActionName)
                    && !string.IsNullOrEmpty(ControllerName) && RouteValues != null)
                    url = helper.Action(ActionName, ControllerName, RouteValues);
                else if (!string.IsNullOrEmpty(ActionName)
                     && RouteValues != null)
                    url = helper.Action(ActionName, RouteValues);
                else if (!string.IsNullOrEmpty(ActionName))
                    url = helper.Action(ActionName);
                else
                    url = helper.Action("Index", "Home");

                filterContext.Result = new RedirectResult(url);
            }
            else
                base.OnActionExecuting(filterContext);
        }
    }
}
