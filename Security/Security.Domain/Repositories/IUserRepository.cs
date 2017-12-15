using BusinessSolutions.Common.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Security.Domain
{
    public interface IUserRepository : IRepository<Guid, User>
    {
        User FindByUserName(string userName);
        Task<User> FindByUserNameAsync(string userName);
        Task<User> FindByUserNameAsync(CancellationToken cancellationToken, string userName);

        User FindByEmail(string email);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByEmailAsync(CancellationToken cancellationToken, string email);

        Task<User> FindByLoginAsync(string loginProvider, string loginKey);
    }
   
}
