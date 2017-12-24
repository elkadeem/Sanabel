using CommonSettings.Domain.Entities;
using CommonSettings.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.IntegrationTest
{
    public class PlaceServiceTestCases
    {
        public static CountryViewModel Country;
        public static Region Region;
        public static City City;
        public static District District;

        public static IEnumerable<TestCaseData> CountryTestCases
        {
            get
            {
                return new List<TestCaseData> {
                    new TestCaseData(1).SetName("GetCountryById_WithvalidId_ReturnCountry"),
                    new TestCaseData(0).SetName("GetCountryById_WithInvalidId_ReturnNull"),
                };

            }
        }
    }
}
