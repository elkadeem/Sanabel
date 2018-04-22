using BusinessSolutions.Common.Infra.Log;
using BusinessSolutions.MVCCommon.Controllers;
using Sanabel.Cases.App;
using Sanabel.Cases.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sanabel.Presentation.MVC.Areas.Cases.Controllers
{
    [Authorize]
    public class CaseReserchController : BaseController
    {
        private readonly CaseResearchService _caseResearchService;
        public CaseReserchController(CaseResearchService caseResearchService,  ILogger logger) : base(logger)
        {
            _caseResearchService = caseResearchService;
        }
        
        public ActionResult Index(SearchCaseReserchViewModel model)
        {
            return View(model);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CaseReserchViewModel model)
        {
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CaseReserchViewModel model)
        {
            return View(model);
        }

        public ActionResult Report(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Report(int id, CaseReserchReportViewModel model)
        {
            return View(model);
        }

        public ActionResult MemberNotes()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MemberNotes(int id, CaseReserchMemberNotesViewModel model)
        {
            return View(model);
        }
    }
}