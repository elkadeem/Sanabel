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
            //AddCity();
            //AddRegion();
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
            PlaceServiceTestCases.Region = new Region
            {
                Name = "Region2",
                CountryId = PlaceServiceTestCases.Country.CountryId,
            };

            PlaceServiceTestCases.Region = _placesServices.SaveRegion(PlaceServiceTestCases.Region);
        }

        public void AddCity()
        {
            PlaceServiceTestCases.City = new City { RegionId = PlaceServiceTestCases.Region.Id, Name = "CityName" };
            PlaceServiceTestCases.City = _placesServices.SaveCity(PlaceServiceTestCases.City);
        }

        public void AddDistrict()
        {
            PlaceServiceTestCases.District = new District { CityId = PlaceServiceTestCases.City.Id, Name = "DistrictName" };
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
            ([Values("", "Country")]string countryName, [Values(0, 1)]int pageIndex)
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
        [TestCase("Country", "", 20, 10)]
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

    }
}
