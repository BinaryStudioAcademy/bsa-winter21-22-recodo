using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recodo.BLL.Services;
using Recodo.Common.Dtos.Video;
using System.Threading.Tasks;
using Recodo.API.Extensions;

namespace Recodo.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly MailService _mailService;
        private readonly UserService _userService;

        public MailController(MailService mailService, UserService userService)
        {
            _mailService = mailService;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Share(VideoShareDTO sharePostInfo)
        {
            var user = await _userService.GetUserById(this.GetUserIdFromToken());
            _mailService.SendEmail(sharePostInfo.Link, sharePostInfo.Email, user.WorkspaceName);
            return Ok();
        }
    }
}