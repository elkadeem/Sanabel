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

        EntityResult AddCountry(CountryViewModel country);

        EntityResult UpdateCountry(CountryViewModel country);

        bool DeleteCountry(int countryId);
        #endregion

        #region Region
        PagedEntity<RegionViewModel> GetRegions(SearchRegionViewModel model);

        List<RegionViewModel> GetRegionsByCountryId(int CountryId);

        RegionViewModel GetRegionById(int regionId);

        EntityResult AddRegion(RegionViewModel regionModel);

        EntityResult UpdateRegion(RegionViewModel regionModel);

        bool DeleteRegion(int regionId);
        #endregion

        #region Cities
        PagedEntity<CityViewModel> GetCities(SearchCityViewModel searchCityModel);

        List<CityViewModel> GetCitiesByRegionId(int regionId);

        List<CityViewModel> GetCitiesByCountryId(int cityId);

        CityViewModel GetCityById(int cityId);

        EntityResult AddCity(CityViewModel city);

        EntityResult UpdateCity(CityViewModel city);

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
