using BusinessSolutions.Common.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sanabel.Security.Domain
{
    public interface IUserRepository
    {
        PagedEntity<User> SearchUsers(string userName, string email, string fullName            
            , int pageIndex, int pageSize);
        void Add(User user);
        void Remove(User user);
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> FindByUserNameAsync(string userName);
        void Update(User user);
        Task<User> FindByLoginAsync(string loginProvider, string providerKey);
        Task<User> FindByEmailAsync(string email);
    }

}
