using BusinessSolutions.Common.EntityFramework;
using Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;

namespace Security.DataAccessLayer.Repositories
{
    public class UserRepository : BaseEntityFrameworkRepository<Guid, User>, IUserRepository
    {
        public UserRepository(SecurityContext securityContext) : base(securityContext)
        {

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
    }
}
