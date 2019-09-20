using RocketLoopCoreApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RocketLoopCoreApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<User> UpdateAsync(User user);
        Task CreateAsync(User user);
        Task DeleteAsync(int id);
    }
}
