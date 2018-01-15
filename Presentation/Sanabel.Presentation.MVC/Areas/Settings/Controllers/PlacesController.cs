using CommonSettings.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sanabel.Presentation.MVC.Areas.Settings.Controllers
{    
    [Authorize]
    [RoutePrefix("api/Settings/Places")]
    public class PlacesController : ApiController
    {
        private IPlacesService _placesService;
        public PlacesController(IPlacesService placesService)
        {
            _placesService = placesService;
        }
        
        [Route("")]
        public HttpResponseMessage GetAllCountries()
        {
            var countries = _placesService.GetAllCountries().OrderBy(c => c.CountryName);
            return Request.CreateResponse(HttpStatusCode.OK, countries);
        }
    }
}
