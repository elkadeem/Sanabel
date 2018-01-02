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
            };
        }
    }
}
