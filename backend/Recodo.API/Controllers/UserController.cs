using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromForm] UpdateUserDTO userDTO, [FromForm] IFormFile avatar)
        {
            await _userService.UpdateUser(userDTO, avatar);
            return NoContent();
        }

        [HttpPost("Update-Password-Email")]
        public async Task<IActionResult> UpdatePasswordEmail([FromBody] UpdateUserDTO userDTO)
        {
            await _userService.UpdateUserPasswordEmail(userDTO);
            return NoContent();
        }

        [HttpPost("Reset-Password/{userId:int}")]
        public async Task<IActionResult> ResetPassword(int userId)
        {
            await _userService.ResetPassword(userId);
            return NoContent();
        }

        [HttpPost("Delete-User/{userId:int}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUser(userId);
            return NoContent();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("FromToken")]
        public async Task<IActionResult> GetUserFromToken()
        {
            var user = await _userService.GetUserById(this.GetUserIdFromToken());

            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            return Ok(await _userService.GetUserById(id));
        }
    }
}
