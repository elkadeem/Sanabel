using BusinessSolutions.Common.Infra.Log;
using BusinessSolutions.Common.Infra.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Sanabel.Presentation.Localization;
using Sanabel.Presentation.MVC.Models;
using Sanabel.Security.Application;
using Sanabel.Security.Application.Models;
using Sanabel.Security.Domain;
using Security.AspIdentity;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sanabel.Presentation.MVC.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly ILogger _logger;
        public ManageController(IUserService userService, ApplicationUserManager userManager, ApplicationSignInManager signInManager, ILogger logger)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(userService, nameof(userService));
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? AccountResource.ChangePasswordSuccess
                : message == ManageMessageId.SetPasswordSuccess ? AccountResource.SetPasswordSuccess
                : message == ManageMessageId.SetTwoFactorSuccess ? AccountResource.SetTwoFactorSuccess
                : message == ManageMessageId.Error ? AccountResource.ManagerError
                : message == ManageMessageId.AddPhoneSuccess ? AccountResource.AddPhoneSuccess
                : message == ManageMessageId.RemovePhoneSuccess ? AccountResource.RemovePhoneSuccess
                : "";

            Guid userId = Guid.Parse(User.Identity.GetUserId());
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(userId),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(userId),
                Logins = await _userManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId.ToString())
            };
            return View(model);
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            EntityResult entityResult = await _userService.ChangePassword(userId, model);
            if (entityResult.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }

            AddErrors(entityResult);
            return View(model);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(EntityResult result)
        {
            foreach (var error in result.ValidationErrors)
            {
                ModelState.AddModelError("", error.Message);
            }
        }

        private bool HasPassword()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            var user = _userManager.FindById(userId);
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            var user = _userManager.FindById(userId);
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}