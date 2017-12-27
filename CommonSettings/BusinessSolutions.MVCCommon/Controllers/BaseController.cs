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
        protected NLog.ILogger Logger => this._logger;

        public BaseController(NLog.ILogger logger)
        {
            _logger = logger;            
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
