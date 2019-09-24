using Microsoft.AspNetCore.Mvc;
using RocketLoopCoreApi.Models;
using RocketLoopCoreApi.Services.Interfaces;
using System.Threading.Tasks;

namespace RocketLoopCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            User user = await _userService.GetUserAsync(id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync([FromBody]User user)
        {
            if (user == null)
            {
                return BadRequest(ModelState);
            }
            User userResult = await _userService.UpdateAsync(user);
            return Ok(userResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.CreateAsync(user);
            return new ObjectResult(user);
        }
    }
}