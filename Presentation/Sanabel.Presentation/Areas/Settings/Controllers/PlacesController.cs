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
            return View();
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
                AddMessageToTempData(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Success);
                return RedirectToAction("Index");
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
            return View();
        }

        // POST: Settings/Places/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Settings/Places/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Settings/Places/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
