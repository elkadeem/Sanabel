using BusinessSolutions.Common.Infra.Validation;
using BusinessSolutions.MVCCommon.Controllers;
using NLog;
using PagedList;
using Security.Application.Models;
using Security.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sanabel.Presentation.MVC.Areas.Volunteers.Controllers
{
    [Authorize]
    public class VolunteerController : BaseController
    {        
        private IUserService _userService;
        public VolunteerController(IUserService userService, ILogger logger) : base(logger)
        {
            _userService = userService;
        }

        
        public ActionResult Index(SearchVolunteersViewModel searchUserModel)
        {
            var result = _userService.SearchUser(searchUserModel);
            searchUserModel.Items = new StaticPagedList<ViewVolunteerViewModel>(result.Items
                , searchUserModel.PageIndex + 1, searchUserModel.PageSize, result.TotalCount);
            return View(searchUserModel);
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            var  model = new VolunteerViewModel();
            GetRoles();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Security.Application.Models.VolunteerViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entityResult = await _userService.AddUser(model);
                    if (entityResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in entityResult.ValidationErrors)
                        {
                            ModelState.AddModelError("", error.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex);
            }

            GetRoles();
            return View(model);
        }        

        [AllowAnonymous]
        public async Task<ActionResult> Edit(Guid id)
        {
            Security.Application.Models.VolunteerViewModel userModel = await _userService.GetUser(id);
            GetRoles();
            return View(userModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, [Bind(Exclude = "UserName,Password,ConfirmPassword")]Security.Application.Models.VolunteerViewModel userModel)
        {
            try
            {
                userModel.Id = id;
                EntityResult result = await _userService.UpdateUser(userModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.ValidationErrors)
                    {
                        ModelState.AddModelError("", error.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex);
            }

            GetRoles();
            return View(userModel);
        }

        private void GetRoles()
        {
            var roles = _userService.GetAllRoles();
            ViewBag.Roles = roles.Select(c =>
                new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.NameAr
                }).ToList();
        }
    }
}