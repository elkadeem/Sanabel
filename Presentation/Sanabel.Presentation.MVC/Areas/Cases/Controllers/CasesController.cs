using BusinessSolutions.Common.Infra.Log;
using Sanabel.Cases.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sanabel.Presentation.MVC.Areas.Cases.Controllers
{
    [Authorize]
    [RoutePrefix("api/Cases")]
    public class CasesController : ApiController
    {
        private readonly Sanabel.Cases.App.ICasesService _caseService;
        private readonly ILogger _logger;
        public CasesController(Sanabel.Cases.App.ICasesService caseService
            , ILogger logger)
        {
            _caseService = caseService ??
                throw new ArgumentNullException("caseService");
            _logger = logger ?? throw new ArgumentNullException("logger");
        }

        public CasesController()
        {

        }

        [HttpGet]
        [Route("Count")]
        public IHttpActionResult Count(CaseStatus? caseStatus)
        {
            try
            {
                int count = _caseService.GetCasesCount(caseStatus);
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return InternalServerError();
            }
        }
    }
}
