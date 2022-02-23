using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recodo.API.Extensions;
using Recodo.BLL.Services;
using Recodo.Common.Dtos.User;
using System.Threading.Tasks;

namespace Recodo.API.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;

        public UserController(AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(UpdateUserDTO user)
        {
            await _userService.ResetPassword(user.Email);
            return NoContent();
        }

        [HttpPost("ResetPasswordDone")]
        public async Task<IActionResult> ResetPasswordDone(UpdateUserDTO user)
        {
            await _userService.ResetPasswordDone(user);
            return NoContent();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("fromToken")]
        public async Task<IActionResult> GetUserFromToken()
        {
            var user = await _userService.GetUserById(this.GetUserIdFromToken());

            return Ok(user);
        }
    }
}
