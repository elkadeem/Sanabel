using BusinessSolutions.Common.EntityFramework;
using CommonSettings.Domain.Repositories;
using CommonSettings.Domain.UnitOfWork;
using Grace.DependencyInjection.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonSettings.DAL
{
    [ExportByInterfaces()]
    public class CommonSettingsUnitOfWork : BaseUnitOfWork, ICommonSettingsUnitOfWork
    {        
        public CommonSettingsUnitOfWork(CommonSettingDataContext dbContext) : base(dbContext)
        {           
            CountryRepository = new CountryRepository(dbContext);
            CityRepository = new CityRepository(dbContext);
            RegionRepository = new RegionRepository(dbContext);
            DistrictRepository = new DistrictRepository(dbContext);
        }

        public ICountryRepository CountryRepository { get; private set; }

        public ICityRepository CityRepository { get; private set; }

        public IRegionRepository RegionRepository { get; private set; }

        public IDistrictRepository DistrictRepository { get; private set; }
        
    }
}
