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
        private List<UIMessage> _viewDataMessages;
        private List<UIMessage> _tempDataMessages;

        protected NLog.ILogger Logger => this.Logger;

        public BaseController(NLog.ILogger logger)
        {
            _logger = logger;
            _viewDataMessages = new List<UIMessage>();
            _tempDataMessages = new List<UIMessage>();
        }

        protected void AddMessageToView(string message, MessageType messageType = MessageType.Information)
        {
            _viewDataMessages.Add(new UIMessage(message, messageType));
        }

        protected void AddMessageToTempData(string message, MessageType messageType = MessageType.Information)
        {
            _tempDataMessages.Add(new UIMessage(message, messageType));
        }

        protected override void EndExecute(IAsyncResult asyncResult)
        {
            ViewBag.HeaderMessages = _viewDataMessages;
            TempData["HeaderMessages"] = _tempDataMessages;
            base.EndExecute(asyncResult);
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
