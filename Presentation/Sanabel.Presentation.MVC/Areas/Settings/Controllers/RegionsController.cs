using BusinessSolutions.Common.Infra.Validation;
using BusinessSolutions.Localization;
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
    public class RegionsController : BaseController
    {
        private IPlacesService _placesService;
        public RegionsController(IPlacesService placesService, NLog.ILogger logger) : base(logger)
        {
            _placesService = placesService;
        }

        // GET: Settings/Regions
        public ActionResult Index(SearchRegionViewModel model)
        {
            GetCountries();
            if (model.CountryId == 0)
                model.CountryId = ViewBag.Countries[0].CountryId;

            var result = _placesService.GetRegions(model);
            model.Items = new StaticPagedList<RegionViewModel>(result.Items
                , model.PageIndex + 1, model.PageSize, result.TotalCount);
            return View(model);
        }

        // GET: Settings/Regions/Details/5
        public ActionResult Details(int id)
        {
            var region = _placesService.GetRegionById(id);
            if (region == null)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }

            return View(region);
        }

        // GET: Settings/Regions/Create
        public ActionResult Create()
        {
            GetCountries();
            var newRegion = new RegionViewModel();
            return View(newRegion);
        }

        // POST: Settings/Regions/Create
        [HttpPost]
        public ActionResult Create(RegionViewModel region)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entityResult = _placesService.AddRegion(region);

                    if (entityResult.Succeeded)
                    {
                        AddMessageToTempData(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Success);
                        return RedirectToAction("Index", new { CountryId = region.CountryId });
                    }

                    AddValidationErrors(entityResult);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                AddMessageToView(CommonResources.UnExpectedError, BusinessSolutions.MVCCommon.MessageType.Error);

            }

            GetCountries();
            return View(region);
        }

        private void GetCountries()
        {
            var countries = _placesService.GetAllCountries();
            ViewBag.Countries = countries;
        }

        private void AddValidationErrors(EntityResult result)
        {
            foreach (var error in result.ValidationErrors)
            {
                if (error.ValidationErrorType == ValidationErrorTypes.DuplicatedValue)
                    ModelState.AddModelError("", CommonSettingsResources.RegionDublicated);
            }
        }

        // GET: Settings/Regions/Edit/5
        public ActionResult Edit(int id)
        {
            var region = _placesService.GetRegionById(id);
            if (region == null)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }

            GetCountries();
            return View(region);
        }

        // POST: Settings/Regions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RegionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.RegionId = id;
                    var entityResult = _placesService.UpdateRegion(model);
                    if (entityResult.Succeeded)
                    {
                        AddMessageToTempData(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Success);
                        return RedirectToAction("Index", new { CountryId = model.CountryId });
                    }

                    AddValidationErrors(entityResult);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                AddMessageToView(CommonResources.UnExpectedError, BusinessSolutions.MVCCommon.MessageType.Error);
            }

            GetCountries();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id, string returnUrl)
        {
            try
            {
                var item = _placesService.GetRegionById(id);
                if (item == null)
                {
                    AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                }
                else
                {
                    var result = _placesService.DeleteRegion(id);
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
    }
}
