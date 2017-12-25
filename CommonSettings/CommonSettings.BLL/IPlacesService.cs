using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Infra.Validation;
using CommonSettings.Domain.Entities;
using CommonSettings.ViewModels;
using System.Collections.Generic;

namespace CommonSettings.BLL
{
    public interface IPlacesService
    {
        #region Countries
        PagedEntity<CountryViewModel> GetCountries(SearchCountryViewModel searchCountryModel);

        List<CountryViewModel> GetAllCountries();

        CountryViewModel GetCountryById(int countryId);

        EntityResult SaveCountry(CountryViewModel country);

        bool DeleteCountry(int countryId);
        #endregion

        #region Region
        PagedEntity<Region> GetRegions(int countryId, string regionName, string code, int pageIndex, int pageSize);

        List<Region> GetRegionsByCountryId(int CountryId);

        Region GetRegionById(int regionId);

        Region SaveRegion(Region region);

        bool DeleteRegion(int regionId);
        #endregion

        #region Cities
        PagedEntity<City> GetCities(int countryId, int regionId, string cityName
            , string code, int pageIndex, int pageSize);

        List<City> GetCitiesByRegionId(int regionId);

        List<City> GetCitiesByCountryId(int CountryId);

        City GetCityById(int cityId);

        City SaveCity(City city);

        bool DeleteCity(int cityId);
        #endregion

        #region District
        PagedEntity<District> GetDistricts(int regionId, int cityId, string districtName
            , string code, int pageIndex, int pageSize);

        List<District> GetDistrictsByCityId(int cityId);

        District GetDistrictById(int districtId);

        District SaveDistrict(District district);

        bool DeleteDistrict(int districtId);
        #endregion

    }
}
