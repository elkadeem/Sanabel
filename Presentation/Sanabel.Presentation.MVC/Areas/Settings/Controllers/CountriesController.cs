using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Infra.Validation;
using BusinessSolutions.Localization;
using BusinessSolutions.MVCCommon.Controllers;
using CommonSettings.BLL;
using CommonSettings.Localization;
using CommonSettings.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Web.Mvc;
using System.Linq;
using BusinessSolutions.MVCCommon;

namespace Sanabel.Presentation.MVC.Areas.Settings.Controllers
{
    public class CountriesController : BaseController
    {
        private IPlacesService _placesService;

        public CountriesController(IPlacesService placesService, NLog.ILogger logger) : base(logger)
        {
            _placesService = placesService;
        }

        // GET: Settings/Places
        public ActionResult Index(SearchCountryViewModel searchModel)
        {
            PagedEntity<CountryViewModel> result = _placesService.GetCountries(searchModel);
            var pagedList = new PagedList.StaticPagedList<CountryViewModel>(result.Items
                , searchModel.PageIndex + 1, searchModel.PageSize, result.TotalCount);

            if (pagedList.Count == 0 && pagedList.TotalItemCount > 0)
            {
                searchModel.PageIndex = pagedList.HasPreviousPage ?
                        pagedList.PageNumber - 2 : 0;
                return RedirectToAction("Index", searchModel);
            }

            searchModel.Items = pagedList;
            return View(searchModel);
        }

        public JsonResult GetAllCountries()
        {
            var countries = _placesService.GetAllCountries().OrderBy(c => c.CountryName);
            return Json(countries, JsonRequestBehavior.AllowGet);
        }

        // GET: Settings/Places/Details/5
        [MustBeGreateThanZeroFilter("id", ActionName = "Index")]
        public ActionResult Details(int id)
        {            
            var item = _placesService.GetCountryById(id);
            if (item == null)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Settings/Places/Create
        public ActionResult Create()
        {
            CountryViewModel model = new CountryViewModel();
            return View(model);
        }

        // POST: Settings/Places/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("InvalidData", CommonResources.InvalidData);
                    return View(model);
                }

                var result = _placesService.AddCountry(model);
                if (result.Succeeded)
                {
                    AddMessageToTempData(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    AddValidationErrors(result);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex);
                AddMessageToView(CommonResources.UnExpectedError, BusinessSolutions.MVCCommon.MessageType.Error);
                return View(model);
            }
        }

        private void AddValidationErrors(EntityResult result)
        {
            foreach (var error in result.ValidationErrors)
            {
                if (error.ValidationErrorType == ValidationErrorTypes.DuplicatedValue)
                    ModelState.AddModelError("", CommonSettingsResources.CountryDuplicatedMessage);
            }
        }

        // GET: Settings/Places/Edit/5
        [MustBeGreateThanZeroFilter("id", ActionName = "Index")]
        public ActionResult Edit(int id)
        {
            var item = _placesService.GetCountryById(id);
            if (item == null)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // POST: Settings/Places/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CountryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("InvalidData", CommonResources.InvalidData);
                    return View(model);
                }

                var item = _placesService.GetCountryById(model.CountryId);
                if (item == null)
                {
                    AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                    return RedirectToAction("Index");
                }

                var result = _placesService.UpdateCountry(model);
                if (result.Succeeded)
                {
                    AddMessageToTempData(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    AddValidationErrors(result);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex);
                AddMessageToView(CommonResources.UnExpectedError, BusinessSolutions.MVCCommon.MessageType.Error);
                return View(model);
            }
        }

        [HttpPost]
        [MustBeGreateThanZeroFilter("id", ActionName = "Index")]
        public ActionResult Delete(int id, string returnUrl)
        {
            try
            {
                var item = _placesService.GetCountryById(id);
                if (item == null)
                {
                    AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                }
                else
                {
                    var result = _placesService.DeleteCountry(id);
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


            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index");
            else
                return RedirectToLocal(returnUrl);
        }

        // POST: Settings/Places/Delete/5
        [HttpPost]
        public ActionResult DeleteAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    return Json(new { IsValid = false, Error = CommonResources.NoDataFound });
                }
                else
                {
                    var item = _placesService.GetCountryById(id);
                    if (item == null)
                    {
                        return Json(new { IsValid = false, Error = CommonResources.NoDataFound });
                        //AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                    }
                    else
                    {
                        var result = _placesService.DeleteCountry(id);
                        if (result)
                            return Json(new { IsValid = true, Error = "" });
                        //AddMessageToTempData(CommonResources.DeleteSuccessfully, BusinessSolutions.MVCCommon.MessageType.Success);
                        else
                            return Json(new { IsValid = false, Error = CommonResources.DeleteError });
                        //AddMessageToTempData(CommonResources.DeleteError, BusinessSolutions.MVCCommon.MessageType.Error);

                    }
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex);
                return Json(new { IsValid = false, Error = CommonResources.UnExpectedError });
                //AddMessageToView(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Error);
            }

            //return RedirectToAction("Index");
        }
    }
}
