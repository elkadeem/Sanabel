using BusinessSolutions.Common.Core;
using CommonSettings.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.Domain.Repositories
{
    public interface ICountryRepository : IRepository<int, Country>
    {
        PagedEntity<Country> GetCountries(string countryName, string code, int pageIndex, int pageSize);
    }
}
