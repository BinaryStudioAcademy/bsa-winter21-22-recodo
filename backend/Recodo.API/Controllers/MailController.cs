using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recodo.BLL.Services;
using Recodo.Common.Dtos.Video;

namespace Recodo.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly MailService _mailService;

        public MailController(MailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SharePost(VideoShareDTO sharePostInfo)
        {
            _mailService.SendEmail(sharePostInfo.Link, sharePostInfo.Email);
            return Ok();
        }
    }
}