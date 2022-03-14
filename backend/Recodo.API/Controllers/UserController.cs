using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recodo.API.Extensions;
using Recodo.BLL.Services;
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

        [HttpPost("ResetPassword/{email}")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            await _userService.ResetPassword(email);
            return NoContent();
        }

        [HttpPost("ResetPasswordFinish/{email}/{newPass}")]
        public async Task<IActionResult> ResetPasswordDone(string email, string newPass)
        {
            var loginDto = await _userService.ResetPasswordFinish(email, newPass);
            var auth = await _authService.Authorize(loginDto, false);

            return Ok(auth);
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
