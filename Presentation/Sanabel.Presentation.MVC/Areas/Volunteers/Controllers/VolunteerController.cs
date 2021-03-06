﻿using BusinessSolutions.Common.Infra.Log;
using BusinessSolutions.Common.Infra.Validation;
using BusinessSolutions.Localization;
using BusinessSolutions.MVCCommon.Controllers;

using PagedList;
using Sanabel.Security.Application;
using Sanabel.Volunteers.Application.Models;
using Sanabel.Volunteers.Application.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sanabel.Presentation.MVC.Areas.Volunteers.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class VolunteerController : BaseController
    {
        private readonly IVolunteerService _volunteerService;
        private readonly IUserService _userService;
        public VolunteerController(IVolunteerService volunteerService, IUserService userService, ILogger logger) : base(logger)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(volunteerService, nameof(volunteerService));
            Guard.ArgumentIsNull<ArgumentNullException>(userService, nameof(userService));
            _volunteerService = volunteerService;
            _userService = userService;
        }


        public async Task<ActionResult> Index(SearchVolunteersViewModel searchUserModel)
        {
            var result = await _volunteerService.SearchVolunteers(searchUserModel);
            searchUserModel.Items = new StaticPagedList<ViewVolunteerViewModel>(result.Items
                , searchUserModel.PageIndex + 1, searchUserModel.PageSize, result.TotalCount);
            return View(searchUserModel);
        }


        public ActionResult Create()
        {
            var model = new VolunteerViewModel();
            GetRoles();
            return View(model);
        }

        public async Task<ActionResult> Details(Guid id)
        {           
            VolunteerViewModel userModel = await _volunteerService.GetVolunteer(id);
            if (userModel == null)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }
            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VolunteerViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entityResult = await _volunteerService.AddVolunteer(model);
                    if (entityResult.Succeeded)
                    {
                        AddMessageToTempData(CommonResources.SavedSuccessfullyMessage
                            , BusinessSolutions.MVCCommon.MessageType.Success);
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
                AddMessageToView(CommonResources.UnExpectedError
                    , BusinessSolutions.MVCCommon.MessageType.Error);
            }

            GetRoles();
            return View(model);
        }


        public async Task<ActionResult> Edit(Guid id)
        {
            VolunteerViewModel userModel = await _volunteerService.GetVolunteer(id);
            if (userModel == null)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }

            GetRoles();
            return View(userModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, VolunteerViewModel volunteerModel)
        {
            try
            {
                volunteerModel.Id = id;
                EntityResult result = await _volunteerService.UpdateVolunteer(volunteerModel);
                if (result.Succeeded)
                {
                    AddMessageToTempData(CommonResources.SavedSuccessfullyMessage
                        , BusinessSolutions.MVCCommon.MessageType.Success);
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
                AddMessageToView(CommonResources.UnExpectedError
                    , BusinessSolutions.MVCCommon.MessageType.Error);
            }

            GetRoles();
            return View(volunteerModel);
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