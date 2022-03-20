using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
