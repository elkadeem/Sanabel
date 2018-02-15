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
            return new PagedEntity<CountryViewModel>(result.Items.Select(c => c.ToCountryModel()), result.TotalCount);
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
                throw;
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
                throw;
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
            var isNameIsNotEmpty = new ExpressionSpecification<Country>(c => !string.IsNullOrEmpty(c.Name))
                .And(new ExpressionSpecification<Country>(c => !string.IsNullOrEmpty(c.Code)));
            if (!isNameIsNotEmpty.IsSatisfiedBy(country))
                throw new ArgumentNullException("Name");

            //var isCodeIsNotEmpty = new ExpressionSpecification<Country>(c => !string.IsNullOrEmpty(c.Code));
            //if (!isCodeIsNotEmpty.IsSatisfiedBy(country))
            //    throw new ArgumentNullException("Code");

            var isNameOrCodeExistSpecification = new ExpressionSpecification<Country>(c => c.Id != country.Id
              && (c.Name.ToLower() == country.Name.ToLower() || c.Code.ToLower() == country.Code.ToLower()));
            if (_unitOfWork.CountryRepository.Find(isNameOrCodeExistSpecification).Any())
                errors.Add(new ValidationError("Country Name or Code already exist", ValidationErrorTypes.DuplicatedValue));

            return errors;
        }
        #endregion

        #region Regions
        public PagedEntity<RegionViewModel> GetRegions(SearchRegionViewModel model)
        {
            if (model == null)
                model = new SearchRegionViewModel();

            var result = _unitOfWork.RegionRepository.GetRegions(model.CountryId, model.RegionName
                , model.RegionCode, model.PageIndex, model.PageSize);

            return new PagedEntity<RegionViewModel>(result.Items.Select(c => c.ToRegionViewModel())
                , result.TotalCount);
        }

        public List<RegionViewModel> GetRegionsByCountryId(int CountryId)
        {
            return _unitOfWork.RegionRepository.GetRegionsByCountryId(CountryId)
                .Select(c => c.ToRegionViewModel()).ToList();
        }

        public RegionViewModel GetRegionById(int regionId)
        {
            return _unitOfWork.RegionRepository.GetByID(regionId).ToRegionViewModel();
        }

        public EntityResult AddRegion(RegionViewModel regionModel)
        {
            try
            {
                if (regionModel == null)
                    throw new ArgumentNullException("regionModel");

                var region = regionModel.ToRegion();
                //Validate Region
                var validationResult = ValidateRegion(region);
                if (validationResult != null && validationResult.Count > 0)
                    return EntityResult.Failed(validationResult.ToArray());
                //Add Region
                _unitOfWork.RegionRepository.Add(region);
                _unitOfWork.Save();
                regionModel.RegionId = region.Id;
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
        }

        public EntityResult UpdateRegion(RegionViewModel regionModel)
        {
            try
            {
                if (regionModel == null)
                    throw new ArgumentNullException("regionModel");
                var currentRegion = _unitOfWork.RegionRepository.GetByID(regionModel.RegionId);
                if (currentRegion == null)
                    throw new ArgumentException("Region is not found.", "regionModel");
                //Validate Region
                var region = regionModel.ToRegion();
                var validationResult = ValidateRegion(region);
                if (validationResult != null && validationResult.Count > 0)
                    return EntityResult.Failed(validationResult.ToArray());
                //Update Region
                _unitOfWork.RegionRepository.Update(region);
                _unitOfWork.Save();
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
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

        private List<ValidationError> ValidateRegion(Region region)
        {

            var isNameIsNotEmpty = new ExpressionSpecification<Region>(c => !string.IsNullOrEmpty(c.Name));
            if (!isNameIsNotEmpty.IsSatisfiedBy(region))
                throw new ArgumentNullException("Name");

            var isCountryExist = new ExpressionSpecification<Country>(c => c.Id == region.CountryId);
            if (_unitOfWork.CountryRepository.Find(isCountryExist).Any() == false)
                throw new ArgumentException("Country is not exist.", "CountryId");

            List<ValidationError> errors = new List<ValidationError>();
            var isRegionExistInCountry = new ExpressionSpecification<Region>(c => c.Id != region.Id
             && c.CountryId == region.CountryId && c.Name.ToLower() == region.Name.ToLower());

            if (_unitOfWork.RegionRepository.Find(isRegionExistInCountry).Any())
                errors.Add(new ValidationError($"Region { region.Name } is already exist.", ValidationErrorTypes.DuplicatedValue));

            return errors;
        }
        #endregion

        #region Cities
        public PagedEntity<CityViewModel> GetCities(SearchCityViewModel searchCityModel)
        {
            if (searchCityModel == null)
                searchCityModel = new SearchCityViewModel();

            var result = _unitOfWork.CityRepository.GetCities(searchCityModel.CountryId
                , searchCityModel.RegionId, searchCityModel.CityName, searchCityModel.CityCode
                , searchCityModel.PageIndex, searchCityModel.PageSize);

            return new PagedEntity<CityViewModel>(result.Items.Select(c => c.ToCityViewModel())
                , result.TotalCount);
        }

        public List<CityViewModel> GetCitiesByCountryId(int cityId)
        {
            return _unitOfWork.CityRepository.GetCitiesByCountryId(cityId)
                .Select(c => c.ToCityViewModel())
                .ToList();
        }

        public CityViewModel GetCityById(int cityId)
        {
            return _unitOfWork.CityRepository.GetByID(cityId).ToCityViewModel();
        }

        public List<CityViewModel> GetCitiesByRegionId(int regionId)
        {
            return _unitOfWork.CityRepository.GetCitiesByRegionId(regionId)
                .Select(c => c.ToCityViewModel())
                .ToList();
        }

        public EntityResult AddCity(CityViewModel cityModel)
        {
            try
            {
                if (cityModel == null)
                    throw new ArgumentNullException("cityModel");

                var city = cityModel.ToCity();
                //Validate Region
                var validationResult = ValidateCity(city);
                if (validationResult != null && validationResult.Count > 0)
                    return EntityResult.Failed(validationResult.ToArray());

                _unitOfWork.CityRepository.Add(city);
                _unitOfWork.Save();
                cityModel.CityId = city.Id;
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
        }

        public EntityResult UpdateCity(CityViewModel cityModel)
        {
            try
            {
                if (cityModel == null)
                    throw new ArgumentNullException("cityModel");

                var city = cityModel.ToCity();
                //Validate Region
                var validationResult = ValidateCity(city);
                if (validationResult != null && validationResult.Count > 0)
                    return EntityResult.Failed(validationResult.ToArray());

                _unitOfWork.CityRepository.Update(city);
                _unitOfWork.Save();
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
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
                throw;
            }
        }

        private List<ValidationError> ValidateCity(City city)
        {

            var isNameIsNotEmpty = new ExpressionSpecification<City>(c => !string.IsNullOrEmpty(c.Name));
            if (!isNameIsNotEmpty.IsSatisfiedBy(city))
                throw new ArgumentNullException("Name");

            var isRegionExist = new ExpressionSpecification<Region>(c => c.Id == city.RegionId);
            if (_unitOfWork.RegionRepository.Find(isRegionExist).Any() == false)
                throw new ArgumentException("Region is not exist.", "RegionId");

            List<ValidationError> errors = new List<ValidationError>();
            var isCityExistInRegion = new ExpressionSpecification<Region>(c => c.Id != city.Id
             && c.CountryId == city.RegionId && c.Name.ToLower() == city.Name.ToLower());

            if (_unitOfWork.RegionRepository.Find(isCityExistInRegion).Any())
                errors.Add(new ValidationError($"City { city.Name } is already exist."
                    , ValidationErrorTypes.DuplicatedValue));

            return errors;
        }
        #endregion

        #region Districts
        public PagedEntity<DistrictViewModel> GetDistricts(SearchDistrictViewModel searchDistrictModel)
        {
            var result = _unitOfWork.DistrictRepository.GetDistricts(searchDistrictModel.RegionId
                , searchDistrictModel.CityId, searchDistrictModel.DistrictName
                , searchDistrictModel.DistrictCode, searchDistrictModel.PageIndex, searchDistrictModel.PageSize);

            return new PagedEntity<DistrictViewModel>(result.Items.Select(c => c.ToDistrictModel()), result.TotalCount);
        }

        public List<DistrictViewModel> GetDistrictsByCityId(int cityId)
        {
            return _unitOfWork.DistrictRepository.GetDistrictsByCityId(cityId)
                .Select(c => c.ToDistrictModel()).ToList();
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

        public DistrictViewModel GetDistrictById(int districtId)
        {
            var district = _unitOfWork.DistrictRepository.GetByID(districtId);
            var districtViewModel = district.ToDistrictModel();
            districtViewModel.CountryId = district.City.Region.CountryId;
            districtViewModel.RegionId = district.City.RegionId;
            return districtViewModel;
        }

        public EntityResult AddDistrict(DistrictViewModel districtModel)
        {
            if (districtModel == null)
                throw new ArgumentNullException("districtModel");
            try
            {
                var district = districtModel.ToDistrict();
                _unitOfWork.DistrictRepository.Add(district);
                _unitOfWork.Save();
                district.Id = district.Id;
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
        }

        public EntityResult UpdateDistrict(DistrictViewModel districtModel)
        {
            if (districtModel == null)
                throw new ArgumentNullException("districtModel");

            try
            {
                var currentCity = _unitOfWork.DistrictRepository.GetByID(districtModel.DistrictId);
                if (currentCity == null)
                    throw new ArgumentException("District is not exist.", "districtModel");

                var district = districtModel.ToDistrict();
                _unitOfWork.DistrictRepository.Update(district);

                _unitOfWork.Save();
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
        }

        private List<ValidationError> ValidateDistrict(District district)
        {
            var isNameIsNotEmpty = new ExpressionSpecification<District>(c => !string.IsNullOrEmpty(c.Name));
            if (!isNameIsNotEmpty.IsSatisfiedBy(district))
                throw new ArgumentNullException("Name");

            var isCityExist = new ExpressionSpecification<City>(c => c.Id == district.CityId);
            if (_unitOfWork.CityRepository.Find(isCityExist).Any() == false)
                throw new ArgumentException("City is not exist.", "CityId");

            List<ValidationError> errors = new List<ValidationError>();
            var isDistrictExistInCity = new ExpressionSpecification<District>(c => c.Id != district.Id
             && c.CityId == district.CityId && c.Name.ToLower() == district.Name.ToLower());

            if (_unitOfWork.DistrictRepository.Find(isDistrictExistInCity).Any())
                errors.Add(new ValidationError($"District { district.Name } is already exist."
                    , ValidationErrorTypes.DuplicatedValue));

            return errors;
        }
        #endregion
    }
}
