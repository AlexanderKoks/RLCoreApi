using RocketLoopCoreApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RocketLoopCoreApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<User> UpdateAsync(User user);
        Task CreateAsync(User user);
        Task DeleteAsync(int id);
    }
}
