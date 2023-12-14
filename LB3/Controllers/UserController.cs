using LB3.Models;
using LB3.Services;
using Microsoft.AspNetCore.Mvc;

namespace LB3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get(string username)
        {
            var user = _userService.get(username);
            user.Password = null;
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            var user_new = _userService.create(user);
            return Ok(user_new);
        }

        [HttpDelete]
        public IActionResult Delete(string username)
        {
            return _userService.delete(username) ? Ok() : NotFound();
        }

        [HttpPut]
        public IActionResult Put(string username, User user)
        {
            var user_new = _userService.update(username, user);
            return Ok(user_new);
        }
    }
}
