using CommonSettings.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using CommonSettings.BLL;
using CommonSettings.ViewModels;
using BusinessSolutions.Common.Core.Specifications;

namespace CommonSettings.IntegrationTest
{
    [TestFixture]
    public class PlacesServiceTest
    {
        private IPlacesService _placesServices;

        [OneTimeSetUp]
        public void Initiate()
        {
            var dataContext = new CommonSettings.DAL.CommonSettingDataContext();
            var unitOfWork = new CommonSettings.DAL.CommonSettingsUnitOfWork(dataContext);
            var log = NLog.LogManager.CreateNullLogger();
            _placesServices = new CommonSettings.BLL.PlacesService(unitOfWork, log);

            AddCountry();
            AddRegion();
            AddCity();

            //AddDistrict();

        }

        private void AddCountry()
        {
            PlaceServiceTestCases.Country = new CountryViewModel
            {
                CountryCode = "00525",
                CountryName = "CountryName",
            };

            var result = _placesServices.AddCountry(PlaceServiceTestCases.Country);
        }

        public void AddRegion()
        {
            PlaceServiceTestCases.Region = new RegionViewModel
            {
                RegionName = "Region2",
                CountryId = PlaceServiceTestCases.Country.CountryId,
            };

            _placesServices.AddRegion(PlaceServiceTestCases.Region);

            for (int i = 1; i <= 5; i++)
            {
                var region = new RegionViewModel
                {
                    RegionName = "RegionForIndex" + i,
                    RegionCode = "Code" + i,
                    CountryId = PlaceServiceTestCases.Country.CountryId,
                };

                var addResult = _placesServices.AddRegion(region);
            }
        }

        public void AddCity()
        {
            PlaceServiceTestCases.City = 
                new CityViewModel { RegionId = PlaceServiceTestCases.Region.RegionId, CityName = "ParentCity" };

            var result = _placesServices.AddCity(PlaceServiceTestCases.City);

            for (int i = 0; i < 5; i++)
            {
                _placesServices.AddCity(new CityViewModel
                {
                    CityCode = "Code" + i,
                    CityName = "المدينة" + i,
                    CityNameEn = "CityName" + i,
                    RegionId = PlaceServiceTestCases.Region.RegionId,
                });
            }
        }

        public void AddDistrict()
        {
            PlaceServiceTestCases.District = new District { CityId = PlaceServiceTestCases.City.CityId, Name = "DistrictName" };
            PlaceServiceTestCases.District = _placesServices.SaveDistrict(PlaceServiceTestCases.District);
        }

        #region Country
        [Test]
        public void GetCountryById_WithZeroAndOne_GetNullAndThenCountry([Values(0, 1)] int countryId)
        {
            var country = _placesServices.GetCountryById(countryId);
            if (countryId == 0)
                country.Should().BeNull();
            else
            {
                country.Should().NotBeNull();
                country.CountryId.Should().Be(countryId);
            }
        }

        [Test]
        [Pairwise]
        public void GetCountries_WithValidAndInvalidIndex_GetItemsOrEmptyItems
            ([Values("", "Country")]string countryName, [Values(0, 20)]int pageIndex)
        {
            SearchCountryViewModel searchViewModel = new SearchCountryViewModel
            {
                PageIndex = pageIndex,
            };

            var countriesPage = _placesServices.GetCountries(searchViewModel);
            countriesPage.TotalCount.Should().BeGreaterOrEqualTo(1);
            if (pageIndex == 0)
            {
                countriesPage.Items.Count().Should().BeGreaterOrEqualTo(1);
            }
            else
            {
                countriesPage.Items.Count().Should().Be(0);
            }
        }

        [Test]
        public void GetAllCounty_ReturnAllCountries()
        {
            var countries = _placesServices.GetAllCountries();
            countries.Count.Should().BeGreaterThan(0);
            countries.Should().Contain(c => c.CountryId == PlaceServiceTestCases.Country.CountryId);
        }

        [Test]
        public void SaveCountry_UpdateCurrentCountryName_UpdateCountry()
        {
            PlaceServiceTestCases.Country.CountryName = "UpdateName";
            PlaceServiceTestCases.Country.CountryNameEn = "UpdateNameEn";
            PlaceServiceTestCases.Country.CountryCode = "000";

            BusinessSolutions.Common.Infra.Validation.EntityResult result
                = _placesServices.UpdateCountry(PlaceServiceTestCases.Country);
            var country = _placesServices.GetCountryById(PlaceServiceTestCases.Country.CountryId);
            country.CountryId.Should().Be(PlaceServiceTestCases.Country.CountryId);
            country.CountryName.Should().Be("UpdateName");
            country.CountryNameEn.Should().Be("UpdateNameEn");
            country.CountryCode.Should().Be("000");
        }

        [Test]
        [TestCase("Country1", "0001", 1, 1)]
        [TestCase("Country", "", 21, 10)]
        [TestCase("", "000", 9, 9)]
        [TestCase("Country11", "110011", 0, 0)]
        public void GetCountrtiesByUsingSpeciifcartions(string countryName, string code
            , int expectedTotalCount, int expectedItemsCount)
        {
            var result = _placesServices.GetCountries(new SearchCountryViewModel
            {
                CountryCode = code,
                CountryName = countryName,
            });

            result.TotalCount.Should().Be(expectedTotalCount);
            result.Items.Count().Should().Be(expectedItemsCount);
        }
        #endregion

        [Test]
        public void AddRegion_WithValidCountryAndName_ReturnSuccess()
        {
            var country = new CountryViewModel
            {
                CountryCode = "1111",
                CountryName = "CountryThatHaveRegion",
            };

            _placesServices.AddCountry(country);
            var region = new RegionViewModel
            {
                CountryId = country.CountryId,
                RegionName = "RegionToAdd"
            };

            var result = _placesServices.AddRegion(region);
            result.Succeeded.Should().Be(true);
        }

        [Test]
        [TestCase("RegionForIndex1", "", 1, 1)]
        [TestCase("", "Code1", 1, 1)]
        [TestCase("RegionForIndex", "", 3, 5)]
        [TestCase("NotFound", "", 0, 0)]
        [TestCase("", "NotFound", 0, 0)]
        public void GetRegions_WithValidParamters_ReturunTotalCountAndItemsIfFound(string regionName, string regionCode
            , int expectedItemsCount, int expectedTotalItemsCount)
        {

            SearchRegionViewModel searchRegionViewModel = new SearchRegionViewModel(3)
            {
                RegionName = regionName,
                CountryId = PlaceServiceTestCases.Country.CountryId,
                RegionCode = regionCode,
                PageIndex = 0,
            };

            var result = _placesServices.GetRegions(searchRegionViewModel);
            result.Items.Count.Should().Be(expectedItemsCount);
            result.TotalCount.Should().Be(expectedTotalItemsCount);
        }

        [Test]
        public void UpdateRegion_WithValidData_UpdateRegionData()
        {
            var region = new RegionViewModel
            {
                RegionId = PlaceServiceTestCases.Region.RegionId,
                CountryId = PlaceServiceTestCases.Region.CountryId,
            };

            region.RegionName = "UpdatedRegionName";
            region.RegionCode = "Updated1";
            region.RegionNameEn = "UpdatedRegionNameEn";

            var result = _placesServices.UpdateRegion(region);

            var currentRegion = _placesServices.GetRegionById(region.RegionId);
            result.Succeeded.Should().Be(true);
            currentRegion.RegionName.Should().Be(region.RegionName);
            currentRegion.RegionNameEn.Should().Be(region.RegionNameEn);
            currentRegion.RegionCode.Should().Be(region.RegionCode);
        }

        //[Test]
        public void DeleteRegion_WithValidId_DeleteRegion()
        {
            var regions = _placesServices.GetRegionsByCountryId(PlaceServiceTestCases.Region.CountryId);

            var isDeleted = _placesServices.DeleteRegion(regions.Last().RegionId);
            isDeleted.Should().Be(true);
        }

        #region Cities
        [Test]
        [TestCase("المدينة", "", 0, 3, 3, 5)]
        [TestCase("المدينة", "", 1, 3, 2, 5)]
        [TestCase("المدينة", "", 2, 3, 0, 5)]
        [TestCase("المدينة", "Code", 1, 3, 2, 5)]
        [TestCase("المدينة1", "Code1", 0, 3, 1, 1)]
        [TestCase("المدينة185", "Code1", 0, 3, 0, 0)]
        public void GetCities_WithValidParamters_ReturnExpectedResult(string cityName, string cityCode
            , int pageIndex, int pageSize
            , int expectedItemsCount, int expectedTotalCounts)
        {
            var searchViewModel = new SearchCityViewModel(3)
            {
                CityName = cityName,
                CityCode = cityCode,
            };

            var searchResult = _placesServices.GetCities(searchViewModel);
            searchResult.Items.Should().HaveCount(expectedItemsCount);
            searchResult.TotalCount.Should().Be(expectedTotalCounts);

        }
        #endregion 
    }
}
