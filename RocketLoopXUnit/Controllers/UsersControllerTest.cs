using System;
using Xunit;
using RocketLoopCoreApi.Controllers;
using RocketLoopCoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RocketLoopCoreApi.Services;
using RocketLoopCoreApi.Repositories.Interfaces;
using RocketLoopCoreApi.Repositories;
using System.Collections.Generic;
using RocketLoopCoreApi.Models;

namespace RocketLoopXUnit
{
    public class UsersControllerTest
    {
        protected UsersController _controller { get; }
        protected IUserService _service { get; }
        protected IUserRepository _repository { get; }
        private List<User> _users;

        public UsersControllerTest()
        {
            // Arrange
            _users = new List<User> (new User[] {
                new User { Id = 1, Name = "name1" },
                new User { Id = 2, Name = "name2" }
            });

            _repository = new UserRepository(_users);
            _service = new UserService(_repository);
            _controller = new UsersController(_service);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var response = await _controller.Get() as ObjectResult;
            var users = response.Value as List<User>;

            // Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.IsType<List<User>>(users);
            Assert.Collection(users,
                    user => Assert.Same(_users[0], user),
                    user => Assert.Same(_users[1], user)
                );
        }

        [Fact]
        public async Task GetUserAsync_WhenCalled_ReturnsOkResult()
        {
            // Act
            var response = await _controller.GetUserAsync(_users[0].Id) as ObjectResult;
            var user = response.Value as User;

            // Assert
            Assert.IsType<ObjectResult>(response);
            Assert.IsType<User>(user);
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_ReturnsOkResult()
        {
            var newUser = new User { Id = 1, Name = "name1 updated" };

            // Act
            var response = await _controller.UpdateAsync(newUser) as ObjectResult;
            var user = response.Value as User;

            // Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Same(user.Name, newUser.Name);
            Assert.IsType<User>(user);
        }
    }
}
