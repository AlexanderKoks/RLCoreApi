using RocketLoopCoreApi.Models;
using RocketLoopCoreApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketLoopCoreApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IEnumerable<User> _users;
        public UserRepository(IEnumerable<User> users)
        {
            _users = users;
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return Task.FromResult(_users.AsEnumerable());
        }

        public Task<User> GetUserAsync(int id)
        {
            var user = _users.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(user);
        }

        public Task<User> UpdateAsync(User user)
        {
            var result = _users.FirstOrDefault(c => c.Id == user.Id);
            result.Name = user.Name;

            return Task.FromResult(result); ;
        }

        public Task CreateAsync(User user)
        {
            _users.ToList().Add(user);
            return Task.FromResult(_users);
        }

        public Task DeleteAsync(int id)
        {
            var user = _users.FirstOrDefault(c => c.Id == id);
            _users.ToList().Remove(user);
            return Task.FromResult(_users);
        }
    }
}
