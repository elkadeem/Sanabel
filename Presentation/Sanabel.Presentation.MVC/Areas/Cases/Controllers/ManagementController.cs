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

        public ManagementController(Sanabel.Cases.App.ICasesService caseService
            , ILogger logger) : base(logger)
        {
            _caseService = caseService ?? throw new ArgumentNullException("caseService");
        }
        
        public async Task<ActionResult> Index(SearchCaseViewModel searchModel)
        {
            var result = await _caseService.GetCases(searchModel);
            searchModel.Items = new StaticPagedList<CaseViewModel>(result.Items
                , searchModel.PageIndex + 1, searchModel.PageSize, result.TotalCount);
            return View(searchModel);
        }

        public async Task<ActionResult> NewCase(SearchCaseViewModel searchModel)
        {
            searchModel.CaseStatus = CaseStatus.New;
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

        public ActionResult Create()
        {
            CaseViewModel newCase = new CaseViewModel();
            return View(newCase);
        }

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

        public async Task<ActionResult> Review(Guid id)
        {
            var item = await _caseService.GetCase(id);
            if (item == null || item.CaseStatus != CaseStatus.New)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }

            return View(new CaseActionViewModel { Case = item });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Review(Guid id, CaseActionViewModel caseModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    caseModel.CaseId = id;
                    EntityResult result = await _caseService.UpdateCaseStatus(caseModel);

                    if (result.Succeeded)
                    {
                        AddMessageToTempData(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Success);
                        return RedirectToAction("Index");
                    }

                    AddErrors(result);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                AddMessageToView(CommonResources.UnExpectedError, BusinessSolutions.MVCCommon.MessageType.Error);
            }
            caseModel.Case = await _caseService.GetCase(caseModel.CaseId);
            return View(caseModel);
        }

        public async Task<ActionResult> Suspend(Guid id)
        {
            var currentCase = await _caseService.GetCase(id);
            if (currentCase == null || currentCase.CaseStatus != CaseStatus.Approved)
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index", new { caseStatus = "Approved" });
            }

            return View(new ActivateCaseViewModel { Case = currentCase });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Suspend(Guid id, ActivateCaseViewModel caseModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    caseModel.CaseId = id;
                    CaseActionViewModel caseActionViewModel = new CaseActionViewModel
                    {
                        CaseId = caseModel.CaseId,
                        Status = CaseStatus.Suspended,
                        StartApplyDate = caseModel.StartApplyDate,
                        Comment = caseModel.Comment,
                        CaseActionDate = DateTime.Now,
                    };

                    EntityResult result = await _caseService.UpdateCaseStatus(caseActionViewModel);

                    if (result.Succeeded)
                    {
                        AddMessageToTempData(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Success);
                        return RedirectToAction("Index");
                    }

                    AddErrors(result);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                AddMessageToView(CommonResources.UnExpectedError, BusinessSolutions.MVCCommon.MessageType.Error);
            }
            caseModel.Case = await _caseService.GetCase(caseModel.CaseId);
            return View(caseModel);
        }

        public async Task<ActionResult> Activate(Guid id)
        {
            var currentCase = await _caseService.GetCase(id);
            if (currentCase == null || (currentCase.CaseStatus != CaseStatus.Suspended
                && currentCase.CaseStatus != CaseStatus.Rejected))
            {
                AddMessageToTempData(CommonResources.NoDataFound, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index");
            }

            return View(new ActivateCaseViewModel { Case = currentCase });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Activate(Guid id, ActivateCaseViewModel caseModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    caseModel.CaseId = id;
                    CaseActionViewModel caseActionViewModel = new CaseActionViewModel
                    {
                        CaseId = caseModel.CaseId,
                        Status = CaseStatus.Approved,
                        StartApplyDate = caseModel.StartApplyDate,
                        Comment = caseModel.Comment,
                        CaseActionDate = DateTime.Now,
                    };

                    EntityResult result = await _caseService.UpdateCaseStatus(caseActionViewModel);

                    if (result.Succeeded)
                    {
                        AddMessageToTempData(CommonResources.SavedSuccessfullyMessage, BusinessSolutions.MVCCommon.MessageType.Success);
                        return RedirectToAction("Index");
                    }

                    AddErrors(result);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                AddMessageToView(CommonResources.UnExpectedError, BusinessSolutions.MVCCommon.MessageType.Error);
            }
            caseModel.Case = await _caseService.GetCase(caseModel.CaseId);
            return View(caseModel);
        }

    }
}
