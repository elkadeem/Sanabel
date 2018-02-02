using BusinessSolutions.Common.Infra.Log;
using BusinessSolutions.Common.Infra.Validation;
using BusinessSolutions.Localization;
using BusinessSolutions.MVCCommon.Controllers;
using PagedList;
using Sanabel.Security.Application;
using Sanabel.Security.Application.Models;
using Security.AspIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sanabel.Presentation.MVC.Controllers
{    
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;        
        private readonly ILogger _logger;

        public UsersController(IUserService userService, ILogger logger) : base(logger)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(userService, nameof(userService));
            _userService = userService;
            _logger = logger;
        }

        public ActionResult Index(SearchUsersViewModel model)
        {
            var users = _userService.SearchUser(model);
            model.Items = new StaticPagedList<ViewUserViewModel>(users.Items
                , model.PageIndex + 1, model.PageSize, users.TotalCount);
            return View(model);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var item = await _userService.GetUser(id);
            if (item == null)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public ActionResult Create()
        {
            UserViewModel model = new UserViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EntityResult result = await _userService.AddUser(userModel);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Users");
                    }

                    AddErrors(result);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }


            return View(userModel);
        }

        public async Task<ActionResult> Update(Guid id)
        {
            var user = await _userService.GetUser(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Guid id, [Bind(Exclude = "Password,ConfirmPassword")]UserViewModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userModel.Id = id;
                    EntityResult result = await _userService.UpdateUser(userModel);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Users");
                    }

                    AddErrors(result);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }


            return View(userModel);
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(Guid userId, SetPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EntityResult result = await _userService.ResetUserPassword(userId, model);
                    if (result.Succeeded)
                        return RedirectToAction("Users");

                    AddErrors(result);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BlockUSer(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EntityResult result = await _userService.BlockUser(id);
                    if (result.Succeeded)
                        return RedirectToAction("Users");

                    AddErrors(result);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UnBlockUSer(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EntityResult result = await _userService.UnBlockUser(id);
                    if (result.Succeeded)
                        return RedirectToAction("Users");

                    AddErrors(result);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return RedirectToAction("Details", new { id = id });
        }

        private void AddErrors(EntityResult result)
        {
            foreach (var error in result.ValidationErrors)
            {
                ModelState.AddModelError("", error.Message);
            }
        }
    }
}