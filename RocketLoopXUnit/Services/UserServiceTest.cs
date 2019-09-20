using Moq;
using RocketLoopCoreApi.Models;
using RocketLoopCoreApi.Repositories.Interfaces;
using RocketLoopCoreApi.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xunit;

namespace RocketLoopXUnit.Services
{
    public class UserServiceTest
    {
        protected UserService _service { get; }
        protected Mock<IUserRepository> _userRepositoryMock { get; }

        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _service = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_return_all_users()
        {
            // Arrange
            var expectedUsers = new ReadOnlyCollection<User>(new List<User>
                {
                    new User { Id = 1, Name = "User 1" },
                    new User { Id = 2, Name = "User 2" },
                    new User { Id = 3, Name = "User 3" }
                });
            _userRepositoryMock
                .Setup(x => x.GetAllUsersAsync())
                .ReturnsAsync(expectedUsers);

            // Act
            var result = await _service.GetAllUsersAsync();

            // Assert
            Assert.Same(expectedUsers, result);
        }
    }
}
