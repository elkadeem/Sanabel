using BusinessSolutions.Common.Core;
using CommonSettings.Domain.Entities;
using CommonSettings.Domain.UnitOfWork;
using CommonSettings.ViewModels;
using Grace.DependencyInjection.Attributes;
using NLog;
using System;
using System.Collections.Generic;
using CommonSettings.BLL.Mappers;
using System.Linq;

namespace CommonSettings.BLL
{
    [ExportByInterfaces()]
    public class PlacesService : IPlacesService
    {
        private ICommonSettingsUnitOfWork _unitOfWork;
        private ILogger _logger;

        public PlacesService(ICommonSettingsUnitOfWork unitOfWork, ILogger log)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            if (log == null)
                throw new ArgumentNullException("log");

            _unitOfWork = unitOfWork;
            _logger = log;
        }

        #region Country
        public PagedEntity<CountryViewModel> GetCountries(SearchCountryViewModel searchCountryModel)
        {
            if (searchCountryModel == null)
                searchCountryModel = new SearchCountryViewModel() { PageSize = 10 };

            var result = _unitOfWork.CountryRepository.GetCountries(searchCountryModel.CountryName, searchCountryModel.CountryCode, searchCountryModel.PageIndex
                , searchCountryModel.PageSize);

            return new PagedEntity<CountryViewModel>(result.Items.Select(c => c.ToCountryModel()).ToList().AsReadOnly(), result.TotalCount);
        }

        public CountryViewModel GetCountryById(int countryId)
        {
            return _unitOfWork.CountryRepository.GetByID(countryId).ToCountryModel();
        }

        public List<CountryViewModel> GetAllCountries()
        {
            return _unitOfWork.CountryRepository.GetAll().Select(c => c.ToCountryModel()).ToList();
        }

        public CountryViewModel SaveCountry(CountryViewModel countryModel)
        {
            try
            {

                if (countryModel == null)
                    throw new ArgumentNullException("countryModel");

                var country = countryModel.ToCountry();
                var currentCity = _unitOfWork.CountryRepository.GetByID(countryModel.CountryId);
                if (currentCity == null)
                    _unitOfWork.CountryRepository.Add(country);
                else
                    _unitOfWork.CountryRepository.Update(country);

                _unitOfWork.Save();
                countryModel.CountryId = _unitOfWork.CountryRepository.GetPrimaryKey(country);
                return countryModel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw ex;
            }
        }

        public bool DeleteCountry(int countryId)
        {
            try
            {
                _unitOfWork.CountryRepository.Remove(countryId);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return false;
            }
        }
        #endregion 

        public bool DeleteCity(int cityId)
        {
            try
            {
                _unitOfWork.CityRepository.Remove(cityId);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw ex;
            }
        }



        public bool DeleteDistrict(int districtId)
        {
            try
            {
                _unitOfWork.DistrictRepository.Remove(districtId);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return false;
            }
        }

        public bool DeleteRegion(int regionId)
        {
            try
            {
                _unitOfWork.RegionRepository.Remove(regionId);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return false;
            }
        }



        public PagedEntity<City> GetCities(int countryId, int regionId, string cityName, string code, int pageIndex, int pageSize)
        {
            return _unitOfWork.CityRepository.GetCities(countryId, regionId, cityName, code, pageIndex, pageSize);
        }

        public List<City> GetCitiesByCountryId(int CountryId)
        {
            return _unitOfWork.CityRepository.GetCitiesByCountryId(CountryId);
        }

        public List<City> GetCitiesByRegionId(int regionId)
        {
            return _unitOfWork.CityRepository.GetCitiesByRegionId(regionId);
        }

        public City GetCityById(int cityId)
        {
            return _unitOfWork.CityRepository.GetByID(cityId);
        }



        public District GetDistrictById(int districtId)
        {
            return _unitOfWork.DistrictRepository.GetByID(districtId);
        }

        public PagedEntity<District> GetDistricts(int regionId, int cityId, string districtName, string code
            , int pageIndex, int pageSize)
        {
            return _unitOfWork.DistrictRepository.GetDistricts(regionId, cityId, districtName
                , code, pageIndex, pageSize);
        }

        public List<District> GetDistrictsByCityId(int cityId)
        {
            return _unitOfWork.DistrictRepository.GetDistrictsByCityId(cityId);
        }

        public Region GetRegionById(int regionId)
        {
            return _unitOfWork.RegionRepository.GetByID(regionId);
        }

        public PagedEntity<Region> GetRegions(int countryId, string regionName, string code, int pageIndex, int pageSize)
        {
            return _unitOfWork.RegionRepository.GetRegions(countryId, regionName, code, pageIndex, pageSize);
        }

        public List<Region> GetRegionsByCountryId(int CountryId)
        {
            return _unitOfWork.RegionRepository.GetRegionsByCountryId(CountryId);
        }

        public City SaveCity(City city)
        {
            try
            {
                var currentCity = _unitOfWork.CityRepository.GetByID(city.Id);
                if (currentCity == null)
                    _unitOfWork.CityRepository.Add(city);
                else
                    _unitOfWork.CityRepository.Update(city);

                _unitOfWork.Save();
                city.Id = _unitOfWork.CityRepository.GetPrimaryKey(city);
                return city;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw ex;
            }
        }



        public District SaveDistrict(District district)
        {
            try
            {
                var currentCity = _unitOfWork.DistrictRepository.GetByID(district.Id);
                if (currentCity == null)
                    _unitOfWork.DistrictRepository.Add(district);
                else
                    _unitOfWork.DistrictRepository.Update(district);

                _unitOfWork.Save();
                district.Id = _unitOfWork.DistrictRepository.GetPrimaryKey(district);
                return district;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw ex;
            }
        }

        public Region SaveRegion(Region region)
        {
            try
            {
                var currentCity = _unitOfWork.RegionRepository.GetByID(region.Id);
                if (currentCity == null)
                    _unitOfWork.RegionRepository.Add(region);
                else
                    _unitOfWork.RegionRepository.Update(region);

                _unitOfWork.Save();
                region.Id = _unitOfWork.RegionRepository.GetPrimaryKey(region);
                return region;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw ex;
            }
        }
    }
}
