using RocketLoopCoreApi.Models;
using RocketLoopCoreApi.Repositories;
using System.Threading.Tasks;
using Xunit;
namespace RocketLoopXUnit.Repositories
{
    public class UserRepositoryTest
    {
        protected UserRepository _userRepository { get; }
        protected User[] _users { get; }

        public UserRepositoryTest()
        {
            _users = new User[]
                {
                    new User { Id = 1, Name = "User 1" },
                    new User { Id = 2, Name = "User 2" },
                    new User { Id = 3, Name = "User 3" }
                };

            _userRepository = new UserRepository(_users);
        }

        [Fact]
        public async Task Should_return_all_users()
        {
            // Act
            var result = await _userRepository.GetAllUsersAsync();

            // Assert
            Assert.Collection(result,
                user => Assert.Same(_users[0], user),
                user => Assert.Same(_users[1], user),
                user => Assert.Same(_users[2], user)
            );
        }
    }
}
