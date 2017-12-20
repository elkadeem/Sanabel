using BusinessSolutions.Common.Core;
using CommonSettings.Domain.Entities;
using CommonSettings.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.Domain.Services
{
    public interface IPlacesService
    {
        #region Countries
        PagedEntity<Country> GetCountries(string countryName, string code, int pageIndex, int pageSize);

        List<Country> GetAllCountries();

        Country SaveCountry(Country country);

        bool DeleteCountry(int countryId);
        #endregion

        #region Region
        PagedEntity<Region> GetRegions(int countryId, string regionName, string code, int pageIndex, int pageSize);

        List<Region> GetRegionsByCountryId(int CountryId);

        Region SaveRegion(Region region);

        bool DeleteRegion(int regionId);
        #endregion

        #region Cities
        PagedEntity<City> GetCities(int countryId, int regionId, string cityName
            , string code, int pageIndex, int pageSize);

        List<City> GetCitiesByRegionId(int regionId);

        List<City> GetCitiesByCountryId(int CountryId);

        City SaveCity(City city);

        bool DeleteCity(int cityId);
        #endregion

        #region District
        PagedEntity<City> GetDistricts(int regionId, int cityId, string districtName
            , string code, int pageIndex, int pageSize);

        List<District> GetDistrictsByCityId(int cityId);
        
        District SaveDistrict(District district);

        bool DeleteDistrict(int districtId);
        #endregion

    }
}
