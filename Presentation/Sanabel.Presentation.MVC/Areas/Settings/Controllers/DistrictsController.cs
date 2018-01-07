using BusinessSolutions.Common.Infra.Validation;
using BusinessSolutions.Localization;
using BusinessSolutions.MVCCommon;
using BusinessSolutions.MVCCommon.Controllers;
using CommonSettings.BLL;
using CommonSettings.Localization;
using CommonSettings.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sanabel.Presentation.MVC.Areas.Settings.Controllers
{
    public class DistrictsController : BaseController
    {
        private IPlacesService _placesService;
        public DistrictsController(IPlacesService placesService, NLog.ILogger logger) : base(logger)
        {
            _placesService = placesService;
        }

        // GET: Settings/Districts
        public ActionResult Index(SearchDistrictViewModel searchDistrictModel)
        {
            var result = _placesService.GetDistricts(searchDistrictModel);
            searchDistrictModel.Items = new StaticPagedList<DistrictViewModel>(result.Items
                , searchDistrictModel.PageIndex + 1, searchDistrictModel.PageSize, result.TotalCount);
            return View(searchDistrictModel);
        }

        // GET: Settings/Districts/Details/5
        [MustBeGreateThanZeroFilter("id", ActionName = "Index")]
        public ActionResult Details(int id)
        {
            var city = _placesService.GetDistrictById(id);
            if (city == null)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }

            return View(city);
        }

        // GET: Settings/Districts/Create
        public ActionResult Create()
        {
            var newDistrict = new DistrictViewModel();
            return View(newDistrict);
        }

        // POST: Settings/Districts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DistrictViewModel districtModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entityResult = _placesService.AddDistrict(districtModel);
                    if (entityResult.Succeeded)
                    {
                        AddMessageToTempData(CommonResources.SavedSuccessfullyMessage
                            , BusinessSolutions.MVCCommon.MessageType.Success);
                        return RedirectToAction("Index");
                    }

                    AddValidationErrors(entityResult);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                AddMessageToView(CommonResources.UnExpectedError, BusinessSolutions.MVCCommon.MessageType.Error);

            }

            return View(districtModel);
        }

        // GET: Settings/Districts/Edit/5
        [MustBeGreateThanZeroFilter("id", ActionName = "Index")]
        public ActionResult Edit(int id)
        {
            var city = _placesService.GetDistrictById(id);
            if (city == null)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }
            
            return View(city);
        }

        // POST: Settings/Districts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DistrictViewModel districtModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    districtModel.DistrictId = id;
                    var entityResult = _placesService.UpdateDistrict(districtModel);
                    if (entityResult.Succeeded)
                    {
                        AddMessageToTempData(CommonResources.SavedSuccessfullyMessage
                            , BusinessSolutions.MVCCommon.MessageType.Success);
                        return RedirectToAction("Index");
                    }

                    AddValidationErrors(entityResult);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                AddMessageToView(CommonResources.UnExpectedError, BusinessSolutions.MVCCommon.MessageType.Error);
            }

            return View(districtModel);
        }

        // POST: Settings/Districts/Delete/5
        [HttpPost]
        [MustBeGreateThanZeroFilter("id", ActionName = "Index")]
        public ActionResult Delete(int id, string returnUrl)
        {
            try
            {
                var item = _placesService.GetDistrictById(id);
                if (item == null)
                {
                    AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                }
                else
                {
                    var result = _placesService.DeleteDistrict(id);
                    if (result)
                        AddMessageToTempData(CommonResources.DeleteSuccessfully, BusinessSolutions.MVCCommon.MessageType.Success);
                    else
                        AddMessageToTempData(CommonResources.DeleteError, BusinessSolutions.MVCCommon.MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex);
                AddMessageToView(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Error);
            }

            if (string.IsNullOrEmpty(returnUrl) && !Url.IsLocalUrl(returnUrl))
                return RedirectToAction("Index");
            else
                return RedirectToLocal(returnUrl);
        }

        private void AddValidationErrors(EntityResult result)
        {
            foreach (var error in result.ValidationErrors)
            {
                if (error.ValidationErrorType == ValidationErrorTypes.DuplicatedValue)
                    ModelState.AddModelError("", CommonSettingsResources.DistrictDuplicateMessage);
            }
        }
    }
}
