using CommonSettings.BLL;
using CommonSettings.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sanabel.Presentation.MVC.Settings.Controllers
{
    [Authorize]
    [RoutePrefix("api/Settings/Places")]
    public class PlacesController : ApiController
    {
        private readonly IPlacesService _placesService;
        public PlacesController(IPlacesService placesService)
        {
            _placesService = placesService;
        }

        [Route("Countries")]
        public HttpResponseMessage GetAllCountries()
        {
            var countries = _placesService.GetAllCountries().OrderBy(c => c.CountryName);
            return Request.CreateResponse(HttpStatusCode.OK, countries);
        }

        [Route("CountryRegions")]
        public HttpResponseMessage GetRegionByCountryId(int? countryId)
        {
            if (countryId == null || countryId <= 0)
                return Request.CreateResponse(HttpStatusCode.OK, new List<RegionViewModel>());

            var regions = _placesService.GetRegionsByCountryId(countryId.Value).OrderBy(c => c.RegionName);
            return Request.CreateResponse(HttpStatusCode.OK, regions);
        }

        [Route("RegionCities")]
        public HttpResponseMessage GetCitiesByRegionId(int? regionId)
        {
            if (regionId == null || regionId <= 0)
                return Request.CreateResponse(HttpStatusCode.OK, new List<CityViewModel>());

            var cities = _placesService.GetCitiesByRegionId(regionId.Value).OrderBy(c => c.CityName);
            return Request.CreateResponse(HttpStatusCode.OK, cities);
        }

        [Route("CountryCities")]
        public HttpResponseMessage GetCitiesByCountryId(int? countryId)
        {
            if (countryId == null || countryId <= 0)
                return Request.CreateResponse(HttpStatusCode.OK, new List<CityViewModel>());

            var cities = _placesService.GetCitiesByCountryId(countryId.Value).OrderBy(c => c.CityName);
            return Request.CreateResponse(HttpStatusCode.OK, cities);
        }


        [Route("CityDistricts")]
        public HttpResponseMessage GetDistrictsByCityId(int? cityId)
        {
            if (cityId == null || cityId <= 0)
                return Request.CreateResponse(HttpStatusCode.OK, new List<DistrictViewModel>());

            var cities = _placesService.GetDistrictsByCityId(cityId.Value).OrderBy(c => c.DistrictName);
            return Request.CreateResponse(HttpStatusCode.OK, cities);
        }

    }
}
