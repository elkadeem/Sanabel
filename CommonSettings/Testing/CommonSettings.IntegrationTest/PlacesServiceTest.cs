using CommonSettings.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CommonSettings.IntegrationTest
{
    [TestFixture]
    public class PlacesServiceTest
    {
        private CommonSettings.Domain.Services.IPlacesService _placesServices;
        
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
            PlaceServiceTestCases.Country = new Country
            {                
                Name = "CountryName",                
            };

            PlaceServiceTestCases.Country = _placesServices.SaveCountry(PlaceServiceTestCases.Country);
        }
                
        public void AddRegion()
        {
            PlaceServiceTestCases.Region = new Region
            {
                Name = "Region2",
                CountryId = PlaceServiceTestCases.Country.Id,
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
            else {
                country.Should().NotBeNull();
                country.Id.Should().Be(countryId);
            }
        }

        [Test]
        [Pairwise]
        public void GetCountries_WithValidAndInvalidIndex_GetItemsOrEmptyItems
            ([Values("", "Country")]string countryName, [Values(0, 1)]int pageIndex)
        {
            var countriesPage = _placesServices.GetCountries(countryName, "", pageIndex, 10);
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
            countries.Should().Contain(c => c.Id == PlaceServiceTestCases.Country.Id);
        }

        [Test]
        public void SaveCountry_UpdateCurrentCountryName_UpdateCountry()
        {
            PlaceServiceTestCases.Country.Name = "UpdateName";
            PlaceServiceTestCases.Country.NameEn = "UpdateNameEn";
            PlaceServiceTestCases.Country.Code = "000";

            var country = _placesServices.SaveCountry(PlaceServiceTestCases.Country);
            country = _placesServices.GetCountryById(PlaceServiceTestCases.Country.Id);
            country.Id.Should().Be(PlaceServiceTestCases.Country.Id);
            country.Name.Should().Be("UpdateName");
            country.NameEn.Should().Be("UpdateNameEn");
            country.Code.Should().Be("000");
        }
        #endregion

    }
}
