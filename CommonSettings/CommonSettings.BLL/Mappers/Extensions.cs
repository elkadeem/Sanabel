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
    }
}
