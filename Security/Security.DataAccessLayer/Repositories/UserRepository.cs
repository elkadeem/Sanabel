using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.EntityFramework;
using BusinessSolutions.Common.Infra.Validation;
using Sanabel.Security.Domain;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sanabel.Security.Infra
{
    public class UserRepository : IUserRepository
    {
        private readonly SecurityContext _dbContext;
        public UserRepository(SecurityContext dbContext)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(dbContext, nameof(dbContext));
            _dbContext = dbContext;
        }

        public void Add(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            _dbContext.Users.Add(user);
        }

        public Task<User> FindByEmailAsync(string email)
        {
            Guard.StringIsNull<ArgumentNullException>(email, nameof(email));
            return _dbContext.Users.Include(c => c.Roles).Include(c => c.Claims)
                .Include(c => c.ExternalLogins)
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public Task<User> FindByLoginAsync(string loginProvider, string providerKey)
        {
            return _dbContext.Users.Include(c => c.Roles)
                .Include(c => c.Claims)
                .Include(c => c.ExternalLogins)
                .FirstOrDefaultAsync(c => c.ExternalLogins.Any(e => e.LoginProvider == loginProvider
                && e.ProviderKey == providerKey));
        }

        public Task<User> FindByUserNameAsync(string userName)
        {
            return _dbContext.Users.Include(c => c.Roles)
                .Include(c => c.Claims)
                .Include(c => c.ExternalLogins)
                .FirstOrDefaultAsync(c => c.UserName == userName);
        }

        public Task<User> GetUserByIdAsync(Guid id)
        {
            return _dbContext.Users.Include(c => c.Roles)
                .Include(c => c.Claims)
                .Include(c => c.ExternalLogins)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Remove(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            _dbContext.Users.Remove(user);
        }

        public PagedEntity<User> SearchUsers(string userName, string email, string fullName
            , int pageIndex, int pageSize)
        {
            var query = _dbContext.Users.AsQueryable();
            if (!string.IsNullOrEmpty(userName))
                query = query.Where(c => c.UserName.Contains(userName));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(c => c.Email.Contains(email));

            if (!string.IsNullOrEmpty(fullName))
                query = query.Where(c => c.FullName.Contains(fullName));

            int totalItemsCount = query.Count();
            var items = query.OrderBy(c => c.UserName)
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            return new PagedEntity<User>(items, totalItemsCount);
        }

        public void Update(User user)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(user, nameof(user));
            var entry = _dbContext.Entry(user);
            if (entry.State == EntityState.Detached)
            {
                _dbContext.Users.Attach(user);
                entry = _dbContext.Entry(user);
            }

            entry.State = EntityState.Modified;
        }
    }
}
