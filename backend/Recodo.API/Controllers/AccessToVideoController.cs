using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodo.BLL.Services;
using Recodo.Common.Dtos.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodo.API.Controllers
{
    [Route("api/access")]
    [ApiController]
    public class AccessToVideoController : ControllerBase
    {
        private readonly AccessToVideoService _accessToVideoService;
        public AccessToVideoController(AccessToVideoService accessToVideoService)
        {
            _accessToVideoService = accessToVideoService;
        }

        [HttpPut]
        public async Task<IActionResult> AddNewAccess(string email, int videoId)
        {
            await _accessToVideoService.AddUserAccess(email, videoId);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<AccessForRegisteredUsersDTO>> GetRegisteredUser(AccessForRegisteredUsersDTO AccessedUser)
        {
            var registeredUser = await _accessToVideoService.FindRegisteredUserAccess(AccessedUser);
            return Ok(registeredUser);
        }
    }
}
