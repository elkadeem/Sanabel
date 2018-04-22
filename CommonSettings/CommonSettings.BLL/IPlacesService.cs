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

        EntityResult AddCountry(CountryViewModel countryModel);

        EntityResult UpdateCountry(CountryViewModel countryModel);

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

        EntityResult AddCity(CityViewModel cityModel);

        EntityResult UpdateCity(CityViewModel cityModel);

        bool DeleteCity(int cityId);
        #endregion

        #region District
        PagedEntity<DistrictViewModel> GetDistricts(SearchDistrictViewModel searchDistrictModel);

        List<DistrictViewModel> GetDistrictsByCityId(int cityId);

        DistrictViewModel GetDistrictById(int districtId);

        EntityResult AddDistrict(DistrictViewModel districtModel);

        EntityResult UpdateDistrict(DistrictViewModel districtModel);

        bool DeleteDistrict(int districtId);
        #endregion

    }
}
