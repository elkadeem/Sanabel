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
using BusinessSolutions.Common.Core.Specifications;
using BusinessSolutions.Common.Infra.Validation;

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
                searchCountryModel = new SearchCountryViewModel();

            ExpressionSpecification<Country> specification
                = new ExpressionSpecification<Country>(c =>
                (string.IsNullOrEmpty(searchCountryModel.CountryName)
                    || c.Name.Contains(searchCountryModel.CountryName)
                    || c.NameEn.Contains(searchCountryModel.CountryName))
                    && (string.IsNullOrEmpty(searchCountryModel.CountryCode)
                        || c.Code.Contains(searchCountryModel.CountryCode)));

            var result = _unitOfWork.CountryRepository.Find(specification, searchCountryModel.PageIndex, searchCountryModel.PageSize);
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

        public EntityResult AddCountry(CountryViewModel countryModel)
        {
            try
            {
                if (countryModel == null)
                    throw new ArgumentNullException("countryModel");

                var country = countryModel.ToCountry();

                var result = ValidateCountry(country);
                if (result != null && result.Count > 0)
                    return EntityResult.Failed(result.ToArray());

                _unitOfWork.CountryRepository.Add(country);
                _unitOfWork.Save();
                countryModel.CountryId = country.Id;
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw ex;
            }
        }

        public EntityResult UpdateCountry(CountryViewModel countryModel)
        {
            try
            {
                if (countryModel == null)
                    throw new ArgumentNullException("countryModel");

                var currentCountry = _unitOfWork.CountryRepository.GetByID(countryModel.CountryId);
                if (currentCountry == null)
                    throw new ArgumentException("countryModel");

                var country = countryModel.ToCountry();
                var result = ValidateCountry(country);
                if (result != null && result.Count > 0)
                    return EntityResult.Failed(result.ToArray());

                _unitOfWork.CountryRepository.Update(country);
                _unitOfWork.Save();
                return EntityResult.Success;
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

        private List<ValidationError> ValidateCountry(Country country)
        {
            List<ValidationError> errors = new List<ValidationError>();
            var isNameIsNotEmpty = new ExpressionSpecification<Country>(c => !string.IsNullOrEmpty(c.Name));
            if (!isNameIsNotEmpty.IsSatisfiedBy(country))
                throw new ArgumentNullException("Name");

            var isCodeIsNotEmpty = new ExpressionSpecification<Country>(c => !string.IsNullOrEmpty(c.Code));
            if (!isCodeIsNotEmpty.IsSatisfiedBy(country))
                throw new ArgumentNullException("Name");

            var isNameOrCodeExistSpecification = new ExpressionSpecification<Country>(c => c.Id != country.Id
              && (c.Name.ToLower() == country.Name.ToLower() || c.Code.ToLower() == country.Code.ToLower()));
            if (_unitOfWork.CountryRepository.Find(isNameOrCodeExistSpecification).Any())
                errors.Add(new ValidationError("Country Name or Code already exist", ValidationErrorTypes.DuplicatedValue));

            return errors;
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
