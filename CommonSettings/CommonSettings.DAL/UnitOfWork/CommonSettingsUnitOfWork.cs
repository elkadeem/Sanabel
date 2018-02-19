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
    public class CommonSettingsUnitOfWork : ICommonSettingsUnitOfWork
    {
        private readonly CommonSettingDataContext _dataContext;
        public CommonSettingsUnitOfWork(CommonSettingDataContext dataContext)
        {
            if (dataContext == null)
                throw new ArgumentNullException("dataContext");

            _dataContext = dataContext;
            CountryRepository = new CountryRepository(dataContext);
            CityRepository = new CityRepository(dataContext);
            RegionRepository = new RegionRepository(dataContext);
            DistrictRepository = new DistrictRepository(dataContext);
        }

        public ICountryRepository CountryRepository { get; private set; }

        public ICityRepository CityRepository { get; private set; }

        public IRegionRepository RegionRepository { get; private set; }

        public IDistrictRepository DistrictRepository { get; private set; }

        public int Save()
        {
            return _dataContext.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _dataContext.SaveChangesAsync();
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return _dataContext.SaveChangesAsync(cancellationToken);
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_dataContext != null)
                        _dataContext.Dispose();
                }

                disposedValue = true;
            }
        }

        ~CommonSettingsUnitOfWork()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
