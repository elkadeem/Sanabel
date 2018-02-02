using BusinessSolutions.Common.Infra.Log;
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
        protected ILogger Logger;        
        public BaseController(ILogger logger)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-SA");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-SA");
            this.Logger = logger;
        }
        
        protected void AddMessageToView(string message, MessageType messageType = MessageType.Information)
        {
            List<UIMessage> messages = ViewBag.HeaderMessages as List<UIMessage>;
            if (messages == null)
                messages = new List<UIMessage>();

            messages.Add(new UIMessage(message, messageType));
            ViewBag.HeaderMessages = messages;
        }

        protected void AddMessageToTempData(string message, MessageType messageType = MessageType.Information)
        {
            List<UIMessage> messages = TempData["HeaderMessages"] as List<UIMessage>;
            if (messages == null)
                messages = new List<UIMessage>();

            messages.Add(new UIMessage(message, messageType));
            TempData["HeaderMessages"] = messages;
        }

        public ActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult RedirectToLocal(string returnUrl, ActionResult defaultActionResult)
        {
            if (defaultActionResult == null)
                throw new ArgumentNullException("defaultActionResult");

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return defaultActionResult;
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new CamelCaseJsonResult(data, JsonRequestBehavior.AllowGet)
            {
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new CamelCaseJsonResult(data, behavior)
            {
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            Logger.Error(filterContext.Exception);
        }
    }
}
