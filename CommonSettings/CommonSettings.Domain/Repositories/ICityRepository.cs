using System.Collections.Generic;
using BusinessSolutions.Common.Core;
using CommonSettings.Domain.Entities;

namespace CommonSettings.Domain.Repositories
{
    public interface ICityRepository : IRepository<int, City>
    {
        PagedEntity<City> GetCities(int countryId, int regionId, string cityName, string code, int pageIndex, int pageSize);

        List<City> GetCitiesByCountryId(int CountryId);
        List<City> GetCitiesByRegionId(int regionId);
    }
}