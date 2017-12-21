using CommonSettings.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSettings.Domain.Entities;
using CommonSettings.Domain.UnitOfWork;
using BusinessSolutions.Common.Core;
using NLog;

namespace CommonSettings.BLL
{
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

        public List<Country> GetAllCountries()
        {
            return _unitOfWork.CountryRepository.GetAll();
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

        public PagedEntity<Country> GetCountries(string countryName, string code, int pageIndex, int pageSize)
        {
            return _unitOfWork.CountryRepository.GetCountries(countryName, code, pageIndex, pageSize);
        }

        public PagedEntity<City> GetDistricts(int regionId, int cityId, string districtName, string code
            , int pageIndex, int pageSize)
        {
            return _unitOfWork.DistrictRepository.GetDistricts(regionId, cityId, districtName
                , code, pageIndex, pageSize);
        }

        public List<District> GetDistrictsByCityId(int cityId)
        {
            return _unitOfWork.DistrictRepository.GetDistrictsByCityId(cityId);
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

        public Country SaveCountry(Country country)
        {
            try
            {
                var currentCity = _unitOfWork.CountryRepository.GetByID(country.Id);
                if (currentCity == null)
                    _unitOfWork.CountryRepository.Add(country);
                else
                    _unitOfWork.CountryRepository.Update(country);

                _unitOfWork.Save();
                country.Id = _unitOfWork.CountryRepository.GetPrimaryKey(country);
                return country;
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
