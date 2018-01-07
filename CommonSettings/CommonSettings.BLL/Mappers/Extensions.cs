using CommonSettings.Domain.Entities;
using CommonSettings.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.BLL.Mappers
{
    public static class Extensions
    {
        public static Country ToCountry(this CountryViewModel countryModel)
        {
            if (countryModel == null)
                return null;
            return new Country
            {
                Code = countryModel.CountryCode,
                Id = countryModel.CountryId,
                Name = countryModel.CountryName,
                NameEn = countryModel.CountryNameEn,
            };
        }

        public static CountryViewModel ToCountryModel(this Country country)
        {
            if (country == null)
                return null;
            return new CountryViewModel
            {
                CountryCode = country.Code,
                CountryId = country.Id,
                CountryName = country.Name,
                CountryNameEn = country.NameEn,
            };
        }

        public static Region ToRegion(this RegionViewModel regionViewModel)
        {
            if (regionViewModel == null)
                return null;
            return new Region
            {
                Code = regionViewModel.RegionCode,
                CountryId = regionViewModel.CountryId,
                Id = regionViewModel.RegionId,
                Name = regionViewModel.RegionName,
                NameEn = regionViewModel.RegionNameEn
            };
        }

        public static RegionViewModel ToRegionViewModel(this Region region)
        {
            if (region == null)
                return null;
            return new RegionViewModel
            {
                RegionId = region.Id,
                CountryId = region.CountryId,
                RegionCode = region.Code,
                RegionName = region.Name,
                RegionNameEn = region.NameEn,
                Country = region.Country == null ? null : region.Country.ToCountryModel()
            };
        }

        public static City ToCity(this CityViewModel cityModel)
        {
            if (cityModel == null)
                return null;
            return new City
            {
                Code = cityModel.CityCode,
                Id = cityModel.CityId,
                Name = cityModel.CityName,
                NameEn = cityModel.CityNameEn,
                RegionId = cityModel.RegionId,
            };
        }

        public static CityViewModel ToCityViewModel(this City city)
        {
            if (city == null)
                return null;

            return new CityViewModel
            {
                CityCode = city.Code,
                CityId = city.Id,
                CityName = city.Name,
                CityNameEn = city.NameEn,
                RegionId = city.RegionId,
                Region = city.Region.ToRegionViewModel()
            };
        }

        public static District ToDistrict(this DistrictViewModel districtModel)
        {
            if (districtModel == null)
                return null;
            return new District
            {
                CityId = districtModel.CityId,
                Code = districtModel.DistricCode,
                Id = districtModel.DistrictId,
                Name = districtModel.DistricName,
                NameEn = districtModel.DistricNameEn
            };
        }

        public static DistrictViewModel ToDistrictModel(this District district)
        {
            if (district == null)
                return null;

            return new DistrictViewModel
            {
                City = district.City.ToCityViewModel(),
                CityId = district.CityId,
                DistricCode = district.Code,
                DistricName = district.Name,
                DistricNameEn = district.NameEn,
                DistrictId = district.Id,
            };
        }

    }
}
