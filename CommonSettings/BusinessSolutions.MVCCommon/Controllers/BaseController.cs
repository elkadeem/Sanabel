using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessSolutions.MVCCommon.Controllers
{
    public class BaseController : Controller
    {
        private NLog.ILogger _logger;

        protected NLog.ILogger Logger => this.Logger;

        public BaseController(NLog.ILogger logger)
        {
            _logger = logger;
        }

        public ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            Logger.Error(filterContext.Exception);
        }
    }
}
