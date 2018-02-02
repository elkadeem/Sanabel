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
    public class UserRepository :  IUserRepository
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
                .FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower());
        }

        public Task<User> FindByLoginAsync(string loginProvider, string providerKey)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByUserNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(User user)
        {
            throw new NotImplementedException();
        }

        public PagedEntity<User> SearchUsers(string userName, string email, string fullName
            , int pageIndex, int pageSize)
        {
            var query = _repository.Query;
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
            throw new NotImplementedException();
        }
    }
}
