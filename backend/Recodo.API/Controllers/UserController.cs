using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        [Route("Add-To-Team/{token}")]
        public async Task<IActionResult> AddToTeam(string token)
        {
            await _userService.AddToTeam(this.GetUserIdFromToken(), token);

            return NoContent();
        }

        [HttpGet]
        [Route("Send-Invite-Link/{email}")]
        public async Task<IActionResult> SendInviteLink(string email)
        {
            await _teamService.SendInviteLink(this.GetUserIdFromToken(), email);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            return Ok(await _userService.GetUserById(id));
        }
    }
}
