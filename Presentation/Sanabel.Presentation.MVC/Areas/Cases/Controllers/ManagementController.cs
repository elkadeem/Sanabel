using BusinessSolutions.Common.Infra.Log;
using BusinessSolutions.Common.Infra.Validation;
using BusinessSolutions.Localization;
using BusinessSolutions.MVCCommon.Controllers;
using PagedList;
using Sanabel.Cases.App.Model;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sanabel.Presentation.MVC.Cases.Controllers
{
    [Authorize]
    public class ManagementController : BaseController
    {
        private readonly Sanabel.Cases.App.ICasesService _caseService;
        public ManagementController(Sanabel.Cases.App.ICasesService caseService, ILogger logger): base(logger)
        {
            _caseService = caseService ?? throw new ArgumentNullException("caseService");
        }
        // GET: Cases/Management
        public async Task<ActionResult> Index(SearchCaseViewModel searchModel)
        {
            var result = await _caseService.GetCases(searchModel);
            searchModel.Items = new StaticPagedList<CaseViewModel>(result.Items
                , searchModel.PageIndex + 1, searchModel.PageSize, result.TotalCount);
            return View(searchModel);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var item = await _caseService.GetCase(id);
            return View(item);
        }

        // GET: Cases/Management/Create
        public ActionResult Create()
        {
            CaseViewModel newCase = new CaseViewModel();
            return View(newCase);
        }

        // POST: Cases/Management/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CaseViewModel caseModel)
        {
            try
            {
                EntityResult result = await _caseService.AddCase(caseModel);
                if (result.Succeeded)
                {
                    AddMessageToTempData(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Success);
                    return RedirectToAction("Index");
                }

                AddErrors(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                AddMessageToView(CommonResources.UnExpectedError, BusinessSolutions.MVCCommon.MessageType.Error);
            }

            return View(caseModel);
        }

        private void AddErrors(EntityResult result)
        {
            foreach (var error in result.ValidationErrors)
            {                
                    ModelState.AddModelError("", error.Message);
            }
        }

        // GET: Cases/Management/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var item = await _caseService.GetCase(id);
            if (item == null)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // POST: Cases/Management/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, CaseViewModel caseModel)
        {
            try
            {
                caseModel.CaseId = id;
                EntityResult result = await _caseService.UpdateCase(caseModel);
                if (result.Succeeded)
                {
                    AddMessageToTempData(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Success);
                    return RedirectToAction("Index");
                }

                AddErrors(result);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                AddMessageToView(CommonResources.UnExpectedError, BusinessSolutions.MVCCommon.MessageType.Error);
            }

            return View(caseModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, string returnUrl)
        {
            try
            {
                EntityResult result = await _caseService.DeleteCase(id);
                if (result.Succeeded)
                {
                    AddMessageToTempData(CommonResources.DeleteSuccessfully, BusinessSolutions.MVCCommon.MessageType.Success);
                    return RedirectToAction("Index");
                }

                AddErrors(result);
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex);
                AddMessageToView(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Error);
            }

            return RedirectToLocal(returnUrl, RedirectToAction("Index"));            
        }
    }
}
