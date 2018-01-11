using BusinessSolutions.Common.EntityFramework;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;
using BusinessSolutions.Common.Core;

namespace Security.DataAccessLayer.Repositories
{
    public class UserRepository : BaseEntityFrameworkRepository<Guid, User>, IUserRepository
    {
        public UserRepository(SecurityContext securityContext) : base(securityContext)
        {

        }

        public override User GetByID(Guid key)
        {
            return Set.Include(c => c.Roles)
                .Include(c => c.Claims)
                .Include(c => c.ExternalLogins).FirstOrDefault(c => c.Id == key);
        }

        public override Task<User> GetByIDAsync(Guid key)
        {
            return Set.Include(c => c.Roles)
                .Include(c => c.Claims)
                .Include(c => c.ExternalLogins).FirstOrDefaultAsync(c => c.Id == key);
        }

        public override Task<User> GetByIDAsync(CancellationToken cancellationToken, Guid key)
        {
            return Set.Include(c => c.Roles)
                .Include(c => c.Claims)
                .Include(c => c.ExternalLogins).FirstOrDefaultAsync(c => c.Id == key, cancellationToken);
        }

        public User FindByEmail(string email)
        {
            return Set.FirstOrDefault(c => c.Email == email);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            var user = await Set.FirstOrDefaultAsync(c => c.Email == email);
            return user;
        }

        public async Task<User> FindByEmailAsync(CancellationToken cancellationToken, string email)
        {
            var user = await Set.FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
            return user;
        }

        public async Task<User> FindByLoginAsync(string loginProvider, string loginKey)
        {
            var user = await Set.FirstOrDefaultAsync(c => c.ExternalLogins.Any(e => e.LoginProvider == loginProvider
             && e.ProviderKey == loginKey));

            return user;
        }

        public User FindByUserName(string userName)
        {
            var user = Set.FirstOrDefault(c => c.UserName == userName);
            return user;
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            var user = await Set.FirstOrDefaultAsync(c => c.UserName == userName);
            return user;
        }

        public async Task<User> FindByUserNameAsync(CancellationToken cancellationToken, string userName)
        {
            var user = await Set.FirstOrDefaultAsync(c => c.UserName == userName, cancellationToken);
            return user;
        }

        public PagedEntity<User> SearchUsers(string userName, string email, string fullName
            , int countryId, int regionId, int cityId, int districtId, int pageIndex, int pageSize)
        {
            var query = Set.AsNoTracking().Include(c => c.District)
                .Include(c => c.City.Region.Country);

            if (!string.IsNullOrEmpty(userName))
                query = query.Where(c => c.UserName.Contains(userName));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(c => c.Email.Contains(email));

            if (!string.IsNullOrEmpty(fullName))
                query = query.Where(c => c.FullName.Contains(fullName));

            if (countryId > 0)
                query = query.Where(c => c.City.Region.CountryId == countryId);

            if (regionId > 0)
                query = query.Where(c => c.City.RegionId == regionId);

            if (cityId > 0)
                query = query.Where(c => c.CityId == cityId);

            if (districtId > 0)
                query = query.Where(c => c.DistrictId == districtId);

            int totalItemsCount = query.Count();
            var items = query.OrderBy(c => c.UserName)
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            return new PagedEntity<User>(items, totalItemsCount);
        }
    }
}
