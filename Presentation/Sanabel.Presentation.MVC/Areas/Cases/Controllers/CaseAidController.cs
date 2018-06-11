using BusinessSolutions.Common.Infra.Log;
using BusinessSolutions.MVCCommon.Controllers;
using Sanabel.Cases.App.Model;
using Sanabel.Cases.App.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sanabel.Presentation.MVC.Cases.Controllers
{
    public class CaseAidController : BaseController
    {
        private readonly Sanabel.Cases.App.ICasesService _caseService;
        public CaseAidController(Sanabel.Cases.App.ICasesService caseService
            , ILogger logger) : base(logger)
        {
            _caseService = caseService ?? throw new ArgumentNullException("caseService");
        }

        public async Task<ActionResult> Index(Guid caseId)
        {
            var caseAids = await _caseService.GetCaseAids(caseId);
            return View(caseAids);
        }

        public async Task<ActionResult> Create(Guid caseId)
        {
            var currentCase = await _caseService.GetCase(caseId);
            //ToDo: Check For Null
            return View(new CaseAidViewModel
            {
                AidDate = DateTime.Today,
                CaseId = currentCase.CaseId,
                Case = currentCase,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Guid caseId
            , CaseAidViewModel caseAidViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _caseService.AddCaseAid(caseId, caseAidViewModel);
                    if (result.Succeeded)
                        return RedirectToAction("Index", new { CaseId = caseId });
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            var currentCase = await _caseService.GetCase(caseId);
            caseAidViewModel.Case = currentCase;
            return View(caseAidViewModel);
        }

        public async Task<ActionResult> Detail(Guid id)
        {
            CaseAidViewModel caseAid = await _caseService.GetCaseAidById(id);
            if (caseAid == null)
            {
                AddMessageToTempData(CasesResource.ItemIsNotExist, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index", "Management", new { area = "Cases", caseType = CaseStatus.Approved });
            }

            return View(caseAid);
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            CaseAidViewModel caseAid = await _caseService.GetCaseAidById(id);
            if (caseAid == null)
            {
                AddMessageToTempData(CasesResource.ItemIsNotExist, BusinessSolutions.MVCCommon.MessageType.Error);
                return RedirectToAction("Index", "Management", new { area = "Cases", caseType = CaseStatus.Approved });
            }

            return View(caseAid);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, CaseAidViewModel caseAidViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    caseAidViewModel.AidId = id;
                    var result = await _caseService.UpdateCaseAid(caseAidViewModel);
                    if (result.Succeeded)
                        return RedirectToAction("Index", new { CaseId = caseAidViewModel.CaseId });
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            var currentCase = await _caseService.GetCase(caseAidViewModel.CaseId);
            caseAidViewModel.Case = currentCase;
            return View(caseAidViewModel);
        }
    }
}