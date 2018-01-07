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
    public class CitiesController : BaseController
    {
        private IPlacesService _placesService;
        public CitiesController(IPlacesService placesService, NLog.ILogger logger) : base(logger)
        {
            _placesService = placesService;
        }

        // GET: Settings/Cities
        public ActionResult Index(SearchCityViewModel searchCityModel)
        {
            var result = _placesService.GetCities(searchCityModel);
            searchCityModel.Items = new StaticPagedList<CityViewModel>(result.Items
                , searchCityModel.PageIndex + 1, searchCityModel.PageSize, result.TotalCount);
            return View(searchCityModel);
        }

        public JsonResult GetCitiesByRegionId(int regionId)
        {
            var regions = _placesService.GetCitiesByRegionId(regionId);
            return Json(regions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCitiesByCountryId(int countryId)
        {
            var regions = _placesService.GetCitiesByCountryId(countryId);
            return Json(regions, JsonRequestBehavior.AllowGet);
        }

        // GET: Settings/Cities/Details/5
        [MustBeGreateThanZeroFilter("id", ActionName = "Index")]
        public ActionResult Details(int id)
        {
            var city = _placesService.GetCityById(id);
            if (city == null)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }

            return View(city);
        }

        // GET: Settings/Cities/Create
        public ActionResult Create()
        {
            var newRegion = new CityViewModel();
            return View(newRegion);
        }

        // POST: Settings/Cities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CityViewModel cityModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entityResult = _placesService.AddCity(cityModel);

                    if (entityResult.Succeeded)
                    {
                        AddMessageToTempData(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Success);
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

            return View(cityModel);
        }

        // GET: Settings/Cities/Edit/5
        [MustBeGreateThanZeroFilter("id", ActionName = "Index")]
        public ActionResult Edit(int id)
        {
            var city = _placesService.GetCityById(id);
            if (city == null)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }

            city.CountryId = city.Region.CountryId;
            return View(city);
        }

        // POST: Settings/Cities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CityViewModel cityModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cityModel.CityId = id;
                    var entityResult = _placesService.UpdateCity(cityModel);
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

            return View(cityModel);
        }

        // POST: Settings/Cities/Delete/5
        [HttpPost]
        [MustBeGreateThanZeroFilter("id", ActionName = "Index")]
        public ActionResult Delete(int id, string returnUrl)
        {
            try
            {
                var item = _placesService.GetCityById(id);
                if (item == null)
                {
                    AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                }
                else
                {
                    var result = _placesService.DeleteCity(id);
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
                    ModelState.AddModelError("", CommonSettingsResources.CityDuplicated);
            }
        }
    }
}
