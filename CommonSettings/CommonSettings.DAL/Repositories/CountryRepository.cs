using BusinessSolutions.Common.EntityFramework;
using CommonSettings.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSettings.Domain.Entities;
using BusinessSolutions.Common.Core;
using Grace.DependencyInjection.Attributes;

namespace CommonSettings.DAL
{
    [ExportByInterfaces()]
    public class CountryRepository : BaseEntityFrameworkRepository<int, Country>, ICountryRepository
    {
        public CountryRepository(CommonSettingDataContext dataContext) : base(dataContext)
        {
        }

        public PagedEntity<Domain.Entities.Country> GetCountries(string countryName
            , string code, int pageIndex, int pageSize)
        {
            var query = Set.AsQueryable();            
            if (!string.IsNullOrEmpty(countryName))
                query = query.Where(c => c.Name.Contains(countryName)
                 || c.NameEn.Contains(countryName));
            if (!string.IsNullOrEmpty(code))
                query = query.Where(c => c.Code.Contains(code));

            int totalCount = query.Count();
            var items = query.OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToList()
                .ToList();

            return new PagedEntity<Country>(items, totalCount);
        }
    }
}
