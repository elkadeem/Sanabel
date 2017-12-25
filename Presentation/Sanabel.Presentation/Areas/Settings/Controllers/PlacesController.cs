using BusinessSolutions.Common.Core;
using BusinessSolutions.Localization;
using BusinessSolutions.MVCCommon.Controllers;
using CommonSettings.BLL;
using CommonSettings.ViewModels;
using System;
using System.Web.Mvc;

namespace Sanabel.Presentation.Areas.Settings.Controllers
{
    public class PlacesController : BaseController
    {
        private IPlacesService _placesService;

        public PlacesController(IPlacesService placesService, NLog.ILogger logger) : base(logger)
        {
            _placesService = placesService;
        }

        // GET: Settings/Places
        public ActionResult Index(SearchCountryViewModel searchModel)
        {
            if (searchModel == null)
                searchModel = new SearchCountryViewModel { PageSize = 10 };

            PagedEntity<CountryViewModel> result = _placesService.GetCountries(searchModel);
            searchModel.Items = new PagedList.StaticPagedList<CountryViewModel>(result.Items
                , searchModel.PageIndex + 1, searchModel.PageSize, result.TotalCount);
            return View(searchModel);
        }

        // GET: Settings/Places/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }

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

                var result = _placesService.SaveCountry(model);
                if (result.Succeeded)
                {
                    AddMessageToTempData(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.ValidationErrors)
                    {
                        ModelState.AddModelError(error.Property, error.Message);
                    }

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex);
                AddMessageToView(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Error);
                return View(model);
            }
        }

        // GET: Settings/Places/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }

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

                var result = _placesService.SaveCountry(model);
                if (result.Succeeded)
                {
                    AddMessageToTempData(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.ValidationErrors)
                    {
                        ModelState.AddModelError(error.Property, error.Message);
                    }

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex);
                AddMessageToView(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Error);
                return View(model);
            }
        }

        // POST: Settings/Places/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                }
                else
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
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex);
                AddMessageToView(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Error);
            }

            return RedirectToAction("Index");
        }
    }
}
