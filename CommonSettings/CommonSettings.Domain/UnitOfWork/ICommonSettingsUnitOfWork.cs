using BusinessSolutions.Common.Core;
using CommonSettings.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.Domain.UnitOfWork
{
    public interface ICommonSettingsUnitOfWork : IUnitOfWork
    {
        ICountryRepository CountryRepository { get; }

        ICityRepository CityRepository { get; }

        IRegionRepository RegionRepository { get; }

        IDistrictRepository DistrictRepository { get; }
    }
}
