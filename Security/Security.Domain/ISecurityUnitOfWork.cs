using BusinessSolutions.Common.Core;

namespace Security.Domain
{
    public interface ISecurityUnitOfWork : IUnitOfWork
    {
        IRoleRepository RoleRepository { get; }

        IUserRepository UserRepository { get; }

        IReadOnlyRepository<int, Country> CountryRepository { get; }

        IReadOnlyRepository<int, Region> RegionRepository { get; }

        IReadOnlyRepository<int, City> CityRepository { get; }

        IReadOnlyRepository<int, District> DistrictRepository { get; }
    }
}
