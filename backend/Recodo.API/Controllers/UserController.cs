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

        [HttpPost("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserDTO userDTO)
        {
            await _userService.UpdateUserPassword(userDTO);
            return NoContent();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UpdateUserDTO userDTO)
        {
            await _userService.ResetPassword(userDTO);
            return NoContent();
        }

        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] UpdateUserDTO userDTO)
        {
            await _userService.DeleteUser(userDTO);
            return NoContent();
        }
    }
}
