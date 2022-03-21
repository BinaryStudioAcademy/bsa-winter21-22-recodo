using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recodo.API.Extensions;
using Recodo.BLL.Services;
using System.Threading.Tasks;

namespace Recodo.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("fromToken")]
        public async Task<IActionResult> GetUserFromToken()
        {
            var user = await _userService.GetUserById(this.GetUserIdFromToken());

            return Ok(user);
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("AddToTeam")]
        public async Task<IActionResult> AddToTeam([FromQuery] string authorEmail)
        {
            await _userService.AddToTeam(this.GetUserIdFromToken(), authorEmail);

            return NoContent();
        }

    }
}
